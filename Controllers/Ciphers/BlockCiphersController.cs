using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CryptoWebService.Backend.BlockCiphers;
using CryptoWebService.Helpers;
using CryptoWebService.Models.BlockCiphers;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWebService.Controllers.Ciphers
{
    public enum EncodingInformation
    {
        ASCII,
        HEX,
        BINARY_STRING,
        BASE64,
        INTEGER
    }

    public class BlockCiphersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AesEncrypt([FromBody]AesViewModel viewModel)
        {


            EncodingInformation encoding = (EncodingInformation)viewModel.Encoding;

            CustomAes aes = new CustomAes()
            {
                CipherMode = (BlockCipherMode)viewModel.Mode
            };

            
            byte[] key = Encoding.ASCII.GetBytes(viewModel.Key);
            byte[] iv = Encoding.ASCII.GetBytes(viewModel.IV);

            switch (encoding)
            {
                case EncodingInformation.ASCII:
                {
                    byte[] message = Encoding.ASCII.GetBytes(viewModel.Message);
                    var encrypted = aes.Encrypt(message, key, iv);
                    return Json(encrypted);
                    }

                case EncodingInformation.HEX:
                {
                    byte[] message = StringHelper.StringHexToByteArray(viewModel.Message);
                    var encrypted = aes.Encrypt(message, key, iv);
                    return Json(encrypted);
                }
                case EncodingInformation.BINARY_STRING:
                {
                    byte[] message = Convert.FromBase64String(viewModel.Message);
                    var encrypted = aes.Encrypt(message, key, iv);
                    return Json(encrypted);
                }
                case EncodingInformation.BASE64:
                {
                    return Json("");
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        [HttpPost]
        public IActionResult AesDecrypt([FromBody]AesViewModel viewModel)
        {
            byte[] message = Convert.FromBase64String(viewModel.Message);
            byte[] key = Encoding.ASCII.GetBytes(viewModel.Key);
            byte[] iv = Encoding.ASCII.GetBytes(viewModel.IV);

            EncodingInformation encoding = (EncodingInformation)viewModel.Encoding;

            CustomAes aes = new CustomAes()
            {
                CipherMode = (BlockCipherMode)viewModel.Mode
            };

            var decrypted = aes.Decrypt(message, key, iv);

            switch (encoding)
            {
                case EncodingInformation.ASCII:
                    break;
                case EncodingInformation.HEX:
                    break;
                case EncodingInformation.BINARY_STRING:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            return Json(decrypted);
        }
    }
}