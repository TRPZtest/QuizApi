using System;
using System.Collections.Generic;

namespace QuizApi.Data.Db.Enteties;

public partial class Quiz
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Take> Takes { get; set; } = new List<Take>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
