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

        public RouteCipher(int key, CipherMode mode)
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
                for (int j = column + 1; j < MessageMatrix.GetLength(1); j++)
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
                int direction = 0;//0-down, 1-left, 2-up, 3-right

                int columnRangeMin = 0;
                int columnRangeMax = MessageMatrix.GetLength(1) - 1;

                int rowRangeMin = 0;
                int rowRangeMax = MessageMatrix.GetLength(0) - 1;

                while (rowRangeMax != rowRangeMin || columnRangeMin != columnRangeMax)
                {

                    if (direction == 0)
                    {
                        if (encryptedCounter >= MessageMatrix.Length)
                            break;
                        for (int i = rowRangeMin; i <= rowRangeMax; i++)
                        {
                            encrypted[encryptedCounter] = MessageMatrix[i, columnRangeMax];
                            encryptedCounter++;
                        }



                        direction = 1;
                        columnRangeMax--;
                    }

                    if (direction == 1)
                    {
                        if (encryptedCounter >= MessageMatrix.Length)
                            break;
                        for (int i = columnRangeMax; i >= columnRangeMin; i--)
                        {
                            encrypted[encryptedCounter] = MessageMatrix[rowRangeMax, i];
                            encryptedCounter++;
                        }


                        direction = 2;
                        rowRangeMax--;
                    }

                    if (direction == 2)
                    {
                        if (encryptedCounter >= MessageMatrix.Length)
                            break;
                        for (int i = rowRangeMax; i >= columnRangeMin; i--)
                        {
                            encrypted[encryptedCounter] = MessageMatrix[i, columnRangeMin];
                            encryptedCounter++;
                        }


                        direction = 3;
                        columnRangeMin++;
                    }
                    if (direction == 3)
                    {
                        if (encryptedCounter >= MessageMatrix.Length)
                            break;
                        for (int i = columnRangeMin; i <= columnRangeMax; i++)
                        {
                            encrypted[encryptedCounter] = MessageMatrix[rowRangeMin, i];
                            encryptedCounter++;
                        }


                        direction = 0;
                        rowRangeMin++;
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

            int dimensionSize = 0;
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
            int counter = 0;
            int direction = 0;

            int columnRangeMin = 0;
            int columnRangeMax = MessageMatrix.GetLength(1) - 1;

            int rowRangeMin = 0;
            int rowRangeMax = MessageMatrix.GetLength(0) - 1;

            while (rowRangeMax != rowRangeMin || columnRangeMin != columnRangeMax)
            {

                if (direction == 0)
                {
                    if (counter >= MessageMatrix.Length)
                        break;
                    for (int i = rowRangeMin; i <= rowRangeMax; i++)
                    {
                        MessageMatrix[i, columnRangeMax] = message[counter];
                        counter++;
                    }

                    direction = 1;
                    columnRangeMax--;
                }

                if (direction == 1)
                {
                    if (counter >= MessageMatrix.Length)
                        break;
                    for (int i = columnRangeMax; i >= columnRangeMin; i--)
                    {
                        MessageMatrix[rowRangeMax, i] = message[counter];
                        counter++;
                    }


                    direction = 2;
                    rowRangeMax--;
                }

                if (direction == 2)
                {
                    if (counter >= MessageMatrix.Length)
                        break;
                    for (int i = rowRangeMax; i >= columnRangeMin; i--)
                    {
                        MessageMatrix[i, columnRangeMin] = message[counter];
                        counter++;
                    }


                    direction = 3;
                    columnRangeMin++;
                }
                if (direction == 3)
                {
                    if (counter >= MessageMatrix.Length)
                        break;
                    for (int i = columnRangeMin; i <= columnRangeMax; i++)
                    {
                        MessageMatrix[rowRangeMin, i] = message[counter];
                        counter++;
                    }


                    direction = 0;
                    rowRangeMin++;
                }

            }


        }

        private string ReadDecryptedFromMatrix()
        {
            char[] decrypted = new char[MessageMatrix.Length];
            int counter = 0;
            for (int i = 0; i < MessageMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < MessageMatrix.GetLength(1); j++)
                {
                    decrypted[counter] = MessageMatrix[i, j];
                    counter++;
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
