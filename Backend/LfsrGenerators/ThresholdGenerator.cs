using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.LfsrGenerators
{
    public class ThresholdGenerator : LfsrGenerator
    {
        public ThresholdGenerator(Lfsr[] registers) : base(registers)
        {
            if (registers.Length % 2 == 0)
            {
                throw new ArgumentException("Długość rejestrów musi być nieparzysta!");
            }

            this.Registers = registers;

        }

        protected override bool GenerateOneBit()
        {
            int threshold = Registers.Length / 2;
            int sum = 0;
            NextStep();
            foreach (var r in Registers)
            {
                sum += Convert.ToInt32(r.GetOutputBit());
            }

            if (sum > threshold)
                return true;
            else
                return false;
        }
    }
}
