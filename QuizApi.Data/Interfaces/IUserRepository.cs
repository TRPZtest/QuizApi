using QuizApi.Data.Db.Enteties;

namespace QuizApi.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserAsync(string login, string password);
    }
}