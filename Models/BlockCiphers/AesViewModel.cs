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
        public int Mode { get; set; }
        public int Encoding { get; set; }
        public int KeyEncoding { get; set; }
        public int IVEncoding { get; set; }

    }
}
