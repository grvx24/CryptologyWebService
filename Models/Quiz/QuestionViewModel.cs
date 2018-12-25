using System;

namespace CryptoWebService.Models.Quiz
{
    public class QuestionViewModel 
    {
        public string QuestionContent { get;  }

        public Answer[] Answers { get; }

        public int Number { get; }

        public QuestionViewModel(string questionContent, Answer[] answers,int number)
        {
            this.QuestionContent = QuestionContent;
            this.Answers = answers;
            this.Number = number;
        }
    }
}
