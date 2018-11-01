using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoWebService.Helpers
{
    public static class ViewControlsHelper
    {
        private static readonly string path = "~/ViewControls";

        public static String GetControlPath(String controlName)
        {
            return path + "/" + controlName + ".cshtml";
        }
    }
}