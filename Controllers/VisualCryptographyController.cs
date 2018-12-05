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
        public IActionResult sekret() => View("Secret", PrepareViewMenager.PrepareVisualCryptoraphyView());

        [HttpPost]
        public IActionResult Secrets([FromBody] SecretsDto secretsDto)
        {
            if (secretsDto == null || secretsDto.Image == null)
            {
                return Json(new { Result = false, Message = "ERROR - The ScretDto is empty." });

            }else if (secretsDto.Image == null)
            {
                return Json(new { Result = false, Message = "ERROR - The Image Data is empty." });
            }
            else
            {
                string[] lista;

                try
                {
                     lista = VisualCryptographyService.DivideStringImagesToSecrets(secretsDto);
                }
                catch (ImageIsNotInGrayScaleException)
                {

                    return Json(new { Result = false, Message = "Obraz nie jest czarno-biały." });
                }
             
                Object secrets = JSONHelper.TransformArrayToJsonArray(lista);

                return Json(new { Result = true, secrets });
            }
        }

        #endregion

        #region VisualSteganography

        public IActionResult steganografiawizualna() => View("VisualSteganography", PrepareViewMenager.PrepareVisualSteganographyView());

        [HttpPost]
        public IActionResult VisualSteganography([FromBody] string[] Images)
        {
            if (Images == null )
            {
                return Json(new { Result = false, Message = "Dane nie zostały przesłane."});
            }
            else
            {
                if (Images.Length != 3 || Images[0] == null|| Images[1] == null|| Images[2] == null)
                {
                    return Json(new { Result = false, Message = "Przesłano mniej niż 3 obrazy." });
                }

                string[] lista;

                try
                {
                    lista = VisualCryptographyService.VisualSteganography(Images);
                }
                catch (ImageIsNotInGrayScaleException )
                {

                    return Json(new { Result = false, Message = "Obraz/obrazy nie są czarno-białe." });
                }

                Object secrets = JSONHelper.TransformArrayToJsonArray(lista);

                return Json(new { Result = true, secrets });
            }
        }


        #endregion

        #region Steganography

        public IActionResult steganografia() => View("Steganography");

        [HttpPost]
        public IActionResult Steganography([FromBody] SteganographyDto steganographyData)
        {
            if (steganographyData == null)
            {
                return Json(new { Result = false, Message = "Dane nie zostały przesłane." });
            }
            else
            {
                try
                {
                    var image = VisualCryptographyService.Steganography(steganographyData);
                    return Json(new { Result = true, image });
                }
                catch (ImageIsNotInGrayScaleException )
                {

                    return Json(new { Result = false, Message = "Obraz/obrazy nie są czarno-białe." });
                }

            }
        }

        #endregion
    }
}
