using QuizApi.Data.Db.Enteties;

namespace QuizApi.Models
{
    public class TakePostResponse
    {
        public long TakeId { get; set; }
        public Result Result { get; set; }
    }
}
