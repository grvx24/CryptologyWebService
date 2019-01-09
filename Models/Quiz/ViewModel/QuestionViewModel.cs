using System.Collections.Generic;

namespace CryptoWebService.Models.Quiz.ViewModel
{
    public class QuestionViewModel 
    {
        public int QuestionId { get; set; }
        public string QuestionContent { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
        public int Number { get; set; }
        public int AmountOfQuestionsInQuiz { get; set; }
    }
}
