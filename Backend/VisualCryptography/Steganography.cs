using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using CryptoWebService.Models.VisualCryptography;

namespace CryptoWebService.Backend.VisualCryptography
{
    public class Steganography
    {

        public string LsbMethod(SteganographyLsbViewModel data)
        {   
            if(data.PurposeId == 1)
            {
                return LSBCode(data);
            }
            else if (data.PurposeId == 2)
            {
                return LSBFind(data);
            }
            else
            {
                throw new Exception("Unknow PurposeId");
            }
        }


        private string LSBCode(SteganographyLsbViewModel data)
        {
            byte[] imageBytes = Convert.FromBase64String(data.Image);

            Bitmap bitmap = new Bitmap(new MemoryStream(imageBytes, 0, imageBytes.Length));

            var _RedBTC = new List<bool>(data.RedBits.Select(c => c == '1').ToList());
            var _GreenBTC = new List<bool>(data.GreenBits.Select(c => c == '1').ToList());
            var _BlueBTC = new List<bool>(data.BlueBits.Select(c => c == '1').ToList());
            //int[] rIndex = data.RedBits.Select()
            int rBits = _RedBTC.Where(c => c).Count();
            int gBits = _GreenBTC.Where(c => c).Count();
            int bBits = _BlueBTC.Where(c => c).Count();
            int bitsOnCoding = rBits + gBits + bBits;

            int maxAmountOfBitsToHide = (bitmap.Width * bitmap.Height * bitsOnCoding);

            int amountOfBitsToHide = data.TextToHide.Length * 8;

            if (amountOfBitsToHide > maxAmountOfBitsToHide)
            {
                throw new IndexOutOfRangeException();
            }

            PixelFormat x = bitmap.PixelFormat;

            if(x != PixelFormat.Format24bppRgb && x != PixelFormat.Format32bppArgb)
            {
                throw new PictureInBadFormatException();
            }

            Color currentPixel;
            byte currentRed;
            byte currentGreen;
            byte currentBlue;
            byte mask;

            byte[] DataToCodeBytes = Encoding.ASCII.GetBytes(data.TextToHide);
            BitArray _DATA_TO_CODE = new BitArray(DataToCodeBytes);
            int _CODING_INDEXER = 0;
            

            for (int i = 0; i < bitmap.Height && _CODING_INDEXER < _DATA_TO_CODE.Length; i++)
            {
                for (int j = 0; j < bitmap.Width && _CODING_INDEXER < _DATA_TO_CODE.Length; j++)
                {
                    currentPixel = bitmap.GetPixel(j, i);
                    currentRed   = currentPixel.R;
                    currentGreen = currentPixel.G;
                    currentBlue  = currentPixel.B;

                    for (int r_iterator = 0; r_iterator < rBits && _CODING_INDEXER < _DATA_TO_CODE.Length; r_iterator++)
                    {
                        mask = (byte)(1 << bitInByteIndex);
                        currentRed
                        var xaa = _DATA_TO_CODE[_CODING_INDEXER];
                        _CODING_INDEXER++;
                    }
                    for (int g_iterator = 0; g_iterator < gBits && _CODING_INDEXER < _DATA_TO_CODE.Length; g_iterator++)
                    {
                        
                        var xaa = _DATA_TO_CODE[_CODING_INDEXER];
                        _CODING_INDEXER++;
                    }
                    for (int b_iterator = 0; b_iterator < bBits && _CODING_INDEXER < _DATA_TO_CODE.Length; b_iterator++)
                    {
                        
                        var xaa = _DATA_TO_CODE[_CODING_INDEXER];
                        _CODING_INDEXER++;
                    }
                    bitmap.SetPixel(j, i, Color.FromArgb(currentPixel.A,currentRed,currentGreen,currentBlue));
                }
            }

            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);

            return Convert.ToBase64String(ms.ToArray());
        }

        private string LSBFind(SteganographyLsbViewModel data)
        {
            byte[] imageBytes = Convert.FromBase64String(data.Image);

            Bitmap bitmap = new Bitmap(new MemoryStream(imageBytes, 0, imageBytes.Length));

            var _RedBTC = new List<bool>(data.RedBits.Select(c => c == '1').ToList());
            var _GreenBTC = new List<bool>(data.GreenBits.Select(c => c == '1').ToList());
            var _BlueBTC = new List<bool>(data.BlueBits.Select(c => c == '1').ToList());
            int bitsOnCoding = _RedBTC.Where(c => c).Count() + _GreenBTC.Where(c => c).Count() + _BlueBTC.Where(c => c).Count();

            PixelFormat x = bitmap.PixelFormat;

            if (x != PixelFormat.Format24bppRgb && x != PixelFormat.Format32bppArgb)
            {
                throw new PictureInBadFormatException();
            }

            return "nothink found";
        }

        private byte[] PackBoolsInByteArray(bool[] bools)
        {
            int len = bools.Length;
            int bytes = len >> 3;
            if ((len & 0x07) != 0) ++bytes;
            byte[] arr2 = new byte[bytes];
            for (int i = 0; i < bools.Length; i++)
            {
                if (bools[i])
                    arr2[i >> 3] |= (byte)(1 << (i & 0x07));
            }
        }
    }
}