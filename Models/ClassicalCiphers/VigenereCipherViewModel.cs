using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Models.ClassicalCiphers
{
    public class VigenereCipherViewModel
    {
        public string Message { get; set; }
        public string Key { get; set; }
        public int AlphabetType { get; set; }
    }
}
