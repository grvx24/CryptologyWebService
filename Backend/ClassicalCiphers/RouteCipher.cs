using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoWebService.Helpers;

namespace CryptoWebService.Backend.ClassicalCiphers
{
    public class RouteCipher : IClassicalCiphers
    {
        public enum CipherMode
        {
            Key_As_Width,
            Key_As_Height
        }

        public int Key { get; set; }
        public char[,] MessageMatrix { get; private set; }
        public CipherMode Mode { get; set; }
        
        public RouteCipher(int key,CipherMode mode)
        {
            Key = key;
            Mode = mode;
        }

        private void FillMatrix(string message)
        {
            int row = 0;
            int column = 0;
            int messageCounter = 0;

            for (int i = 0; i < MessageMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < MessageMatrix.GetLength(1); j++)
                {
                    MessageMatrix[i, j] = message[messageCounter];
                    messageCounter++;

                    if (messageCounter == message.Length)
                    {
                        row = i;
                        column = j;
                        break;
                    }
                }
            }

            for (int i = row; i < MessageMatrix.GetLength(0); i++)
            {
                for (int j = column; j < MessageMatrix.GetLength(1); j++)
                {
                    MessageMatrix[i, j] = 'X';
                }
            }
        }

        private string ReadEncryptedFromMatrix()
        {
            if (MessageMatrix != null)
            {
                char[] encrypted = new char[MessageMatrix.Length];
                int encryptedCounter = 0;
                bool up = false;

                for (int j = MessageMatrix.GetLength(1)-1; j >=0 ; j--)
                {

                    if(!up)
                    {
                        for (int i = 0; i < MessageMatrix.GetLength(0); i++)
                        {
                            encrypted[encryptedCounter] = MessageMatrix[i, j];
                        }

                        up = !up;
                    }
                    else
                    {
                        for (int i = MessageMatrix.GetLength(0); i >=0 ; i--)
                        {
                            encrypted[encryptedCounter] = MessageMatrix[i, j];
                        }

                        up = !up;
                    }
                }

                return new string(encrypted);
            }

            return "";
        }


        public string Encrypt(string message)
        {
            message = StringHelper.ReplaceWhitespace(message, "");
            message = message.ToUpper();

            int dimensionSize=0;
            string encrypted = "";

            if (message.Length % Key != 0)
            {
                dimensionSize = message.Length / Key + 1;
            }
            else
            {
                dimensionSize = message.Length / Key;
            }

            switch (Mode)
            {
                case CipherMode.Key_As_Width:
                {
                    MessageMatrix = new char[dimensionSize, Key];
                    FillMatrix(message);
                    encrypted = ReadEncryptedFromMatrix();
                    break;
                }
                case CipherMode.Key_As_Height:
                {
                    MessageMatrix = new char[Key, dimensionSize];
                    FillMatrix(message);
                    encrypted = ReadEncryptedFromMatrix();
                        break;
                }


            }

            return encrypted;
        }

        private void WriteDecryptedToMatrix(ref string message)
        {
            int messageCounter = 0;
            bool up = false;

            for (int j = MessageMatrix.GetLength(1) - 1; j >= 0; j--)
            {
                if (up)
                {
                    for (int i = 0; i < MessageMatrix.GetLength(0); i++)
                    {
                        MessageMatrix[i, j] = message[messageCounter];
                        messageCounter++;
                    }

                    up = !up;
                }
                else
                {
                    for (int i = MessageMatrix.GetLength(0) - 1; i >= 0; i--)
                    {
                        MessageMatrix[i, j] = message[messageCounter];
                        messageCounter++;
                    }

                    up = !up;
                }
            }
        }

        private string ReadDecryptedFromMatrix()
        {
            char[] decrypted = new char[MessageMatrix.Length];
            for (int i = 0; i < MessageMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < MessageMatrix.GetLength(1); j++)
                {
                    decrypted[i * MessageMatrix.GetLength(0) + j] = MessageMatrix[i, j];
                }
            }

            return new string(decrypted);
        }

        public string Decrypt(string message)
        {
            message = StringHelper.ReplaceWhitespace(message, "");
            message = message.ToUpper();

            var dimensionSize = message.Length / Key;

            string result = "";

            switch (Mode)
            {
                case CipherMode.Key_As_Width:
                {
                    MessageMatrix = new char[dimensionSize, Key];
                    WriteDecryptedToMatrix(ref message);
                    result = ReadDecryptedFromMatrix();

                        break;
                }

                case CipherMode.Key_As_Height:
                {
                    MessageMatrix = new char[Key, dimensionSize];
                    WriteDecryptedToMatrix(ref message);
                    result = ReadDecryptedFromMatrix();

                        break;
                }
            }
            
            return result;
        }
    }
}
