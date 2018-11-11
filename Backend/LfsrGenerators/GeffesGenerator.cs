using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.LfsrGenerators
{
    public class GeffesGenerator : LfsrGenerator
    {
        public readonly int NumberOfRegisters = 3;

        //Generates registers with default length (8)
        public GeffesGenerator()
        {
            Lfsr[] lfsr = new Lfsr[3];
            for (int i = 0; i < lfsr.Length; i++)
            {
                lfsr[i] = new Lfsr();
            }
            Registers = lfsr;

        }

        public GeffesGenerator(Lfsr[] registers) : base(registers)
        {
            if (registers.Length != NumberOfRegisters)
            {
                throw new ArgumentException("Ten generator używa 3 rejestrów!");
            }

        }


        protected override bool GenerateOneBit()
        {
            bool[] a = { Registers[0].GetOutputBit(), Registers[1].GetOutputBit(), Registers[2].GetOutputBit() };
            bool result = (a[2] & a[0]) | ((!a[0]) & a[1]);

            return result;
        }
    }
}
