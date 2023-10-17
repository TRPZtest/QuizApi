namespace QuizApi.Models
{
    public class OptionsResponse
    {
        public List<OptionView> Options { get; set; }
    }

    public class OptionView
    {
        public long Id { get; set; }
        public string? AnswerText { get; set; }
    }
}
