using Microsoft.EntityFrameworkCore;
using QuizApi.Data.Db.Enteties;
using QuizApi.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApi.Data.Db
{
    public class QuizRepository : IQuizRepository
    {
        private readonly TestingDbContext _context;

        public QuizRepository(TestingDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Quiz>?> GetQuizzesAsync(long userId)
        {
            var user = await _context.Users
                .Include(x => x.Quizzes)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
            
            return user?.Quizzes?.ToList();
        }

        public async Task<Quiz> GetQuizAsync(long quizId)
        {
            var quiz = await _context.Quizzes
                .Include(x => x.Questions)                   
                    .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == quizId);       

            return quiz;
        }

        public async Task<Response[]> GetResponses(long takeId)
        {
            var responses = await _context.Responses.Where(x => x.TakeId == takeId)
                .Include(x => x.Option)
                .AsNoTracking()
                .ToArrayAsync();

            return responses;
        }

        public async Task<Response[]> GetResponses(long takeId, long questionId)
        {
            var responses = await _context.Responses.Where(x => x.TakeId == takeId)
                .Include(x => x.Option)
                .AsNoTracking()
                .ToArrayAsync();

            return responses;
        }

        public async Task AddResponsesAsync(params Response[] responses)
        {
            await _context.Responses.AddRangeAsync(responses);
        }

        public async Task<Take> AddTakeAsync(Take take)
        {
            var result = await _context.Takes.AddAsync(take);

            await _context.Entry(result.Entity).Reference(x => x.Result).LoadAsync();

            return result.Entity;
        }

        public async Task<Take?> GetTakeAsync(long userId, long quizId)
        {
            var take = await _context.Takes
                .Include(x => x.Result)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.QuizId == quizId);

            return take;
        }

        public async Task AddResultAsync(Result result)
        {
            await _context.Results.AddAsync(result);
        }

        public async Task<Result?> GetResultAsync(long takeId)
        {
            var result = await _context.Results
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == takeId);           
             
            return result;
        }

        public async Task<int> SaveChangesAsync()
        {
            var dbChangesCount = await _context.SaveChangesAsync();

            return dbChangesCount;
        }

        public async Task<Quiz> GetQuizByTakeIdAsync(long takeId)
        {
            var quiz = await _context.Quizzes
               .Include(x => x.Questions)
                   .ThenInclude(x => x.Options)
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Takes.Any(x => x.Id == takeId));

            return quiz;
        }

        public async Task<List<Option>> GetOptions(long questionId)
        {
            var options =await _context.Options
                .Where(x => x.QuestionId == questionId)
                .AsNoTracking()
                .ToListAsync();

            return options;
        }
    }
}
