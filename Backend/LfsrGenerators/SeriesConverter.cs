using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.LfsrGenerators
{
    public class SeriesConverter
    {
        public static byte[] BoolToByteArray(bool[] input)
        {
            if (input.Length % 8 != 0)
            {
                var rest = input.Length % 8;

                byte[] ret = new byte[(input.Length / 8) + 1];
                for (int i = 0; i < input.Length - rest; i += 8)
                {
                    int value = 0;
                    for (int j = 0; j < 8; j++)
                    {
                        if (input[i + j])
                        {
                            value += 1 << (7 - j);
                        }
                    }
                    ret[i / 8] = (byte)value;
                }
                int value2 = 0;
                for (int i = input.Length - rest; i < input.Length; i++)
                {
                    if (input[i])
                    {
                        value2 += 1 << i;
                    }

                }
                ret[ret.Length - 1] = (byte)value2;

                return ret;

            }
            else
            {
                byte[] ret = new byte[input.Length / 8];
                for (int i = 0; i < input.Length; i += 8)
                {
                    int value = 0;
                    for (int j = 0; j < 8; j++)
                    {
                        if (input[i + j])
                        {
                            value += 1 << (7 - j);
                        }
                    }
                    ret[i / 8] = (byte)value;
                }

                return ret;
            }
        }

        public static Int32[] BoolToInt32(bool[] input)
        {
            if (input.Length % 32 != 0)
            {
                var rest = input.Length % 32;

                Int32[] ret = new Int32[(input.Length / 32) + 1];
                for (int i = 0; i < input.Length - rest; i += 32)
                {
                    int value = 0;
                    for (int j = 0; j < 32; j++)
                    {
                        if (input[i + j])
                        {
                            value += 1 << (31 - j);
                        }
                    }
                    ret[i / 32] = (Int32)value;
                }

                int value2 = 0;
                for (int i = input.Length - rest; i < input.Length; i++)
                {
                    if (input[i])
                    {
                        value2 += 1 << i;
                    }

                }
                ret[ret.Length - 1] = value2;
                return ret;

            }
            else
            {
                Int32[] ret = new Int32[input.Length / 32];
                for (int i = 0; i < input.Length; i += 32)
                {
                    int value = 0;
                    for (int j = 0; j < 32; j++)
                    {
                        if (input[i + j])
                        {
                            value += 1 << (31 - j);
                        }
                    }
                    ret[i / 32] = (Int32)value;
                }
                return ret;
            }
        }

        private byte[] ToByteArray(bool[] input)
        {
            if (input.Length % 8 != 0)
            {
                byte[] ret = new byte[(input.Length / 8)];
                for (int i = 0; i < input.Length - input.Length % 8; i += 8)
                {
                    int value = 0;
                    for (int j = 0; j < 8; j++)
                    {
                        if (input[i + j])
                        {
                            value += 1 << (7 - j);
                        }
                    }
                    ret[i / 8] = (byte)value;
                }
                return ret;

            }
            else
            {
                byte[] ret = new byte[input.Length / 8];
                for (int i = 0; i < input.Length; i += 8)
                {
                    int value = 0;
                    for (int j = 0; j < 8; j++)
                    {
                        if (input[i + j])
                        {
                            value += 1 << (7 - j);
                        }
                    }
                    ret[i / 8] = (byte)value;
                }
                return ret;
            }
        }

        public static char BoolToChar(bool input)
        {
            return input ? '1' : '0';
        }
    }
}
