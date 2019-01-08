using CryptoWebService.Models.Quiz.Dto;
using System.Collections.Generic;

namespace CryptoWebService.Models.Quiz.ViewModel
{
    public class QuizzesListViewModel
    {
        public IEnumerable<QuizDto> Quizzes { get; set; }

        public string CategoryName { get; set; }

        public string SecondCategoryName { get; set; }
    }
}
