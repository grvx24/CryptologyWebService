using System.Collections.Generic;

namespace CryptoWebService.Models.Quiz
{
    public class UserAnswer
    {
        public int QuestionId { get; set; }

        public IEnumerable<int> SelectedAnswersId { get; set; }
    }
}
