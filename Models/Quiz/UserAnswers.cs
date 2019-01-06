using System.Collections.Generic;

namespace CryptoWebService.Models.Quiz
{
    public class UserAnswers
    {
        public IEnumerable<UserAnswer> QuestionAnswers { get; set; }

        public int QuizId { get; set; }
    }
}
