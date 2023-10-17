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
    public class UserRepository : IUserRepository
    {
        private readonly TestingDbContext _context;

        public UserRepository(TestingDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<User?> GetUserAsync(string login, string password)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
            return user;
        }
    }
}
