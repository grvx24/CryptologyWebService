using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.LfsrGenerators
{
    public class Lfsr
    {
        public BitArray Register { get; private set; }
        public int[] FeedbackFunction { get; set; }
        int lenght;

        public override string ToString()
        {
            char[] result = new char[Register.Length];

            for (int i = 0; i < Register.Length; i++)
            {
                result[i] = Register[i] ? '1' : '0';
            }

            return new string(result);
        }

        public Lfsr()
        {
            Register = new BitArray(8);
            lenght = 8;
            Random rng = new Random();

            for (int i = 0; i < lenght; i++)
            {
                Register[i] = rng.Next(0, 2) > 0;

            }

            FeedbackFunctionInit();
        }

        public Lfsr(int lenght)
        {
            Register = new BitArray(lenght);
            this.lenght = lenght;
            Random rng = new Random();

            for (int i = 0; i < lenght; i++)
            {
                Register[i] = rng.Next(0, 2) > 0;

            }

            FeedbackFunctionInit();
        }

        public Lfsr(string registerValues)
        {
            this.lenght = registerValues.Length;
            Register = new BitArray(lenght);

            for (int i = 0; i < lenght; i++)
            {
                Register[i] = registerValues[i] == '1' ? true : false;

            }

            FeedbackFunctionInit();
        }

        public void SetRegisterValues(BitArray array)
        {
            Register = new BitArray(array.Length);
            lenght = array.Length;
            for (int i = 0; i < lenght; i++)
            {
                Register[i] = array[i];
            }

            FeedbackFunctionInit();
        }

        public void NextStep()
        {
            bool temp = true;


            for (int i = 0; i < FeedbackFunction.Length; i++)
            {
                temp ^= Register[FeedbackFunction[i]];
            }


            for (int i = 1; i < Register.Count; i++)
            {
                Register[i - 1] = Register[i];
            }


            Register[Register.Count - 1] = temp;


        }
        public bool GetOutputBit()
        {
            return Register[Register.Length - 1];
        }

        private void FeedbackFunctionInit()
        {
            switch (lenght)
            {
                case 2:
                    FeedbackFunction = new int[] { 0, lenght - 1 };
                    break;
                case 3:
                    FeedbackFunction = new int[] { 0, lenght - 1 };
                    break;
                case 4:
                    FeedbackFunction = new int[] { 0, lenght - 1 };
                    break;
                case 5:
                    FeedbackFunction = new int[] { 0, 2 };
                    break;
                case 6:
                    FeedbackFunction = new int[] { 0, lenght - 1 };
                    break;
                case 7:
                    FeedbackFunction = new int[] { 0, lenght - 1 };
                    break;
                case 8:
                    FeedbackFunction = new int[] { 0, 1, 6, 7 };
                    break;
                case 9:
                    FeedbackFunction = new int[] { 0, 4 };
                    break;
                case 10:
                    FeedbackFunction = new int[] { 0, 3 };
                    break;
                case 11:
                    FeedbackFunction = new int[] { 0, 8, 10 };
                    break;
                case 12:
                    FeedbackFunction = new int[] { 0, 3, 9, 10, 11 };
                    break;
                case 13:
                    FeedbackFunction = new int[] { 0, 2, 4, 5, 6, 11 };
                    break;
                case 14:
                    FeedbackFunction = new int[] { 0, 3, 4, 5 };
                    break;
                case 15:
                    FeedbackFunction = new int[] { 0, 1, 13, 14 };
                    break;
                case 16:
                    FeedbackFunction = new int[] { 0, 3, 6 };
                    break;
                case 17:
                    FeedbackFunction = new int[] { 0, 6, 7, 8 };
                    break;
                case 18:
                    FeedbackFunction = new int[] { 0, 1, 2, 8 };
                    break;
                case 19:
                    FeedbackFunction = new int[] { 0, 1, 5, 18 };
                    break;
                case 20:
                    FeedbackFunction = new int[] { 0, 1, 2, 8, 17, 19 };
                    break;

                default:
                    break;
            }
        }

    }
}
