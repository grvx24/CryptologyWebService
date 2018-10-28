using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoWebService.Backend.BlockCiphers;

namespace CryptoWebService.Models.BlockCiphers
{

    public class AesViewModel
    {
        public string Message { get; set; }
        public string Key { get; set; }
        public string IV { get; set; }
        public string Mode { get; set; }
        public string Encoding { get; set; }
        public string KeyEncoding { get; set; }
        public string IVEncoding { get; set; }
        public bool Padding { get; set; }

    }
}
