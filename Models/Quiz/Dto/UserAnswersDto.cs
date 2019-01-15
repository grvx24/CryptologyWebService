using System.Collections.Generic;

namespace CryptoWebService.Models.Quiz.Dto
{
    public class UserAnswersDto
    {
        public IEnumerable<UserAnswerDto> QuestionAnswers { get; set; }

        public int QuizId { get; set; }
    }
}
