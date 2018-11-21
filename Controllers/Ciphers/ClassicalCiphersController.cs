using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CryptoWebService.Backend.ClassicalCiphers;
using CryptoWebService.Models.Ciphers;
using CryptoWebService.Models.ClassicalCiphers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;

namespace CryptoWebService.Controllers.Ciphers
{
    public class ClassicalCiphersController : Controller
    {
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

        [HttpGet]
        public IActionResult Bacon()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BaconEncrypt([FromBody]BaconCipherViewModel viewModel)
        {
            BaconCipher cipher = new BaconCipher();

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
        public IActionResult BaconDecrypt([FromBody]BaconCipherViewModel viewModel)
        {
            BaconCipher cipher = new BaconCipher();

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
        public IActionResult ColumnarTransposition()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ColumnarTranspositionEncrypt([FromBody]ColumnarTranspositionCipherViewModel viewModel)
        {
            ColumnarTranspositionCipher cipher = new ColumnarTranspositionCipher(viewModel.Key);

            string encrypted = "";

            try
            {
                encrypted=cipher.Encrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(encrypted);
        }

        [HttpPost]
        public IActionResult ColumnarTranspositionDecrypt([FromBody]ColumnarTranspositionCipherViewModel viewModel)
        {
            ColumnarTranspositionCipher cipher = new ColumnarTranspositionCipher(viewModel.Key);

            string decrypted = "";

            try
            {
                decrypted = cipher.Decrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(decrypted);
        }

        [HttpGet]
        public IActionResult Fence()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FenceEncrypt([FromBody]FenceCipherViewModel viewModel)
        {
            FenceCipher cipher = new FenceCipher(viewModel.Key);

            string encrypted = "";

            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(encrypted);
        }

        [HttpPost]
        public IActionResult FenceDecrypt([FromBody]FenceCipherViewModel viewModel)
        {
            FenceCipher cipher = new FenceCipher(viewModel.Key);

            string decrypted = "";

            try
            {
                decrypted = cipher.Decrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(decrypted);
        }

        [HttpGet]
        public IActionResult Playfair()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PlayfairEncrypt([FromBody]PlayfairCipherViewModel viewModel)
        {
            PlayfairCipher cipher = new PlayfairCipher(viewModel.Key);

            string encrypted = "";

            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(encrypted);
        }
        [HttpPost]
        public IActionResult PlayfairDecrypt([FromBody]PlayfairCipherViewModel viewModel)
        {
            PlayfairCipher cipher = new PlayfairCipher(viewModel.Key);

            string decrypted = "";

            try
            {
                decrypted = cipher.Decrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(decrypted);
        }

        [HttpGet]
        public IActionResult Route()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RouteEncrypt([FromBody]RouteEncryptViewModel viewModel)
        {
            RouteCipher cipher = new RouteCipher(viewModel.Key,(RouteCipher.CipherMode)viewModel.Mode);

            string encrypted = "";

            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(encrypted);
            
        }


        [HttpPost]
        public IActionResult RouteDecrypt([FromBody]RouteEncryptViewModel viewModel)
        {
            RouteCipher cipher = new RouteCipher(viewModel.Key, (RouteCipher.CipherMode)viewModel.Mode);

            string decrypted = "";

            try
            {
                decrypted = cipher.Decrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(decrypted);

        }

        [HttpGet]
        public IActionResult Vigenere()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VigenereEncrypt([FromBody]VigenereCipherViewModel viewModel)
        {
            VigenereCipher cipher = new VigenereCipher
                (viewModel.Key, Alphabets.GetAlphabet((Alphabets.AlphabetType) viewModel.AlphabetType));

            string encrypted = "";

            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(encrypted);
        }

        [HttpPost]
        public IActionResult VigenereDecrypt([FromBody]VigenereCipherViewModel viewModel)
        {
            VigenereCipher cipher = new VigenereCipher
                (viewModel.Key, Alphabets.GetAlphabet((Alphabets.AlphabetType)viewModel.AlphabetType));

            string decrypted = "";

            try
            {
                decrypted = cipher.Decrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(decrypted);
        }

    }
}