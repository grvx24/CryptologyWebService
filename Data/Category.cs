using System;
using System.Collections.Generic;

namespace CryptoWebService.Data
{
    public partial class Category
    {
        public Category()
        {
            Quiz = new HashSet<Quiz>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Quiz> Quiz { get; set; }
    }
}
