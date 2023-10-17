using System;
using System.Collections.Generic;

namespace QuizApi.Data.Db.Enteties;

public partial class Take
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long QuizId { get; set; }

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();

    public virtual Result? Result { get; set; }

    public virtual User User { get; set; } = null!;
}
