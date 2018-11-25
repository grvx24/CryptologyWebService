using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoWebService.Backend.LfsrGenerators;

namespace CryptoWebService.Helpers
{
    public static class LfsrHelper
    {
        public static string[] GetRegisterAsStringArray(Lfsr[] registers)
        {
            string[] array = new string[registers.Length];

            for (int i = 0; i < registers.Length; i++)
            {
                array[i] = registers[i].ToString();
            }

            return array;
        }
    }
}
