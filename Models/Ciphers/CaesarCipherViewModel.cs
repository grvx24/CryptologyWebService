using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoWebService.Backend.ClassicalCiphers;

namespace CryptoWebService.Models.Ciphers
{
    public class CaesarCipherViewModel
    {
        public string Message { get; set; }
        public int Key { get; set; }
        public int AlphabetType { get; set; }
    }
}
