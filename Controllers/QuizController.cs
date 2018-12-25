using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.IO;
using CryptoWebService.Backend.VisualCryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using CryptoWebService.Models.VisualCryptography;
using CryptoWebService.Models;
using CryptoWebService.Helpers;
using CryptoWebService.Data;

namespace CryptoWebService.Controllers
{

    public class QuizController : Controller
    {
        #region Secrets
        [Route("quiz/{category?}/{id?}")]
        public IActionResult quiz(string category, int? id)
        {
            if (String.IsNullOrEmpty(category))
            {
                // widok wszystkich dostępnych kategorii
                //pobierz liste kategori
                // var Categories = new List<Category>

                return View("QuizzesCategory");
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
        #endregion

    }
}
