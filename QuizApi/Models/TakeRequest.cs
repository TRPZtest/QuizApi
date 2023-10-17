using System.ComponentModel.DataAnnotations;

namespace QuizApi.Models
{
    public class TakeRequest
    {
        [Required]
        public long QuizId { get; set; }
    }
}
