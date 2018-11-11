using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.LfsrGenerators
{
    public class StopAndGoGenerator : LfsrGenerator
    {
        public readonly int NumberOfRegisters = 3;

        public StopAndGoGenerator()
        {
            Lfsr[] lfsrs = new Lfsr[NumberOfRegisters];
            for (int i = 0; i < lfsrs.Length; i++)
            {
                lfsrs[i] = new Lfsr();
            }
            Registers = lfsrs;

        }


        public StopAndGoGenerator(Lfsr[] registers) : base(registers)
        {
            if (registers.Length != 3)
            {
                throw new ArgumentException("Liczba rejestrów musi być równa 3!");
            }

        }

        protected override void NextStep()
        {
            return;
        }

        protected override bool GenerateOneBit()
        {

            bool output1 = false;
            bool output2 = false;

            if (Registers[0].GetOutputBit() == true)
            {
                output1 = Registers[1].GetOutputBit();
                Registers[1].NextStep();
            }
            else
            {
                output2 = Registers[2].GetOutputBit();
                Registers[2].NextStep();
            }
            Registers[0].NextStep();
            bool mainOutput = output1 ^ output2;
            return mainOutput;
        }
    }
}
