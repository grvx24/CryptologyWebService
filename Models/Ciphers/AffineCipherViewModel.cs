using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Models.Ciphers
{
    public class AffineCipherViewModel
    {
        public string Message { get; set; }
        public int KeyA { get; set; }
        public int KeyB { get; set; }
        public int AlphabetType { get; set; }
    }
}
