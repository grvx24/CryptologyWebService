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
            CaesarCipher cipher = new CaesarCipher(Alphabets.ALPHABET_EN,viewModel.Key);
            switch ((Alphabets.AlphabetType)viewModel.AlphabetType)
            {
                case Alphabets.AlphabetType.EN:
                    cipher.Alphabet = Alphabets.ALPHABET_EN;
                    break;
                case Alphabets.AlphabetType.PL:
                    cipher.Alphabet = Alphabets.ALPHABET_PL;
                    break;
                case Alphabets.AlphabetType.EN_Digits:
                    cipher.Alphabet = Alphabets.ALPHABET_EN_DIGITS;
                    break;
                case Alphabets.AlphabetType.PL_Digits:
                    cipher.Alphabet = Alphabets.ALPHABET_PL_DIGITS;
                    break;
                case Alphabets.AlphabetType.EN_Digits_Extended:
                    cipher.Alphabet = Alphabets.ALPHABET_EN_EXTENDED;
                    break;
                case Alphabets.AlphabetType.PL_Digits_Extended:
                    cipher.Alphabet = Alphabets.ALPHABET_PL_EXTENDED;
                    break;
            }
            

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
            CaesarCipher cipher = new CaesarCipher(Alphabets.ALPHABET_EN, viewModel.Key);
            switch ((Alphabets.AlphabetType)viewModel.AlphabetType)
            {
                case Alphabets.AlphabetType.EN:
                    cipher.Alphabet = Alphabets.ALPHABET_EN;
                    break;
                case Alphabets.AlphabetType.PL:
                    cipher.Alphabet = Alphabets.ALPHABET_PL;
                    break;
                case Alphabets.AlphabetType.EN_Digits:
                    cipher.Alphabet = Alphabets.ALPHABET_EN_DIGITS;
                    break;
                case Alphabets.AlphabetType.PL_Digits:
                    cipher.Alphabet = Alphabets.ALPHABET_PL_DIGITS;
                    break;
                case Alphabets.AlphabetType.EN_Digits_Extended:
                    cipher.Alphabet = Alphabets.ALPHABET_EN_EXTENDED;
                    break;
                case Alphabets.AlphabetType.PL_Digits_Extended:
                    cipher.Alphabet = Alphabets.ALPHABET_PL_EXTENDED;
                    break;
            }

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