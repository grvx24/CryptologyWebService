using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using CryptoWebService.Helpers;

namespace CryptoWebService.Backend.ClassicalCiphers
{
    public class PlayfairCipher : IClassicalCiphers
    {

        private readonly string _alphabet = Alphabets.ALPHABET_EN_WITHOUT_J;
        public string Key { get; set; }
        public char[,] KeyMatrix { get; private set; }

        private class CharPosition
        {
            private int _matrixSize = 5;
            public int X { get; }
            public int Y { get; }

            public CharPosition(int x, int y)
            {
                X = x;
                Y = y;
            }

            public CharPosition NextRightChar()
            {
                int newYPosition = Y + 1;

                if (Y >= _matrixSize - 1)
                {
                    newYPosition = 0;
                }

                return new CharPosition(X, newYPosition);
            }

            public CharPosition NextBottomChar()
            {
                int newXPosition = X + 1;

                if (X >= _matrixSize - 1)
                {
                    newXPosition = 0;
                }

                return new CharPosition(newXPosition, Y);
            }

            public CharPosition NextLeftChar()
            {
                int newYPosition = Y - 1;

                if (Y == 0)
                {
                    newYPosition = _matrixSize - 1;
                }

                return new CharPosition(X, newYPosition);
            }

            public CharPosition NextTopChar()
            {
                int newXPosition = X - 1;

                if (X == 0)
                {
                    newXPosition = _matrixSize - 1;
                }

                return new CharPosition(newXPosition, Y);
            }
        }
        private Dictionary<char, CharPosition> _charPositionInMatrix;

        public PlayfairCipher(string key)
        {
            key = StringHelper.ReplaceWhitespace(key, "");
            key = key.ToUpper();
            key = key.Replace('J', 'I');

            var keyDistinct = key.Distinct();
            this.Key = new string(keyDistinct.ToArray());
            var otherCharacters = new string(_alphabet.Where(c => !Key.Contains(c)).ToArray());

            var matrixString = this.Key + otherCharacters;
            int matrixStringCounter = 0;

            KeyMatrix = new char[5, 5];
            _charPositionInMatrix = new Dictionary<char, CharPosition>();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var character = matrixString[matrixStringCounter];
                    KeyMatrix[i, j] = character;
                    _charPositionInMatrix.Add(character, new CharPosition(i, j));
                    matrixStringCounter++;
                }
            }


        }

        private bool IsTheSameRows(char a, char b)
        {
            int row = -1;

            for (int i = 0; i < KeyMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < KeyMatrix.GetLength(1); j++)
                {
                    if (KeyMatrix[i, j] == a)
                    {
                        row = i;
                        break;
                    }
                }
            }

            if (row < 0)
            {
                return false;
            }

            for (int i = 0; i < KeyMatrix.GetLength(1); i++)
            {

                if (KeyMatrix[row, i] == b)
                {
                    return true;
                }
            }

            return false;

        }

        private bool IsTheSameColumns(char a, char b)
        {
            int column = -1;

            for (int i = 0; i < KeyMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < KeyMatrix.GetLength(1); j++)
                {
                    if (KeyMatrix[i, j] == a)
                    {
                        column = j;
                        break;
                    }
                }
            }

            if (column < 0)
            {
                return false;
            }

            for (int i = 0; i < KeyMatrix.GetLength(1); i++)
            {

                if (KeyMatrix[i, column] == b)
                {
                    return true;
                }
            }

            return false;
        }

        public string Encrypt(string message)
        {
            message = StringHelper.ReplaceWhitespace(message, "");
            message = message.ToUpper();
            message = message.Replace('J', 'I');
            if (message.Length % 2 != 0)
            {
                message += "X";
            }

            char[] encrypted = new char[message.Length];


            var splitMessage = StringHelper.SplitInParts(message, 2).ToArray();

            int encryptedMessageCounter = 0;

            for (int i = 0; i < splitMessage.Length; i++)
            {

                var char1 = splitMessage[i][0];
                var char2 = splitMessage[i][1];

                if (IsTheSameRows(char1, char2))
                {
                    var char1Position = _charPositionInMatrix[char1];
                    var char2Position = _charPositionInMatrix[char2];

                    var newCharacter1 = char1Position.NextRightChar();
                    encrypted[encryptedMessageCounter] = KeyMatrix[newCharacter1.X, newCharacter1.Y];
                    encryptedMessageCounter++;

                    var newCharacter2 = char2Position.NextRightChar();
                    encrypted[encryptedMessageCounter] = KeyMatrix[newCharacter2.X, newCharacter2.Y];
                    encryptedMessageCounter++;

                }
                else if (IsTheSameColumns(splitMessage[i][0], splitMessage[i][1]))
                {
                    var char1Position = _charPositionInMatrix[char1];
                    var char2Position = _charPositionInMatrix[char2];

                    var newCharacter1 = char1Position.NextBottomChar();
                    encrypted[encryptedMessageCounter] = KeyMatrix[newCharacter1.X, newCharacter1.Y];
                    encryptedMessageCounter++;

                    var newCharacter2 = char2Position.NextBottomChar();
                    encrypted[encryptedMessageCounter] = KeyMatrix[newCharacter2.X, newCharacter2.Y];
                    encryptedMessageCounter++;
                }
                else
                {
                    var char1Position = _charPositionInMatrix[char1];
                    var char2Position = _charPositionInMatrix[char2];

                    var newCharacter1 = new CharPosition(char1Position.X, char2Position.Y);
                    encrypted[encryptedMessageCounter] = KeyMatrix[newCharacter1.X, newCharacter1.Y];
                    encryptedMessageCounter++;


                    var newCharacter2 = new CharPosition(char2Position.X, char1Position.Y);
                    encrypted[encryptedMessageCounter] = KeyMatrix[newCharacter2.X, newCharacter2.Y];
                    encryptedMessageCounter++;
                }

            }

            return new string(encrypted);
        }

        public string Decrypt(string message)
        {
            message = StringHelper.ReplaceWhitespace(message, "");
            message = message.ToUpper();
            message = message.Replace('J', 'I');
            if (message.Length % 2 != 0)
            {
                message += "X";
            }

            var splitMessage = StringHelper.SplitInParts(message, 2).ToArray();

            char[] decrypted = new char[message.Length];
            int decryptedMessageCounter = 0;

            foreach (var t in splitMessage)
            {
                var char1 = t[0];
                var char2 = t[1];

                if (IsTheSameRows(char1, char2))
                {
                    var char1Position = _charPositionInMatrix[char1];
                    var char2Position = _charPositionInMatrix[char2];

                    var newCharacter1 = char1Position.NextLeftChar();
                    decrypted[decryptedMessageCounter] = KeyMatrix[newCharacter1.X, newCharacter1.Y];
                    decryptedMessageCounter++;

                    var newCharacter2 = char2Position.NextLeftChar();
                    decrypted[decryptedMessageCounter] = KeyMatrix[newCharacter2.X, newCharacter2.Y];
                    decryptedMessageCounter++;

                }
                else if (IsTheSameColumns(t[0], t[1]))
                {
                    var char1Position = _charPositionInMatrix[char1];
                    var char2Position = _charPositionInMatrix[char2];

                    var newCharacter1 = char1Position.NextTopChar();
                    decrypted[decryptedMessageCounter] = KeyMatrix[newCharacter1.X, newCharacter1.Y];
                    decryptedMessageCounter++;

                    var newCharacter2 = char2Position.NextTopChar();
                    decrypted[decryptedMessageCounter] = KeyMatrix[newCharacter2.X, newCharacter2.Y];
                    decryptedMessageCounter++;
                }
                else
                {
                    var char1Position = _charPositionInMatrix[char1];
                    var char2Position = _charPositionInMatrix[char2];

                    var newCharacter1 = new CharPosition(char1Position.X, char2Position.Y);
                    decrypted[decryptedMessageCounter] = KeyMatrix[newCharacter1.X, newCharacter1.Y];
                    decryptedMessageCounter++;


                    var newCharacter2 = new CharPosition(char2Position.X, char1Position.Y);
                    decrypted[decryptedMessageCounter] = KeyMatrix[newCharacter2.X, newCharacter2.Y];
                    decryptedMessageCounter++;
                }
            }

            return new string(decrypted);
        }
    }
}
