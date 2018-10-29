using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.IO;
using CryptoWebService.Backend.VisualCryptography;

namespace CryptoWebService.Controllers
{
    public class VisualCryptographyController : Controller
    {
        #region VisualCryptography

        public IActionResult VisualCryptography()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Secrets([FromBody] string imageDataWithoutHeader)
        {
            string[] lista = VisualCryptographyService.DivideStringImageToSecrets(imageDataWithoutHeader);

            JArray secretList = new JArray();
            for (int i = 0; i < lista.Length; i++)
            {
                secretList.Add(new JObject(
                    new JProperty ("value", lista[i])
                    ));
            }
           
            Object rss =new JObject(new JProperty("secrets", secretList));

            return Json(rss);
        }


        #endregion

        #region WaterMarks

        public IActionResult WaterMarks()
        {
            return View();
        }

        [HttpPost]
        public IActionResult WaterMarks(int result)
        {
            return View();
        }

        #endregion
    }
}
