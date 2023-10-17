using Azure.Core;
using QuizApi.Data.Db;
using QuizApi.Data.Interfaces;
using QuizApi.Helpers;
using QuizApi.Models;

namespace QuizApi.Services
{
    public class AuthService
    {
        private readonly IUserRepository _repository;

        public AuthService(IUserRepository userRepository) 
        {
            _repository = userRepository;
        }
        public async Task<string> GetJwtAsync(string login, string password)
        {
            var user = await _repository.GetUserAsync(login, password);

            if (user == null)
                return string.Empty;

            var token = JwtHelper.GetJwt(user);

            return token;
        }

    }
}
