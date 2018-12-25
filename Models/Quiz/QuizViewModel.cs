using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Models.Quiz
{
    public class QuizViewModel
    {
        public QuestionViewModel[] Questions { get; }

        public string QuestionName { get; }

        public QuizViewModel(QuestionViewModel[] questions, string questionName)
        {
            Questions = questions;
            QuestionName = questionName;
        }
    }
}
