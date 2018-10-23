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

namespace CryptoWebService.Controllers
{
    public class HashFunctionsController : Controller
    {
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
        public IActionResult Bacon()
        {
            return View();
        }
    }
}