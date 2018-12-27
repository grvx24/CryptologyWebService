using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CryptoWebService.Backend.ClassicalCiphers;
using CryptoWebService.Helpers;
using CryptoWebService.Models.Ciphers;
using CryptoWebService.Models.ClassicalCiphers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;

namespace CryptoWebService.Controllers.Ciphers
{
    public class ClassicalCiphersController : Controller
    {
 #region Caesar 
        [HttpGet]
        public IActionResult Caesar()
        {
            return View();
        }
  
     
        [HttpPost]
        public IActionResult CaesarEncrypt([FromBody]CaesarCipherViewModel viewModel)
        {
            CaesarCipher cipher = new CaesarCipher(viewModel.Key)
            {
                Alphabet = Alphabets.GetAlphabet((Alphabets.AlphabetType) viewModel.AlphabetType)
            };
            cipher.Alphabet = Alphabets.GetAlphabet((Alphabets.AlphabetType) viewModel.AlphabetType);

            string encrypted = "";
            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
            }
            catch (NullReferenceException )
            {

            }
            catch (Exception )
            {
                return BadRequest(new {Result = false, Message = Text.InvalidCharacter});
            }
            return Json(encrypted);
        }

        [HttpPost]
        public IActionResult CaesarDecrypt([FromBody]CaesarCipherViewModel viewModel)
        {
            CaesarCipher cipher = new CaesarCipher(viewModel.Key)
            {
                Alphabet = Alphabets.GetAlphabet((Alphabets.AlphabetType) viewModel.AlphabetType)
            };

            string decrypted = "";
            try
            {
                decrypted = cipher.Decrypt(viewModel.Message);
            }
            catch (Exception )
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }
            return Json(decrypted);
        }

        [HttpPost]
        public IActionResult CaesarVisualization([FromBody]CaesarCipherViewModel viewModel)
        {
            CaesarCipher cipher = new CaesarCipher(viewModel.Key)
            {
                Alphabet = Alphabets.GetAlphabet((Alphabets.AlphabetType)viewModel.AlphabetType)
            };
            cipher.Alphabet = Alphabets.GetAlphabet((Alphabets.AlphabetType)viewModel.AlphabetType);

            string[] results = new string[4] { "alphabet", "newalphabet", "output","input"};
            string encrypted = "";
            string input = viewModel.Message;
            input = StringHelper.ReplaceWhitespace(input, "");
            input = input.ToUpper();
            int alphabetLength = cipher.Alphabet.Length;
            int newKey = (viewModel.Key) % alphabetLength;
            string cipherAlphabet = "";
            for (int i = 0; i < alphabetLength; i++)
            {
                if((i+newKey)>= alphabetLength)
                {
                    cipherAlphabet += cipher.Alphabet[i+newKey- alphabetLength];
                }
                else
                {
                    cipherAlphabet += cipher.Alphabet[i + newKey];
                }
            }
            results[0] = cipher.Alphabet;
            results[1] = cipherAlphabet;
            results[3] = input;
            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
                results[2] = encrypted;
            
            }
            catch (NullReferenceException )
            {

            }
            catch (Exception )
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }
            return Json(results);
        }

#endregion

 #region Affine
        [HttpGet]
        public IActionResult Affine()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AffineEncrypt([FromBody] AffineCipherViewModel viewModel)
        {
            AffineCipher cipher = new AffineCipher(viewModel.KeyA, viewModel.KeyB)
            {
                Alphabet = Alphabets.GetAlphabet((Alphabets.AlphabetType) viewModel.AlphabetType)
            };

            string encrypted = "";

            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
            }
            catch (Exception )
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }

            return Json(encrypted);
        }
        [HttpPost]
        public IActionResult AffineDecrypt([FromBody] AffineCipherViewModel viewModel)
        {
            AffineCipher cipher = new AffineCipher(viewModel.KeyA, viewModel.KeyB)
            {
                Alphabet = Alphabets.GetAlphabet((Alphabets.AlphabetType)viewModel.AlphabetType)
            };

            string decrypted = "";

            try
            {
                decrypted = cipher.Decrypt(viewModel.Message);
            }
            catch (Exception )
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }

            return Json(decrypted);
        }

        [HttpPost]
        public IActionResult AffineVisualization([FromBody]AffineCipherViewModel viewModel)
        {
            AffineCipher cipher = new AffineCipher(viewModel.KeyA, viewModel.KeyB)
            {
                Alphabet = Alphabets.GetAlphabet((Alphabets.AlphabetType)viewModel.AlphabetType)
            };

            cipher.Alphabet = Alphabets.GetAlphabet((Alphabets.AlphabetType)viewModel.AlphabetType);

            string[] results = new string[3] { "alphabet", "output", "input" };
            string encrypted = "";
            string input = viewModel.Message;
            input = StringHelper.ReplaceWhitespace(input, "");
            input = input.ToUpper();
            int alphabetLength = cipher.Alphabet.Length;
            input = StringHelper.ReplaceWhitespace(input, "");
            input = input.ToUpper();
            results[0] = cipher.Alphabet;
            results[2] = input;
            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
                results[1] = encrypted;

            }
            catch (NullReferenceException )
            {

            }
            catch (Exception )
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }
            return Json(results);
        }
        #endregion

 #region Bacon
        [HttpGet]
        public IActionResult Bacon()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BaconEncrypt([FromBody]BaconCipherViewModel viewModel)
        {
            BaconCipher cipher = new BaconCipher();

            string encrypted = "";
            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
            }
            catch (Exception )
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }

            return Json(encrypted);
        }

        [HttpPost]
        public IActionResult BaconDecrypt([FromBody]BaconCipherViewModel viewModel)
        {
            BaconCipher cipher = new BaconCipher();

            string decrypted = "";
            try
            {
                decrypted = cipher.Decrypt(viewModel.Message);
            }
            catch (Exception )
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }

            return Json(decrypted);
        }

        [HttpPost]
        public IActionResult BaconVisualization([FromBody]BaconCipherViewModel viewModel)
        {
            BaconCipher cipher = new BaconCipher();

            string[] results = new string[2] { "input", "output" };
            string encrypted = "";
            string input = viewModel.Message;
            input = StringHelper.ReplaceWhitespace(input, "");
            input = input.ToUpper();
            results[0] = input;
            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
                results[1] = encrypted;

            }
            catch (Exception )
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }
            return Json(results);
        }

        #endregion

#region ColumnarTransposition
        [HttpGet]
        public IActionResult ColumnarTransposition()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ColumnarTranspositionEncrypt([FromBody]ColumnarTranspositionCipherViewModel viewModel)
        {
            ColumnarTranspositionCipher cipher = new ColumnarTranspositionCipher(viewModel.Key);

            string encrypted = "";

            try
            {
                encrypted=cipher.Encrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(encrypted);
        }

        [HttpPost]
        public IActionResult ColumnarTranspositionDecrypt([FromBody]ColumnarTranspositionCipherViewModel viewModel)
        {
            ColumnarTranspositionCipher cipher = new ColumnarTranspositionCipher(viewModel.Key);

            string decrypted = "";

            try
            {
                decrypted = cipher.Decrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(decrypted);
        }


        public IActionResult ColumnarTranspositionVisualization([FromBody]ColumnarTranspositionCipherViewModel viewModel)
        {
            ColumnarTranspositionCipher cipher = new ColumnarTranspositionCipher(viewModel.Key);

            string encrypted = "";
            string sortedKey;
            int[] columnNumbers;
            string key;

            string[] results = new string[5] { "sortedKey", "columnNumbers", "output", "input","key" };

            string input = viewModel.Message;
            input = StringHelper.ReplaceWhitespace(input, "");
            input = input.ToUpper();
            results[3] = input;
            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
                results[2] = encrypted;
                key= cipher.Key;
                key = StringHelper.ReplaceWhitespace(key, "");
                key = key.ToUpper();
                results[4] = key;
                sortedKey = new String(cipher._sortedKey);
                results[0] = sortedKey;
                columnNumbers = cipher._columnNumbers;
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < columnNumbers.Length-1; i++)
                {
                    sb.Append(columnNumbers[i]+",");
                }
                sb.Append(columnNumbers[columnNumbers.Length - 1]);
                results[1] = sb.ToString();
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(results);
        }

        #endregion ColumnarTransposition

#region Fence
        [HttpGet]
        public IActionResult Fence()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FenceEncrypt([FromBody]FenceCipherViewModel viewModel)
        {
            FenceCipher cipher = new FenceCipher(viewModel.Key);

            string encrypted = "";

            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(encrypted);
        }

        [HttpPost]
        public IActionResult FenceDecrypt([FromBody]FenceCipherViewModel viewModel)
        {
            FenceCipher cipher = new FenceCipher(viewModel.Key);

            string decrypted = "";

            try
            {
                decrypted = cipher.Decrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(decrypted);
        }

        [HttpPost]
        public IActionResult FenceVisualization([FromBody]FenceCipherViewModel viewModel)
        {
            FenceCipher cipher = new FenceCipher(viewModel.Key);
            
            string encrypted = "";
            string input = viewModel.Message;
            string[] results = new string[3] { "encrypted", "input","table"};
            input = StringHelper.ReplaceWhitespace(input, "");
            input = input.ToUpper();
            results[1] = input;
            char[][] table;
            try
            {
                string message = viewModel.Message;
                encrypted = cipher.Encrypt(message);
                results[0] = encrypted;

                table = cipher.getTable(encrypted);

                var stringTable = new StringBuilder();
                for (int i = 0; i < viewModel.Key; i++)
                {
                    for (int j = 0; j < encrypted.Length; j++)
                    {
                        stringTable.Append(table[i][j]);
                    }
                }

                results[2] = stringTable.ToString();

            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(results);
        }
        #endregion

#region Playfair
        [HttpGet]
        public IActionResult Playfair()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PlayfairEncrypt([FromBody]PlayfairCipherViewModel viewModel)
        {
            PlayfairCipher cipher = new PlayfairCipher(viewModel.Key);

            string encrypted = "";

            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(encrypted);
        }
        [HttpPost]
        public IActionResult PlayfairDecrypt([FromBody]PlayfairCipherViewModel viewModel)
        {
            PlayfairCipher cipher = new PlayfairCipher(viewModel.Key);

            string decrypted = "";

            try
            {
                decrypted = cipher.Decrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(decrypted);
        }
        [HttpPost]
        public IActionResult PlayfairVisualization([FromBody]PlayfairCipherViewModel viewModel)
        {
            PlayfairCipher cipher = new PlayfairCipher(viewModel.Key);

            string encrypted = "";
            string input = viewModel.Message;
            string[] results = new string[4] { "output", "input", "table","digrams"};
            input = StringHelper.ReplaceWhitespace(input, "");
            input = input.ToUpper();
            results[1] = input;
            char[,] table;
            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
                results[0] = encrypted;

                table = cipher.KeyMatrix;

                var stringTable = new StringBuilder();
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        stringTable.Append(table[i, j]);
                    }
                }
                results[2] = stringTable.ToString();

                if (input.Length % 2 != 0)
                {
                    input += "X";
                }
                
                results[3] = input;

            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(results);
        }
        #endregion

#region Route
        [HttpGet]
        public IActionResult Route()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RouteEncrypt([FromBody]RouteEncryptViewModel viewModel)
        {
            RouteCipher cipher = new RouteCipher(viewModel.Key,(RouteCipher.CipherMode)viewModel.Mode);

            string encrypted = "";

            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(encrypted);
            
        }


        [HttpPost]
        public IActionResult RouteDecrypt([FromBody]RouteEncryptViewModel viewModel)
        {
            RouteCipher cipher = new RouteCipher(viewModel.Key, (RouteCipher.CipherMode)viewModel.Mode);

            string decrypted = "";

            try
            {
                decrypted = cipher.Decrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(decrypted);

        }
        public IActionResult RouteVisualization([FromBody]RouteEncryptViewModel viewModel)
        {
            RouteCipher cipher = new RouteCipher(viewModel.Key, (RouteCipher.CipherMode)viewModel.Mode);

            string encrypted = "";


            string[] results = new string[3] {"output", "input","route"};
            List<int> RouteList = new List<int>();
            string input = viewModel.Message;
            input = StringHelper.ReplaceWhitespace(input, "");
            input = input.ToUpper();
            results[1] = input;
            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
                results[0] = encrypted;


                int encryptedCounter = 0;
                int direction = 0;//0-down, 1-left, 2-up, 3-right

                int columnRangeMin = 0;
                int columnRangeMax = cipher.MessageMatrix.GetLength(1) - 1;

                int rowRangeMin = 0;
                int rowRangeMax = cipher.MessageMatrix.GetLength(0) - 1;

                int steps = 0;

                while (steps < input.Length)
                {
                    if (direction == 0)
                    {
                        if (encryptedCounter >= cipher.MessageMatrix.Length)
                            break;
                        for (int i = rowRangeMin; i <= rowRangeMax; i++)
                        {
                            RouteList.Add(i);
                            RouteList.Add(columnRangeMax);
                            encryptedCounter++;
                        }

                        direction = 1;
                        columnRangeMax--;
                    }

                    if (direction == 1)
                    {
                        if (encryptedCounter >= cipher.MessageMatrix.Length)
                            break;
                        for (int i = columnRangeMax; i >= columnRangeMin; i--)
                        {
                            RouteList.Add(rowRangeMax);
                            RouteList.Add(i);
                            encryptedCounter++;
                        }


                        direction = 2;
                        rowRangeMax--;
                    }

                    if (direction == 2)
                    {
                        if (encryptedCounter >= cipher.MessageMatrix.Length)
                            break;
                        for (int i = rowRangeMax; i >= columnRangeMin; i--)
                        {
                            RouteList.Add(i);
                            RouteList.Add(columnRangeMin);
                            encryptedCounter++;
                        }


                        direction = 3;
                        columnRangeMin++;
                    }
                    if (direction == 3)
                    {
                        if (encryptedCounter >= cipher.MessageMatrix.Length)
                            break;
                        for (int i = columnRangeMin; i <= columnRangeMax; i++)
                        {
                            RouteList.Add(rowRangeMin);
                            RouteList.Add(i);
                            encryptedCounter++;
                        }

                        direction = 0;
                        rowRangeMin++;
                    }
                }
                    var stringRoute = new StringBuilder();
                    for (int i = 0; i < RouteList.Count; i++)
                    {
                        stringRoute.Append(RouteList[i]);
                        stringRoute.Append(',');
                    }
                    results[2] = stringRoute.ToString();

            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(results);
        }

        #endregion

        #region Vigenere
        [HttpGet]
        public IActionResult Vigenere()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VigenereEncrypt([FromBody]VigenereCipherViewModel viewModel)
        {
            VigenereCipher cipher = new VigenereCipher
                (viewModel.Key, Alphabets.GetAlphabet((Alphabets.AlphabetType) viewModel.AlphabetType));

            string encrypted = "";

            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(encrypted);
        }

        [HttpPost]
        public IActionResult VigenereDecrypt([FromBody]VigenereCipherViewModel viewModel)
        {
            VigenereCipher cipher = new VigenereCipher
                (viewModel.Key, Alphabets.GetAlphabet((Alphabets.AlphabetType)viewModel.AlphabetType));

            string decrypted = "";

            try
            {
                decrypted = cipher.Decrypt(viewModel.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
            }

            return Json(decrypted);
        }

        [HttpPost]
        public IActionResult VigenereVisualization([FromBody]VigenereCipherViewModel viewModel)
        {
            VigenereCipher cipher = new VigenereCipher
                       (viewModel.Key, Alphabets.GetAlphabet((Alphabets.AlphabetType)viewModel.AlphabetType));

            cipher.Alphabet = Alphabets.GetAlphabet((Alphabets.AlphabetType)viewModel.AlphabetType);

            string[] results = new string[4] { "alphabet", "output", "input","key" };
            string encrypted = "";
            string input = viewModel.Message;
            input = StringHelper.ReplaceWhitespace(input, "");
            input = input.ToUpper();
            int alphabetLength = cipher.Alphabet.Length;
            results[0] = cipher.Alphabet;
            results[2] = input;
            try
            {
                encrypted = cipher.Encrypt(viewModel.Message);
                results[1] = encrypted;
                var key = new StringBuilder();
                int keyIndex = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    key.Append(cipher.Key[keyIndex]);

                    keyIndex++;
                    if (keyIndex >= cipher.Key.Length)
                    {
                        keyIndex = 0;
                    }
                }
                results[3] = key.ToString();

            }
            catch (NullReferenceException )
            {

            }
            catch (Exception )
            {
                return BadRequest(new { Result = false, Message = Text.InvalidCharacter });
            }
            return Json(results);
        }
        #endregion

    }
}

