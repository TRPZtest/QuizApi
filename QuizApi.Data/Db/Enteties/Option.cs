using System;
using System.Collections.Generic;

namespace QuizApi.Data.Db.Enteties;

public partial class Option
{
    public long Id { get; set; }

    public bool? IsCorrect { get; set; }

    public string? AnswerText { get; set; }

    public long QuestionId { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();
}
