using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using QuizApi.Data.Db.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace QuizApi.Tests.TestHelpers
{
    internal class TestDb
    {
        public TestingDbContext GetDbContext()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            // These options will be used by the context instances in this test suite, including the connection opened above.
            var contextOptions = new DbContextOptionsBuilder<TestingDbContext>()
                .UseSqlite(connection)
                .Options;

            // Create the schema and seed some data
            var context = new TestingDbContext(contextOptions);

            context.Database.EnsureCreated();

            var user1 = new User { Login = "user1", Password = "password" };
            var user2 = new User { Login = "user2", Password = "password" };
            // Insert data into the Users table
           context.Users.AddRange(
                user1,
                user2
            );

            context.SaveChanges();

            // Insert data into the Quizzes table
            context.Quizzes.Add(new Quiz { Name = "C# basics", Users = new User[] { user1 } });
            context.SaveChanges();

            // Insert data into the Questions table
            context.Questions.AddRange(
                new Question { QuizId = 1, QuestionText = "C# is an alias of C++?" },
                new Question { QuizId = 1, QuestionText = "What is a correct syntax to output 'Hello World' in C#?" },
                new Question { QuizId = 1, QuestionText = "Which of the followings is not allowed in C# as an access modifier?" }
            );
            context.SaveChanges();
            // Insert data into the Options table
            context.Options.AddRange(
                new Option { IsCorrect = true, AnswerText = "False", QuestionId = 1 },
                new Option { IsCorrect = false, AnswerText = "True", QuestionId = 1 },
                new Option { IsCorrect = false, AnswerText = "cout << 'Hello World';", QuestionId = 2 },
                new Option { IsCorrect = false, AnswerText = "System.out.println('Hello World');", QuestionId = 2 },
                new Option { IsCorrect = true, AnswerText = "Console.WriteLine('Hello World');", QuestionId = 2 },
                new Option { IsCorrect = false, AnswerText = "public", QuestionId = 2 },
                new Option { IsCorrect = true, AnswerText = "friend", QuestionId = 3 },
                new Option { IsCorrect = false, AnswerText = "internal", QuestionId = 3 },
                new Option { IsCorrect = false, AnswerText = "private;", QuestionId = 3 },
                new Option { IsCorrect = false, AnswerText = "protected", QuestionId = 3 }
            );
            context.SaveChanges();
            // Insert data into the Quizzes table
            context.Quizzes.Add(new Quiz { Name = "PHP Quiz", Users = new User[] { user1, user2 } });
            context.SaveChanges();
            // Insert data into the Questions table
            context.Questions.AddRange(
                new Question { QuizId = 2, QuestionText = "How will you concatenate two strings?" },
                new Question { QuizId = 2, QuestionText = "Which of the following type of variables are instances of programmer-defined classes?" }
            );
            context.SaveChanges();
            // Insert data into the Options table
            context.Options.AddRange(
                new Option { IsCorrect = true, AnswerText = "Using . operator.", QuestionId = 4 },
                new Option { IsCorrect = false, AnswerText = "Using + operator.", QuestionId = 4 },
                new Option { IsCorrect = false, AnswerText = "Using add() function", QuestionId = 4 },
                new Option { IsCorrect = false, AnswerText = "Using append() function", QuestionId = 4 },
                new Option { IsCorrect = false, AnswerText = "Strings", QuestionId = 5 },
                new Option { IsCorrect = true, AnswerText = "Arrays", QuestionId = 5 },
                new Option { IsCorrect = false, AnswerText = "Objects", QuestionId = 5 },
                new Option { IsCorrect = false, AnswerText = "Resources", QuestionId = 5 }
            );
            context.SaveChanges();

            

            context.ChangeTracker.Clear();

            return context;
        }
    }
}
