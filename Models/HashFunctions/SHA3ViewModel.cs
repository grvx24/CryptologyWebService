using CryptoWebService.Backend.HashFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CryptoWebService.Backend.HashFunctions.SHA3Hash;

namespace CryptoWebService.Models.HashFunctions
{
    public class SHA3ViewModel
    {
        public string Message { get; set; }
        public SHA3Hash.HashFunctionSize HashSize { get; set; }
    }
}
