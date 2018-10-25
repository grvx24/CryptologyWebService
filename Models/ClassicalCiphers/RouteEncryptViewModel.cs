using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Models.ClassicalCiphers
{
    public class RouteEncryptViewModel
    {
        public string Message { get; set; }
        public int Key { get; set; }
        public int Mode { get; set; }
    }
}
