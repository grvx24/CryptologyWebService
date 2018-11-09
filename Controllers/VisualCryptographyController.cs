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
    [Route("[action]")]
    public class VisualCryptographyController : Controller
    {
        #region VisualCryptography

        public IActionResult sekret()
        {
            return View("Secret", PrepareVisualCryptoraphyView());
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

        [HttpPost]
        public IActionResult Secrets([FromBody] SecretsDto secretsDto)
        {
            if (secretsDto.Image == null)
            {
                return Json(new { Result = false, Message = "The argument is empty." });
            }
            else
            {
                string[] lista = VisualCryptographyService.DivideStringImageToSecrets(secretsDto);

                Object secrets = JSONHelper.TransformArrayToJsonArray(lista);

                return Json(new { Result = true, secrets });
            }
        }


        public IActionResult VisualCryptographyAction() => View();

        public IActionResult VisualCryptography23554545() => View();

        #endregion

        #region Steganography

        public IActionResult steganografia() => View("Steganography");

        [HttpPost]
        public IActionResult Steganography(int result) => View();

        #endregion

        #region WaterMarks

        public IActionResult znakiwodne() => View("WaterMarks");

        [HttpPost]
        public IActionResult WaterMarks(int result) => View();

        #endregion
    }
}
