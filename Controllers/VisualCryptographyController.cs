using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

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
        public HttpResponseMessage Secrets([FromBody] string imageDataWithoutHeader)
        {
            byte[] imageBytes = Convert.FromBase64String(imageDataWithoutHeader);
            MemoryStream ms = new MemoryStream(imageBytes, 0,imageBytes.Length);

            Bitmap bitmap = new Bitmap(ms);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(imageBytes);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

            return result;
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
