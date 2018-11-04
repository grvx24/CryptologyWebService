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

namespace CryptoWebService.Controllers
{
    public class VisualCryptographyController : Controller
    {
        #region VisualCryptography

        public IActionResult VisualCryptography23554545()
        {
            return View();
        }


        public IActionResult VisualCryptographyAction()
        {
            return View();
        }
        

        public IActionResult VisualCryptography()
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
                    Width = 300,
                    Height = 200,
                    Amplitude = 2,
                    Period = 20000
                },
                new AnimationDto("/images/1.bmp", "/images/2.bmp")
                {
                    Amplitude = 150
                }
            };

            List<ImageDto> Images = new List<ImageDto>
            {
                new ImageDto("/images/SimpleMethodBlackPixel.png","Kodowanie czarnego piksela")
                {
                    Width = 793,
                    Height = 200
                },
                new ImageDto("/images/SimpleMethodWhitePixel.png","Kodowanie białęgo piksela")
                {
                    Width = 991,
                    Height = 250
                }
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
            if(imageDataWithoutHeader != null)
            {
                string[] lista = VisualCryptographyService.DivideStringImageToSecrets(imageDataWithoutHeader);

                Object secrets = JSONHelper.TransformArrayToJsonArray(lista);

                return Json(new { Result = true, secrets });
            }
            else
            {
                return Json(new { Result = false, Message = "The argument is empty." });
            }
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
