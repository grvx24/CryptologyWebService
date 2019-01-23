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


        private string LSBFind(SteganographyLsbViewModel data)
        {
            byte[] imageBytes = Convert.FromBase64String(data.Image);

            Bitmap bitmap = new Bitmap(new MemoryStream(imageBytes, 0, imageBytes.Length));

            var _RedBTC = new List<bool>(data.RedBits.Select(c => c == '1').ToList());
            var _GreenBTC = new List<bool>(data.GreenBits.Select(c => c == '1').ToList());
            var _BlueBTC = new List<bool>(data.BlueBits.Select(c => c == '1').ToList());
            int bitsOnCoding = _RedBTC.Where(c => c).Count() + _GreenBTC.Where(c => c).Count() + _BlueBTC.Where(c => c).Count();

            int maxAmountOfBitsToHide = (bitmap.Width * bitmap.Height * bitsOnCoding);

            int amountOfBitsToHide = data.TextToHide.Length * 8;

            if (amountOfBitsToHide > maxAmountOfBitsToHide)
            {
                throw new IndexOutOfRangeException();
            }

            PixelFormat x = bitmap.PixelFormat;


            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);

            return Convert.ToBase64String(ms.ToArray());
        }

        private string LSBCode(SteganographyLsbViewModel data)
        {
            return "nothink found";
        }  
    }
}