using System.Collections.Generic;

namespace CryptoWebService.Models.Quiz
{
    public class QuestionViewModel 
    {
        public string QuestionContent { get; set; }

        public List<AnswerViewModel> Answers { get; set; }

        public int Number { get; set; }

        public int AmountOfQuestionsInQuiz { get; set; }
    }
}
