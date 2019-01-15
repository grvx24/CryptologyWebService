using System;
using System.Collections.Generic;

namespace CryptoWebService.Data
{
    public partial class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool Correct { get; set; }
        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
