using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Models.Quiz
{
    public class Answer
    {
        public string AnswerContent { get; }

        public bool IsCorrect { get; }

        public Answer(string answerContent, bool IsCorrect)
        {
            this.AnswerContent = answerContent;
            this.IsCorrect = IsCorrect;
        }
    }
}
