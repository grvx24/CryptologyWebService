using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.LfsrGenerators
{
    public class CascadeGenerator : LfsrGenerator
    {
        public CascadeGenerator(int numOfLfsr)
        {
            Lfsr[] lfsr = new Lfsr[numOfLfsr];
            for (int i = 0; i < lfsr.Length; i++)
            {
                lfsr[i] = new Lfsr();
            }

            Registers = lfsr;
        }

        public CascadeGenerator(Lfsr[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("Pole register nie może mieć wartośći null!");
            }
            else
            {
                Registers = registers;
            }
        }


        protected override bool GenerateOneBit()
        {
            bool clock = true;
            Registers[0].NextStep();
            bool xor = true;
            for (int i = 0; i < Registers.Length-1; i++)
            {
                bool output = Registers[i].GetOutputBit();
                xor = output ^ xor;
                clock = xor;
                if (clock)
                {
                    Registers[i+1].NextStep();
                    clock = Registers[i + 1].GetOutputBit() ^ clock;
                }
                else
                {
                    clock = Registers[i+1].GetOutputBit() ^ clock;
                }
            }

            return Registers[Registers.Length - 1].GetOutputBit();

        }

        protected override void NextStep()
        {
            return;
        }
    }
}
