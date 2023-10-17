using QuizApi.Data.Db.Enteties;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QuizApi.Models
{
    public class QuizResponsesRequest
    {
        [Required]
        public long TakeId { get; set; }      
        public long[] OptionIds { get; set; } = new long[0];
    }
}
