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

            for (int i = 0; i < Registers.Length; i++)
            {
                if (clock)
                {
                    Registers[i].NextStep();
                    clock = Registers[i].GetOutputBit() ^ clock;

                }
                else
                {
                    clock = Registers[i].GetOutputBit() ^ clock;
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
