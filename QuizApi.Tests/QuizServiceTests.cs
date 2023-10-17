using Microsoft.EntityFrameworkCore;
using QuizApi.Data.Db;
using QuizApi.Data.Db.Enteties;
using QuizApi.Services;
using QuizApi.Tests.TestHelpers;


namespace QuizApi.Tests
{
    public class QuizServiceTests
    {
        [Fact]
        public async Task GetTwoQuizzes()
        {
            var testDb = new TestDb();
            var context = testDb.GetDbContext();
            var service = new QuizService(new QuizRepository(context));

            var user = context.Users
                .AsNoTracking()
                .First(x => x.Login == "user1");

            var quizzes = await service.GetQuizzesAsync(user.Id);

            var quizzesCount = quizzes.Count();

            Assert.Equal(2, quizzesCount); //user with id = 1 must have two quizzes to pass
            context.Dispose();
        }

        [Fact]
        public async Task CheckResultPoints()
        {
            var testDb = new TestDb();
            var context = testDb.GetDbContext();
            var service = new QuizService(new QuizRepository(context));

            var user = context.Users
                .AsNoTracking()
                .First(x => x.Login == "user2");

            var quizId = 2;

            var correctOptions = context.Options.Where(x => x.IsCorrect == true && x.Question.QuizId == quizId).ToList(); //select correct answers

            var take = service.AddTakeAsync(quizId, user.Id);

            var responses = correctOptions.Select(x => new Response { OptionId = x.Id, TakeId = take.Id })
                .ToArray();

            await service.AddResponsesAsync(responses);

            var result = await service.AddResultAsync(take.Id);

            Assert.Equal(result.MaxPoints, result.Points); //only correct 
            context.Dispose();
        }

        [Fact]
        public async Task CheckTakeConstraint()
        {
            var testDb = new TestDb();
            var context = testDb.GetDbContext();
            var service = new QuizService(new QuizRepository(context));

            var quizId = 2;
            var user = context.Users
             .AsNoTracking()
             .First(x => x.Login == "user1");

            await service.AddTakeAsync(quizId, user.Id);
            var countAfterFirstAdd = context.Takes.Count();

            await service.AddTakeAsync(quizId, user.Id);
            var countAfterSecondAdd = context.Takes.Count();

            Assert.Equal(countAfterFirstAdd, countAfterSecondAdd); //count of takes must not change after first time
            context.Dispose();
        }


        [Fact]
        public async Task ResultOnOldestResponsesOnly()
        {
            var testDb = new TestDb();
            var context = testDb.GetDbContext();
            var service = new QuizService(new QuizRepository(context));
            
            var user = context.Users
             .AsNoTracking()
             .First(x => x.Login == "user1");

            var quizId = 1;

            var take = await service.AddTakeAsync(quizId, user.Id);
        
            var correctOptions = context.Options.Where(x => x.IsCorrect == true && x.Question.QuizId == quizId).ToList(); //select correct answers
            var correctResponses = correctOptions.Select(x => new Response { OptionId = x.Id, TakeId = take.Id })
                .ToArray();

            var randomOptions = context.Questions.Where(x => x.QuizId == quizId)
                .Select(x => x.Options.First(x => x.IsCorrect != true));
            var wrongResponses = randomOptions.Select(x => new Response { OptionId = x.Id, TakeId = take.Id })
                .ToArray();

            await service.AddResponsesAsync(correctResponses);
            await service.AddResponsesAsync(wrongResponses);

            var result = await service.AddResultAsync(take.Id);

            Assert.Equal(result.MaxPoints, result.Points); //In this case result must be calculated only from correct answers
        }

    }
}