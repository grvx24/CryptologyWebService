using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Models.Rsa
{
    public class RsaDecryptViewModel
    {
        public string[] Message { get; set; }
        public string EorD { get; set; }
        public string N { get; set; }
    }
}
