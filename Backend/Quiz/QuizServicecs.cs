using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.Quiz
{
    public class QuizServicecs
    {
        DbContext dbContext;

        public QuizServicecs(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}
