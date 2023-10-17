using System;
using System.Collections.Generic;

namespace QuizApi.Data.Db.Enteties;

public partial class Response
{
    public long Id { get; set; }

    public long TakeId { get; set; }

    public long OptionId { get; set; }

    public DateTime Created { get; set; }

    public virtual Option Option { get; set; } = null!;

    public virtual Take Take { get; set; } = null!;
}
