using System;
using System.Collections.Generic;

namespace CryptoWebService.Data
{
    public partial class Quiz
    {
        public Quiz()
        {
            Question = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string QuizName { get; set; }
        public int QuizNumber { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<Question> Question { get; set; }
    }
}
