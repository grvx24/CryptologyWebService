using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.LfsrGenerators
{
    public class SelfDecimationGenerator : LfsrGenerator
    {
        public readonly int NumberOfRegisters = 1;
        public int K_value = 5;
        public int D_value = 3;

        public SelfDecimationGenerator()
        {
            Registers = new Lfsr[1] { new Lfsr() };
        }

        public SelfDecimationGenerator(Lfsr[] register)
        {
            if (register.Length != NumberOfRegisters)
            {
                throw new ArgumentException("Ten generator musi mieć  1 rejestr!");
            }

            Registers = register;
        }

        protected override bool GenerateOneBit()
        {
            //Registers[0].NextStep();
            bool output = Registers[0].GetOutputBit();

            if (output)
            {
                for (int i = 0; i < K_value; i++)
                {
                    Registers[0].NextStep();
                }
            }
            else
            {
                for (int i = 0; i < D_value; i++)
                {
                    Registers[0].NextStep();
                }
            }

            return output;
        }

        protected override void NextStep()
        {
            return;
        }
    }
}
