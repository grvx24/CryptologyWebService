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
        #region Secrets
        public IActionResult sekret() => View("Secret", VisualCryptographyService.PrepareVisualCryptoraphyView());

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
