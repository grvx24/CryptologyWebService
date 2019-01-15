using System;
using System.Collections.Generic;

namespace CryptoWebService.Data
{
    public partial class Question
    {
        public Question()
        {
            Answer = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public int QuizId { get; set; }

        public Quiz Quiz { get; set; }
        public ICollection<Answer> Answer { get; set; }
    }
}
