using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuizApi.Data.Db;
using QuizApi.Data.Db.Enteties;
using QuizApi.Data.Interfaces;
using QuizApi.Models;
using System.ComponentModel.DataAnnotations;

namespace QuizApi.Services
{
    public class QuizService
    {
        private readonly IQuizRepository _repository;

        public QuizService(IQuizRepository repository) 
        { 
            _repository = repository;
        }
      
        public async Task<List<Quiz>> GetQuizzesAsync(long userId)
        {
            var quizzes = await _repository.GetQuizzesAsync(userId);
            
            return quizzes;
        }
       
        public async Task<Quiz> GetQuizAsync(long Id)
        {
            var quiz = await _repository.GetQuizAsync(Id);

            return quiz;    
        }

        public async Task<Take> AddTakeAsync(long quizId, long userId)
        {
            Take take; //instead of upsert
            try
            {
                take = await _repository.AddTakeAsync(new Take { QuizId = quizId, UserId = userId });

                await _repository.SaveChangesAsync();

                return take;
            }
            catch (Exception ex)
            {
                var innerEx = ex.InnerException;

                if (innerEx?.Message.Contains("UNIQUE") == true) 
                {
                    take = await _repository.GetTakeAsync(userId, quizId);

                    return take;
                }
                else
                    throw;            
            }            
        }
      
        public async Task<int> AddResponsesAsync(Response[] responses)
        {
            await _repository.AddResponsesAsync(responses);
            var addedItemsCount = await _repository.SaveChangesAsync();

            if (addedItemsCount < 1)
                throw new Exception("Error while adding response");

            return addedItemsCount;
        }
       
        public async Task<Result> AddResultAsync(long takeId)
        {           
            var responses = await _repository.GetResponses(takeId);
            var quiz = await _repository.GetQuizByTakeIdAsync(takeId);

            var maxPoint = quiz.Questions.Count();
            var points = responses.Count(x => x.Option.IsCorrect == true);

            var result = new Result { MaxPoints = maxPoint, Points = points, TakeId = takeId };

            await _repository.AddResultAsync(result);
            var addedItemsCount = await _repository.SaveChangesAsync();
          
            return result;
        }
      
        public async Task<Result?> GetResultAsync(long takeId)
        {
            var result = await _repository.GetResultAsync(takeId);
           
            return result;
        }

        public async Task<List<Option>> GetOptions(long questionId)
        {
            var options = await _repository.GetOptions(questionId);

            return options;
        }
    }
}
