using System.Collections.Generic;

namespace CryptoWebService.Models.Quiz.Model
{
    public class QuestionModel
    {
        public int QuizId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionContent { get; set; }
        public List<AnswerModel> Answers {get;set;}
    }
}
