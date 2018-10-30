using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CryptoWebService.Backend.HashFunctions.HMAC;

namespace CryptoWebService.Models.HashFunctions
{
    public class HMACViewModel
    {
        public string Message { get; set; }
        public string Key { get; set; }
        public HashFunctionType HashType { get; set; }
    }
}
