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
        BASE64
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
            var mode = int.Parse(viewModel.Mode);
            EncodingInformation keyEncoding = (EncodingInformation)int.Parse(viewModel.KeyEncoding);
            EncodingInformation IVEncoding = (EncodingInformation)int.Parse(viewModel.IVEncoding);
            EncodingInformation encoding = (EncodingInformation)int.Parse(viewModel.Encoding);

            CustomAes aes = new CustomAes()
            {
                CipherMode = (BlockCipherMode)mode
            };

            byte[] key = null;
            byte[] iv = null;

            switch (keyEncoding)
            {
                case EncodingInformation.HEX:
                    key=StringHelper.StringHexToByteArray(viewModel.Key.ToUpper());
                    break;
                case EncodingInformation.BINARY_STRING:
                    key = StringHelper.BinaryStringToBytes(viewModel.Key);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (IVEncoding)
            {
                case EncodingInformation.HEX:
                    iv = StringHelper.StringHexToByteArray(viewModel.IV.ToUpper());
                    break;
                case EncodingInformation.BINARY_STRING:
                    iv = StringHelper.BinaryStringToBytes(viewModel.IV);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }



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
                    byte[] message = StringHelper.BinaryStringToBytes(viewModel.Message);
                    var encrypted = aes.Encrypt(message, key, iv);
                    return Json(encrypted);
                }
                case EncodingInformation.BASE64:
                {
                    byte[] message = Convert.FromBase64String(viewModel.Message);
                    var encrypted = aes.Encrypt(message, key, iv);
                    return Json(encrypted);
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        [HttpPost]
        public IActionResult AesDecrypt([FromBody]AesViewModel viewModel)
        {
            var mode = int.Parse(viewModel.Mode);
            var messageEncoding = int.Parse(viewModel.Encoding);
            EncodingInformation keyEncoding = (EncodingInformation)int.Parse(viewModel.KeyEncoding);
            EncodingInformation IVEncoding = (EncodingInformation)int.Parse(viewModel.IVEncoding);

            byte[] message = Convert.FromBase64String(viewModel.Message);

            byte[] key = null;
            byte[] iv = null;

            switch (keyEncoding)
            {
                case EncodingInformation.HEX:
                    key = StringHelper.StringHexToByteArray(viewModel.Key);
                    break;
                case EncodingInformation.BINARY_STRING:
                    key = StringHelper.BinaryStringToBytes(viewModel.Key);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (IVEncoding)
            {
                case EncodingInformation.HEX:
                    iv = StringHelper.StringHexToByteArray(viewModel.IV);
                    break;
                case EncodingInformation.BINARY_STRING:
                    iv = StringHelper.BinaryStringToBytes(viewModel.IV);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            CustomAes aes = new CustomAes(viewModel.Padding)
            {
                CipherMode = (BlockCipherMode)mode
            };

            var decrypted = aes.Decrypt(message, key, iv);
            
            return Json(decrypted);
        }
    }
}