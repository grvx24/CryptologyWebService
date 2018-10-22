using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Drawing;

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
        public IActionResult Secret([FromBody] string imageData)
        {
            byte[] imageBytes = Convert.FromBase64String(imageData);
            MemoryStream ms = new MemoryStream(imageBytes, 0,imageBytes.Length);

            ms.Write(imageBytes, 0, imageBytes.Length);

            var image = new Bitmap(System.Drawing.Image.FromFile(inputPath);

            //Bitmap bmpReturn = null;

            //string fileName = "MyUniqueImageFileName.png";
            //string fileNameWitPath = Path.Combine(Server.MapPath("~/FolderToSave"), fileName);

            //using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            //{
            //    using (BinaryWriter bw = new BinaryWriter(fs))
            //    {
            //        byte[] data = Convert.FromBase64String(imageData);
            //        bw.Write(data);
            //        bw.Close();
            //    }
            //    fs.Close();
            //}
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
