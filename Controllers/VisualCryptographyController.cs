using Microsoft.AspNetCore.Mvc;

namespace CryptoWebService.Controllers
{
    public class VisualCryptographyController : Controller
    {
        #region Secret

        public IActionResult Secret()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Secret(int result)
        {
            return View();
        }

        #endregion

        #region WaterMarks

        public IActionResult WaterMarks()
        {
            return View();
        }

        [HttpPost]
        public IActionResult WaterMarks(int result)
        {
            return View();
        }

        #endregion
    }
}
