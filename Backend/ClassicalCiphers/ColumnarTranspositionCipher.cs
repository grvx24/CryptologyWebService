using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CryptoWebService.Helpers;

namespace CryptoWebService.Backend.ClassicalCiphers
{
    public class ColumnarTranspositionCipher:IClassicalCiphers
    {
        private string _key;
        public string Key
        {
            get => _key;
            set => _key = value.ToUpper();
        }


        public char[] _sortedKey;
        public int[] _columnNumbers;
        private char[,] _charMatrixEncrypted;
        private char[,] _charMatrixDecrypted;

        public ColumnarTranspositionCipher(string key)
        {
            Key = key;
        }

        public string Encrypt(string message)
        {
            message = StringHelper.ReplaceWhitespace(message, "");
            message = message.ToUpper();
            Key = StringHelper.ReplaceWhitespace(Key, "");
            _sortedKey = Key.OrderBy(c => c).ToArray();
            _columnNumbers = new int[_sortedKey.Length];
            int[] columnOrder = new int[_columnNumbers.Length];

            for (int i = 0; i < _sortedKey.Length; i++)
            {
                for (int j = 0; j < _sortedKey.Length; j++)
                {
                    if (Key[j] == _sortedKey[i])
                    {
                        if (_columnNumbers[j] == 0)
                        {
                            _columnNumbers[j] = i + 1;
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < columnOrder.Length; i++)
            {
                columnOrder[i] = Array.IndexOf(_columnNumbers, i + 1);
            }

            int columns = _columnNumbers.Length;
            int rows = 0;
            if (message.Length % columns != 0)
            {
                rows = message.Length / columns + 1;
            }
            else
            {
                rows = message.Length / columns;
            }
            _charMatrixEncrypted = new char[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    _charMatrixEncrypted[i, j] = 'X';
                }
            }

            int messageCounter = 0;
            for (int i = 0; i < _charMatrixEncrypted.GetLength(0); i++)
            {
                for (int j = 0; j < _charMatrixEncrypted.GetLength(1); j++)
                {
                    _charMatrixEncrypted[i, j] = message[messageCounter];
                    messageCounter++;

                    if (messageCounter == message.Length)
                    {
                        break;
                    }
                }
            }

            char[] encrypted = new char[_charMatrixEncrypted.Length];
            int encryptedCounter = 0;

            for (int i = 0; i < columnOrder.Length; i++)
            {
                for (int j = 0; j < _charMatrixEncrypted.GetLength(0); j++)
                {
                    encrypted[encryptedCounter] = _charMatrixEncrypted[j, columnOrder[i]];
                    encryptedCounter++;
                }
            }

            return new string(encrypted);
        }

        public string Decrypt(string message)
        {
            message = StringHelper.ReplaceWhitespace(message, "");
            message = message.ToUpper();
            _sortedKey = Key.OrderBy(c => c).ToArray();
            _columnNumbers = new int[_sortedKey.Length];
            int[] columnOrder = new int[_columnNumbers.Length];

            for (int i = 0; i < _sortedKey.Length; i++)
            {
                for (int j = 0; j < _sortedKey.Length; j++)
                {
                    if (Key[j] == _sortedKey[i])
                    {
                        if (_columnNumbers[j] == 0)
                        {
                            _columnNumbers[j] = i + 1;
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < columnOrder.Length; i++)
            {
                columnOrder[i] = Array.IndexOf(_columnNumbers, i + 1);
            }

            int columns = _columnNumbers.Length;
            int rows = 0;
            if (message.Length % columns != 0)
            {
                rows = message.Length / columns + 1;
            }
            else
            {
                rows = message.Length / columns;
            }
            _charMatrixDecrypted = new char[rows, columns];

            int messageCounter = 0;
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    _charMatrixDecrypted[j, columnOrder[i]] = message[messageCounter];
                    messageCounter++;
                }

            }


            char[] decrypted = new char[_charMatrixDecrypted.Length];
            int decryptedCounter = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    decrypted[decryptedCounter] = _charMatrixDecrypted[i, j];
                    decryptedCounter++;
                }
            }


            return new string(decrypted);
        }
    }
}
