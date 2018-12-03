using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoWebService.Helpers;

namespace CryptoWebService.Backend.ClassicalCiphers
{
    public class FenceCipher : IClassicalCiphers
    {
        public int FenceLevel { get; set; }

        public FenceCipher(int fenceLevel)
        {
            this.FenceLevel = fenceLevel;
        }

        public string Encrypt(string message)
        {
            message = StringHelper.ReplaceWhitespace(message, "");
            message = message.ToUpper();

            try
            {
                message = message.ToUpper();

                var rows = new List<char>[FenceLevel];
                for (int i = 0; i < rows.Length; i++)
                {
                    rows[i]=new List<char>();
                }

                int currentRow = 0;
                int direction = 1;
                for (int i = 0; i < message.Length; i++)
                {
                    rows[currentRow].Add(message[i]);

                    if ((currentRow == FenceLevel - 1 && direction==1) || (currentRow==0 && direction==-1))
                    {
                        direction = -direction;
                    }

                    currentRow += direction;
                }

                var encrypted = new StringBuilder(message.Length);

                for (int i = 0; i < rows.Length; i++)
                {
                    rows[i].ForEach(c => encrypted.Append(c));
                }

                return encrypted.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }

            
        }

        public string Decrypt(string message)
        {
            message = StringHelper.ReplaceWhitespace(message, "");
            message = message.ToUpper();

            try
            {
                message = message.ToUpper();

                char[][] rows = new char[FenceLevel][];
                for (int i = 0; i < FenceLevel; i++)
                {
                    rows[i] = new char[message.Length];
                }

                int row = 0, col = 0;
                bool directionDown = false;

                for (int i = 0; i < message.Length; i++)
                {
                    if (row == 0)
                        directionDown = true;
                    if (row == FenceLevel - 1)
                        directionDown = false;
                    rows[row][col++] = '*';

                    if (directionDown)
                        row++;
                    else
                        row--;

                }

                int index = 0;

                for (int i = 0; i < FenceLevel; i++)
                {
                    for (int j = 0; j < message.Length; j++)
                    {
                        if (rows[i][j] == '*' && index < message.Length)
                            rows[i][j] = message[index++];
                    }
                }

                var decrypted = new StringBuilder(message.Length);
                row = 0;
                col = 0;

                for (int i = 0; i < message.Length; i++)
                {
                    if (row == 0)
                        directionDown = true;
                    if (row == FenceLevel - 1)
                        directionDown = false;

                    if (rows[row][col] != '*')
                        decrypted.Append(rows[row][col]);

                    if (directionDown)
                        row++;
                    else
                        row--;
                }

                return decrypted.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw ;
            }
            
        }

        public char[][] getTable(string message)
        {
            message = StringHelper.ReplaceWhitespace(message, "");
            message = message.ToUpper();

            try
            {
                message = message.ToUpper();

                char[][] rows = new char[FenceLevel][];
                for (int i = 0; i < FenceLevel; i++)
                {
                    rows[i] = new char[message.Length];
                }

                int row = 0, col = 0;
                bool directionDown = false;

                for (int i = 0; i < message.Length; i++)
                {
                    if (row == 0)
                        directionDown = true;
                    if (row == FenceLevel - 1)
                        directionDown = false;
                    rows[row][col++] = '*';

                    if (directionDown)
                        row++;
                    else
                        row--;

                }

                int index = 0;

                for (int i = 0; i < FenceLevel; i++)
                {
                    for (int j = 0; j < message.Length; j++)
                    {
                        if (rows[i][j] == '*' && index < message.Length)
                            rows[i][j] = message[index++];
                    }
                }

                return rows;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
