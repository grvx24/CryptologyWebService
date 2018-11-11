using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.LfsrGenerators
{
    public class ShrinkingGenerator : LfsrGenerator
    {
        public readonly int NumberOfRegisters = 2;

        public ShrinkingGenerator()
        {
            Lfsr[] lfsr = new Lfsr[2];
            for (int i = 0; i < lfsr.Length; i++)
            {
                lfsr[i] = new Lfsr();
            }
            Registers = lfsr;
        }

        public ShrinkingGenerator(Lfsr[] registers)
        {
            if (registers.Length != 2)
            {
                throw new ArgumentException("Liczba rejestrów musi być równa 2!");
            }
            else
            {
                Registers = registers;
            }
        }
        protected override bool GenerateOneBit()
        {
            while (!Registers[0].GetOutputBit())
            {
                NextStep();
            }

            var result = Registers[1].GetOutputBit();
            return result;

        }
    }
}
