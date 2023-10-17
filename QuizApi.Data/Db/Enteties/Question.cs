using System;
using System.Collections.Generic;

namespace QuizApi.Data.Db.Enteties;

public partial class Question
{
    public long Id { get; set; }

    public long QuizId { get; set; }

    public string QuestionText { get; set; } = null!;

    public virtual ICollection<Option> Options { get; set; } = new List<Option>();

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();
}
