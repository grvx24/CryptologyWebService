using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CryptoWebService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace CryptoWebService.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public FileResult DownloadDesktop()
        {
            string rootDir = _hostingEnvironment.WebRootPath;
            string filedir = "/desktop/KryptoKombajnDesktop.zip";
            string filename = "KryptoKombajnDesktop.zip";
            FileInfo file = new FileInfo(Path.Combine(rootDir, filedir));

            byte[] fileBytes = System.IO.File.ReadAllBytes(rootDir+ filedir);
            return File(fileBytes, "application/x-msdownload", filename);
            
        }
    }
}
