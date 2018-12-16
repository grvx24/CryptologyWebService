using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.LfsrGenerators
{
    public interface IGenerators
    {
        byte[] GenerateBytes(int length);
        char[] GenerateBitsAsChars(int length);
    }
}
