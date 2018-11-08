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

        public IActionResult VisualCryptographyAction()
        {
            return View();
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


        public IActionResult VisualCryptography23554545()
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
