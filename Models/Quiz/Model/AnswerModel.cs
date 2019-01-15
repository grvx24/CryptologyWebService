namespace CryptoWebService.Models.Quiz.Model
{
    public class AnswerModel
    {
        public int AnswerId { get; set; }
        public string AnswerContent { get; set; }
        public bool IsCorrect { get; set; }
    }
}
