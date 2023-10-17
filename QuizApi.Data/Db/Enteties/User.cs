using System;
using System.Collections.Generic;

namespace QuizApi.Data.Db.Enteties;

public partial class User
{
    public long Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Take> Takes { get; set; } = new List<Take>();

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
}
