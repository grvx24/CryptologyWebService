using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CryptoWebService.Backend.HashFunctions.Keccak.Sha3HashAlgorithm;

namespace CryptoWebService.Models.HashFunctions
{
    public class SHA3ViewModel
    {
        public string Message { get; set; }
        public Size HashSize { get; set; }
    }
}
