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

namespace CryptoWebService.Controllers
{
    public class VisualCryptographyController : Controller
    {
        #region VisualCryptography

        public IActionResult VisualCryptography()
        {
            return View();
        }

        public IActionResult VisualCryptographyMain()
        {
            return View(PrepareVisualCryptoraphyView());
        }

        private ViewModelDto PrepareVisualCryptoraphyView()
        {
            ViewModelDto VieModelDto = new ViewModelDto();

            List<AnimationDto> Animations = new List<AnimationDto>
            {
                new AnimationDto("/images/1.bmp", "/images/2.bmp")
                {
                    Amplitude = 100
                },
                new AnimationDto("/images/1.bmp", "/images/2.bmp")
                {
                    Amplitude = 150
                }
            };

            List<ImageDto> Images = new List<ImageDto>
            {
                new ImageDto("/images/SimpleMethod1.png")
            };
            VieModelDto.AnimationList = Animations;
            VieModelDto.ImageList = Images;

            return VieModelDto;
        }

        public IActionResult VisualCryptographyUploadFiles()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new { count = files.Count, size, filePath });
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
