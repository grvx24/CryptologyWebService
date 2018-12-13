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
            catch (NullReferenceException )
            {

            }
            catch (Exception )
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
            catch (NullReferenceException )
            {

            }
            catch (Exception )
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
            catch (NullReferenceException )
            {

            }
            catch (Exception )
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
            catch (NullReferenceException )
            {

            }
            catch (Exception )
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
                }

                int messageLength = md5_viso.Get_MessageLength(message);
                int paddedBits = md5_viso.Get_PaddedBits(message);
                int messageLength2 = messageLength + paddedBits;
                int modResult = messageLength2 % 512;

                results[0] = messageLength.ToString();
                results[1] = paddedBits.ToString();
                results[2] = messageLength2.ToString();
                results[3] = modResult.ToString();
                results[4] = (paddedBits - 1).ToString();
                results[5] = md5_viso.Get_binary_length(message);
                results[6] = md5_viso.Get_binary(message);
                results[7] = md5_viso.StringToBinary(message);
            }   
            catch (Exception )
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }
            return Json(results);
        }




        [HttpPost]
        public IActionResult MD5_Table([FromBody]MD5ViewModel viewModel)
        {

            MD5_Visualization md5 = new MD5_Visualization();
            string json = "";
            List<Registers> _registers = new List<Registers>();
            try
            {              
                string message = viewModel.Message;
                if (message.Length * 8 < 448)
                {
                    md5 = new MD5_Visualization(message);

                    
                }

                //List<Registers> _registers = new List<Registers>();

                /*Round 1 Function F*/
                MD5_Visualization.FF(ref md5.A, md5.B, md5.C, md5.D, 0, 7, 1, md5.X, _registers);
                MD5_Visualization.FF(ref md5.D, md5.A, md5.B, md5.C, 1, 12, 2, md5.X, _registers);
                MD5_Visualization.FF(ref md5.C, md5.D, md5.A, md5.B, 2, 17, 3, md5.X, _registers);
                MD5_Visualization.FF(ref md5.B, md5.C, md5.D, md5.A, 3, 22, 4, md5.X, _registers);
                MD5_Visualization.FF(ref md5.A, md5.B, md5.C, md5.D, 4, 7, 5, md5.X, _registers);
                MD5_Visualization.FF(ref md5.D, md5.A, md5.B, md5.C, 5, 12, 6, md5.X, _registers);
                MD5_Visualization.FF(ref md5.C, md5.D, md5.A, md5.B, 6, 17, 7, md5.X, _registers);
                MD5_Visualization.FF(ref md5.B, md5.C, md5.D, md5.A, 7, 22, 8, md5.X, _registers);
                MD5_Visualization.FF(ref md5.A, md5.B, md5.C, md5.D, 8, 7, 9, md5.X, _registers);
                MD5_Visualization.FF(ref md5.D, md5.A, md5.B, md5.C, 9, 12, 10, md5.X, _registers);
                MD5_Visualization.FF(ref md5.C, md5.D, md5.A, md5.B, 10, 17, 11, md5.X, _registers);
                MD5_Visualization.FF(ref md5.B, md5.C, md5.D, md5.A, 11, 22, 12, md5.X, _registers);
                MD5_Visualization.FF(ref md5.A, md5.B, md5.C, md5.D, 12, 7, 13, md5.X, _registers);
                MD5_Visualization.FF(ref md5.D, md5.A, md5.B, md5.C, 13, 12, 14, md5.X, _registers);
                MD5_Visualization.FF(ref md5.C, md5.D, md5.A, md5.B, 14, 17, 15, md5.X, _registers);
                MD5_Visualization.FF(ref md5.B, md5.C, md5.D, md5.A, 15, 22, 16, md5.X, _registers);


                /*Round 2 Function G*/
                MD5_Visualization.GG(ref md5.A, md5.B, md5.C, md5.D, 1, 5, 17, md5.X, _registers);
                MD5_Visualization.GG(ref md5.D, md5.A, md5.B, md5.C, 6, 9, 18, md5.X, _registers);
                MD5_Visualization.GG(ref md5.C, md5.D, md5.A, md5.B, 11, 14, 19, md5.X, _registers);
                MD5_Visualization.GG(ref md5.B, md5.C, md5.D, md5.A, 0, 20, 20, md5.X, _registers);
                MD5_Visualization.GG(ref md5.A, md5.B, md5.C, md5.D, 5, 5, 21, md5.X, _registers);
                MD5_Visualization.GG(ref md5.D, md5.A, md5.B, md5.C, 10, 9, 22, md5.X, _registers);
                MD5_Visualization.GG(ref md5.C, md5.D, md5.A, md5.B, 15, 14, 23, md5.X, _registers);
                MD5_Visualization.GG(ref md5.B, md5.C, md5.D, md5.A, 4, 20, 24, md5.X, _registers);
                MD5_Visualization.GG(ref md5.A, md5.B, md5.C, md5.D, 9, 5, 25, md5.X, _registers);
                MD5_Visualization.GG(ref md5.D, md5.A, md5.B, md5.C, 14, 9, 26, md5.X, _registers);
                MD5_Visualization.GG(ref md5.C, md5.D, md5.A, md5.B, 3, 14, 27, md5.X, _registers);
                MD5_Visualization.GG(ref md5.B, md5.C, md5.D, md5.A, 8, 20, 28, md5.X, _registers);
                MD5_Visualization.GG(ref md5.A, md5.B, md5.C, md5.D, 13, 5, 29, md5.X, _registers);
                MD5_Visualization.GG(ref md5.D, md5.A, md5.B, md5.C, 2, 9, 30, md5.X, _registers);
                MD5_Visualization.GG(ref md5.C, md5.D, md5.A, md5.B, 7, 14, 31, md5.X, _registers);
                MD5_Visualization.GG(ref md5.B, md5.C, md5.D, md5.A, 12, 20, 32, md5.X, _registers);


                /*Round 3 Function H*/
                MD5_Visualization.HH(ref md5.A, md5.B, md5.C, md5.D, 5, 4, 33, md5.X, _registers);
                MD5_Visualization.HH(ref md5.D, md5.A, md5.B, md5.C, 8, 11, 34, md5.X, _registers);
                MD5_Visualization.HH(ref md5.C, md5.D, md5.A, md5.B, 11, 16, 35, md5.X, _registers);
                MD5_Visualization.HH(ref md5.B, md5.C, md5.D, md5.A, 14, 23, 36, md5.X, _registers);
                MD5_Visualization.HH(ref md5.A, md5.B, md5.C, md5.D, 1, 4, 37, md5.X, _registers);
                MD5_Visualization.HH(ref md5.D, md5.A, md5.B, md5.C, 4, 11, 38, md5.X, _registers);
                MD5_Visualization.HH(ref md5.C, md5.D, md5.A, md5.B, 7, 16, 39, md5.X, _registers);
                MD5_Visualization.HH(ref md5.B, md5.C, md5.D, md5.A, 10, 23, 40, md5.X, _registers);
                MD5_Visualization.HH(ref md5.A, md5.B, md5.C, md5.D, 13, 4, 41, md5.X, _registers);
                MD5_Visualization.HH(ref md5.D, md5.A, md5.B, md5.C, 0, 11, 42, md5.X, _registers);
                MD5_Visualization.HH(ref md5.C, md5.D, md5.A, md5.B, 3, 16, 43, md5.X, _registers);
                MD5_Visualization.HH(ref md5.B, md5.C, md5.D, md5.A, 6, 23, 44, md5.X, _registers);
                MD5_Visualization.HH(ref md5.A, md5.B, md5.C, md5.D, 9, 4, 45, md5.X, _registers);
                MD5_Visualization.HH(ref md5.D, md5.A, md5.B, md5.C, 12, 11, 46, md5.X, _registers);
                MD5_Visualization.HH(ref md5.C, md5.D, md5.A, md5.B, 15, 16, 47, md5.X, _registers);
                MD5_Visualization.HH(ref md5.B, md5.C, md5.D, md5.A, 2, 23, 48, md5.X, _registers);


                /*Round 4 Function I*/
                MD5_Visualization.II(ref md5.A, md5.B, md5.C, md5.D, 0, 6, 49, md5.X, _registers);
                MD5_Visualization.II(ref md5.D, md5.A, md5.B, md5.C, 7, 10, 50, md5.X, _registers);
                MD5_Visualization.II(ref md5.C, md5.D, md5.A, md5.B, 14, 15, 51, md5.X, _registers);
                MD5_Visualization.II(ref md5.B, md5.C, md5.D, md5.A, 5, 21, 52, md5.X, _registers);
                MD5_Visualization.II(ref md5.A, md5.B, md5.C, md5.D, 12, 6, 53, md5.X, _registers);
                MD5_Visualization.II(ref md5.D, md5.A, md5.B, md5.C, 3, 10, 54, md5.X, _registers);
                MD5_Visualization.II(ref md5.C, md5.D, md5.A, md5.B, 10, 15, 55, md5.X, _registers);
                MD5_Visualization.II(ref md5.B, md5.C, md5.D, md5.A, 1, 21, 56, md5.X, _registers);
                MD5_Visualization.II(ref md5.A, md5.B, md5.C, md5.D, 8, 6, 57, md5.X, _registers);
                MD5_Visualization.II(ref md5.D, md5.A, md5.B, md5.C, 15, 10, 58, md5.X, _registers);
                MD5_Visualization.II(ref md5.C, md5.D, md5.A, md5.B, 6, 15, 59, md5.X, _registers);
                MD5_Visualization.II(ref md5.B, md5.C, md5.D, md5.A, 13, 21, 60, md5.X, _registers);
                MD5_Visualization.II(ref md5.A, md5.B, md5.C, md5.D, 4, 6, 61, md5.X, _registers);
                MD5_Visualization.II(ref md5.D, md5.A, md5.B, md5.C, 11, 10, 62, md5.X, _registers);
                MD5_Visualization.II(ref md5.C, md5.D, md5.A, md5.B, 2, 15, 63, md5.X, _registers);
                MD5_Visualization.II(ref md5.B, md5.C, md5.D, md5.A, 9, 21, 64, md5.X, _registers);

                // serialize JSON to a string and then write string to a file
              //json = JsonConvert.SerializeObject(_registers, Formatting.Indented);

            }
            catch (Exception )
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }
            //return Json(_registers);
            return Json(_registers.ToArray());
        }



    }
}