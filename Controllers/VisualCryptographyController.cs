using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.IO;

namespace CryptoWebService.Controllers
{
    public class VisualCryptographyController : Controller
    {
        #region Secret

        public IActionResult Secret()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Secret([FromBody] string imageDataWithoutHeader)
        {
            
            byte[] imageBytes = Convert.FromBase64String(imageDataWithoutHeader);
            MemoryStream ms = new MemoryStream(imageBytes, 0,imageBytes.Length);

            Bitmap bitmap = new Bitmap(ms);

            return View();
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
