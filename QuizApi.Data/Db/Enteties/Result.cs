using System;
using System.Collections.Generic;

namespace QuizApi.Data.Db.Enteties;

public partial class Result
{
    public long Id { get; set; }

    public long TakeId { get; set; }

    public int Points { get; set; }

    public int MaxPoints { get; set; }

    public virtual Take Take { get; set; } = null!;
}
