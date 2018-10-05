using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CryptoWebService.Backend.ClassicalCiphers;
using CryptoWebService.Models.Ciphers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;

namespace CryptoWebService.Controllers.Ciphers
{
    public class ClassicalCiphersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Caesar()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult CaesarEncrypt([FromBody]CaesarCipherViewModel viewModel)
        {
            CaesarCipher cipher = new CaesarCipher(viewModel.Key)
            {
                Alphabet = Alphabets.GetAlphabet((Alphabets.AlphabetType) viewModel.AlphabetType)
            };
            cipher.Alphabet = Alphabets.GetAlphabet((Alphabets.AlphabetType) viewModel.AlphabetType);

            string encrypted = "";
            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
            }
            catch (NullReferenceException e)
            {

            }
            catch (Exception e)
            {
                return BadRequest(new {Result = false, Message = Text.InvalidCharacter});
            }
            return Json(encrypted);
        }

        [HttpPost]
        public IActionResult CaesarDecrypt([FromBody]CaesarCipherViewModel viewModel)
        {
            CaesarCipher cipher = new CaesarCipher(viewModel.Key)
            {
                Alphabet = Alphabets.GetAlphabet((Alphabets.AlphabetType) viewModel.AlphabetType)
            };

            string decrypted = "";
            try
            {
                decrypted = cipher.Decrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }
            return Json(decrypted);
        }

        [HttpGet]
        public IActionResult Affine()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AffineEncrypt([FromBody] AffineCipherViewModel viewModel)
        {
            AffineCipher cipher = new AffineCipher(viewModel.KeyA, viewModel.KeyB)
            {
                Alphabet = Alphabets.GetAlphabet((Alphabets.AlphabetType) viewModel.AlphabetType)
            };

            string encrypted = "";

            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }

            return Json(encrypted);
        }
        [HttpPost]
        public IActionResult AffineDecrypt([FromBody] AffineCipherViewModel viewModel)
        {
            AffineCipher cipher = new AffineCipher(viewModel.KeyA, viewModel.KeyB)
            {
                Alphabet = Alphabets.GetAlphabet((Alphabets.AlphabetType)viewModel.AlphabetType)
            };

            string decrypted = "";

            try
            {
                decrypted = cipher.Decrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }

            return Json(decrypted);
        }
    }
}