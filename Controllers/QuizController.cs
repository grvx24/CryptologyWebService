using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CryptoWebService.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CryptoWebService.Controllers
{

    public class QuizController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuizController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Route("quiz/{category?}/{id?}")]
        public async Task<IActionResult> quiz(string category, int? id)
        {
            if (String.IsNullOrEmpty(category))
            {
                // widok wszystkich dostępnych kategorii
                //pobierz liste kategori
                // var Categories = new List<Category>
                //var qs = new QuizService(new ApplicationDbContext(this.con));

                var cat = _context.Category.ToList();

                return View("QuizzesCategory", cat);
            }
            else
            {
                if(id == null || id == 0)
                {
                    //widok listy quizow z danej kategorii
                    return View("QuizzesList");
                }
                else
                {
                    //widok quizu z danej kategori o danym id
                    return View("Quiz");
                }
            }
        }

    }
}
