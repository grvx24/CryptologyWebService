using System;
using System.Numerics;
using System.Text;
using CryptoWebService.Backend.AsymmetricCryptography;
using CryptoWebService.Models.Rsa;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWebService.Controllers
{
    public class AsymmetricCryptographyController : Controller
    {
        public IActionResult RSA()
        {
            return View();
        }


        public IActionResult RsaGenerateKeys([FromBody]RsaKeyViewModel viewModel)
        {
            BasicRsa rsa = new BasicRsa();
            rsa.GenerateKey(viewModel.P,viewModel.Q);

            string publicKey = rsa.N+","+rsa.E;
            string privateKey = rsa.N+","+rsa.D;


            return Json(new {publicKey = publicKey, privateKey=privateKey});
        }

        public IActionResult Encrypt([FromBody]RsaEncryptViewModel viewModel)
        {
            BasicRsa rsa = new BasicRsa();
            BigInteger E = BigInteger.Parse(viewModel.EorD);
            BigInteger N = BigInteger.Parse(viewModel.N);

            string[] encrypted = new string[viewModel.Message.Length];

            for (int i = 0; i < viewModel.Message.Length; i++)
            {
                var character = viewModel.Message[i];
                encrypted[i]= rsa.Encrypt(character, E, N).ToString();
            }
            var result = String.Join(',', encrypted);

            return Json(result);
        }

        public IActionResult Decrypt([FromBody]RsaDecryptViewModel viewModel)
        {
            BasicRsa rsa = new BasicRsa();
            BigInteger E = BigInteger.Parse(viewModel.EorD);
            BigInteger N = BigInteger.Parse(viewModel.N);

            try
            {
                StringBuilder sb = new StringBuilder(viewModel.Message.Length);
                for (int i = 0; i < viewModel.Message.Length; i++)
                {
                    var value = rsa.Decrypt(BigInteger.Parse(viewModel.Message[i]), E, N);
                    sb.Append(Convert.ToChar(value));
                }


                return Json(sb.ToString());
            }
            catch (Exception)
            {
                return BadRequest(new { Result = false, Message = "Liczby pierwsze nie mogą być takie same!" });
            }

        }


    }
}