using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CryptoWebService.Backend.HashFunctions;
using CryptoWebService.Models.HashFunctions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;
using static CryptoWebService.Backend.HashFunctions.HMAC;

namespace CryptoWebService.Controllers
{
    public class HashFunctionsController : Controller
    {
        public MD5_Visualization MD5_Visualization { get; private set; }

        [HttpGet]
        public IActionResult MD5()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MD5Encrypt([FromBody]MD5ViewModel viewModel)
        {
            MD5Hash hash = new MD5Hash();
           

            string encrypted = "";
            try
            {
                encrypted = hash.Encrypt(viewModel.Message);
            }
            catch (NullReferenceException e)
            {

            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }
            return Json(encrypted);
        }


        [HttpGet]
        public IActionResult SHA1()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SHA1Encrypt([FromBody]SHA1ViewModel viewModel)
        {
            SHA1Hash hash = new SHA1Hash();


            string encrypted = "";
            try
            {
                encrypted = hash.Encrypt(viewModel.Message);
            }
            catch (NullReferenceException e)
            {

            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }
            return Json(encrypted);
        }



        [HttpGet]
        public IActionResult SHA256()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SHA256Encrypt([FromBody]SHA1ViewModel viewModel)
        {
            SHA256Hash hash = new SHA256Hash();


            string encrypted = "";
            try
            {
                encrypted = hash.Encrypt(viewModel.Message);
            }
            catch (NullReferenceException e)
            {

            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }
            return Json(encrypted);
        }


        [HttpGet]
        public IActionResult SHA512()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SHA512Encrypt([FromBody]SHA1ViewModel viewModel)
        {
            SHA512Hash hash = new SHA512Hash();


            string encrypted = "";
            try
            {
                encrypted = hash.Encrypt(viewModel.Message);
            }
            catch (NullReferenceException e)
            {

            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }
            return Json(encrypted);
        }

        [HttpGet]
        public IActionResult HMAC()
        {
            return View();
        }

        [HttpPost]
        public IActionResult HMACEncrypt([FromBody]HMACViewModel viewModel)
        {
            HMAC hmac = new HMAC(viewModel.HashType);

            string encrypted = "";

            try
            {
                encrypted = hmac.Encrypt(viewModel.Message,viewModel.Key);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(encrypted);

        }

        [HttpGet]
        public IActionResult HashFunction()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MDC()
        { 
            return View();
        }



        [HttpPost]
        public IActionResult MD5_Padding([FromBody]MD5ViewModel viewModel)
        {

            MD5_Visualization md5_viso = new MD5_Visualization();
            string[] results = new string[8] { "nie_działa", "nie_działa ", "nie_działa ", "nie_działa", "nie działa", "nie działa", "nie działa" , "nie działa"};

            try
            {
                //string message = viewModel.Message_visualization;
                string message = viewModel.Message;
                //string message = "Hello World!";
                if (message.Length * 8 < 448)
                {
                    md5_viso = new MD5_Visualization(message);

                    //BUTTON SHOW ROUNDS ->enable
                }

                int messageLength = md5_viso.Get_MessageLength(message);
                int paddedBits = md5_viso.Get_PaddedBits(message);
                int messageLength2 = messageLength + paddedBits;
                int modResult = messageLength2 % 512;

                // results[1] = "Dupa blada.";
                results[0] = messageLength.ToString();
                results[1] = paddedBits.ToString();
                results[2] = messageLength2.ToString();
                results[3] = modResult.ToString();
                results[4] = (paddedBits - 1).ToString();
                results[5] = md5_viso.Get_binary_length(message);
                results[6] = md5_viso.Get_binary(message);
                results[7] = md5_viso.StringToBinary(message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }
            return Json(results);
        }




    }
}