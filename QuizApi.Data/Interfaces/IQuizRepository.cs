using Microsoft.EntityFrameworkCore;
using QuizApi.Data.Db.Enteties;
using System.Net.Quic;

namespace QuizApi.Data.Interfaces
{
    public interface IQuizRepository
    {
        Task AddResponsesAsync(params Response[] responses);
        Task AddResultAsync(Result result);
        Task<Take> AddTakeAsync(Take take);
        Task<Take?> GetTakeAsync(long userId, long quizId);             
        Task<Quiz> GetQuizAsync(long quizId);
        Task<Quiz> GetQuizByTakeIdAsync(long takeId);
        Task<List<Quiz>> GetQuizzesAsync(long userId);
        Task<Response[]> GetResponses(long takeId);
        Task<Response[]> GetResponses(long takeId, long questionId);
        Task<Result?> GetResultAsync(long takeId);
        Task<List<Option>> GetOptions(long questionId);
        Task<int> SaveChangesAsync();
    }
}