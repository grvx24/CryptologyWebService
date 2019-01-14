using System.Collections.Generic;

namespace CryptoWebService.Models.Quiz.ViewModel
{
    public class QuizViewModel
    {
        public int QuizId { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
        public string QuizName { get; set; }
        public int AmountOfQuestions { get; set; }
        public bool CanEdit { get; set; }
    }
}
