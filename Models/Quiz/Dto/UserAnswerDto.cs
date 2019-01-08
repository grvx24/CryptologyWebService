using System.Collections.Generic;

namespace CryptoWebService.Models.Quiz.Dto
{
    public class UserAnswerDto
    {
        public int QuestionId { get; set; }

        public IEnumerable<int> SelectedAnswersId { get; set; }
    }
}
