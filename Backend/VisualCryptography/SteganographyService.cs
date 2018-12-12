using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using CryptoWebService.Models.VisualCryptography;

namespace CryptoWebService.Backend.VisualCryptography
{
    public static class SteganographyService
    {
        public static string LsbMethod(SteganographyLsbDto data)
        {

            //int amountOfBitsOnCoding = Int32.Parse(_AmountOfBitsOnCoding);
            //int bitsOnCoding = (_Image.Width * _Image.Height * amountOfBitsOnCoding);
            //int space = _TextToHide.Length * 8;
            //if(space > bitsOnCoding)
            //{
            //    throw new IndexOutOfRangeException();
            //}

            //PixelFormat x = _Image.PixelFormat;

            //var colorOfspecificPixel = _Image.GetPixel(0, 0);
            //BitArray BitsToHide = new BitArray(Encoding.ASCII.GetBytes(_TextToHide));

            //for (int bitIndexMsg = 0, i = 0, j = 0; bitIndexMsg < space;)
            //{
            //    var gra = BitsToHide[bitIndexMsg];
            //    var color = _Image.GetPixel(i,j);
            //    byte r = color.R;
            //    byte g = color.G;
            //    byte b = color.B;
            //    for (int bitIndex = 7;  bitIndex >( 8 - amountOfBitsOnCoding); bitIndex--)
            //    {
            //        byte mask = (byte)(1 << bitIndex);

            //        //self[byteIndex] ^= mask;
            //        bitIndexMsg = bitIndexMsg + 3;
            //    }

            //    _Image.SetPixel(i, j, Color.FromArgb(r, g, b));


            //    bitIndexMsg++;
            //    j = bitIndexMsg % _Image.Width;
            //    i = (j == 0 ? i + 1 : i);
            //}

            //MemoryStream ms = new MemoryStream();
            //_Image.Save(ms, ImageFormat.Png);

            //return Convert.ToBase64String(ms.ToArray());
            return "elo";
        }

        public static string PatchWorkMethod(SteganographyPatchWorkDto data)
        {

            return "elo";
        }

        //private static string LeastSignificantBitMethod(Bitmap _Image, string _AmountOfBitsOnCoding, string _TextToHide)
        //{
        //    int amountOfBitsOnCoding = Int32.Parse(_AmountOfBitsOnCoding);
        //    int bitsOnCoding = (_Image.Width * _Image.Height * amountOfBitsOnCoding);
        //    int space = _TextToHide.Length * 8;
        //    if (space > bitsOnCoding)
        //    {
        //        throw new IndexOutOfRangeException();
        //    }

        //    PixelFormat x = _Image.PixelFormat;

        //    var colorOfspecificPixel = _Image.GetPixel(0, 0);
        //    BitArray BitsToHide = new BitArray(Encoding.ASCII.GetBytes(_TextToHide));

        //    for (int bitIndexMsg = 0, i = 0, j = 0; bitIndexMsg < space;)
        //    {
        //        var gra = BitsToHide[bitIndexMsg];
        //        var color = _Image.GetPixel(i, j);
        //        byte r = color.R;
        //        byte g = color.G;
        //        byte b = color.B;
        //        for (int bitIndex = 7; bitIndex > (8 - amountOfBitsOnCoding); bitIndex--)
        //        {
        //            byte mask = (byte)(1 << bitIndex);

        //            //self[byteIndex] ^= mask;
        //            bitIndexMsg = bitIndexMsg + 3;
        //        }

        //        _Image.SetPixel(i, j, Color.FromArgb(r, g, b));


        //        bitIndexMsg++;
        //        j = bitIndexMsg % _Image.Width;
        //        i = (j == 0 ? i + 1 : i);
        //    }

        //    MemoryStream ms = new MemoryStream();
        //    _Image.Save(ms, ImageFormat.Png);

        //    return Convert.ToBase64String(ms.ToArray());

        //}
    }
}