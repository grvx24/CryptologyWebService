using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace CryptoWebService.Models.Rsa
{
    public class RsaEncryptViewModel
    {
        public string Message { get; set; }
        public string EorD { get; set; }
        public string N { get; set; }
    }
}
