using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoWebService.Backend.ClassicalCiphers;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Caesar(string message, int key)
        {
            CaesarCipher cipher = new CaesarCipher(Alphabets.ALPHABET_EN,key);

            ViewData["EncryptedMessage"] = cipher.Encrypt(message);

            return View();
        }
    }
}