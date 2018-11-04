using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CryptoWebService.Backend.VisualCryptography
{
    public static class VisualCryptographyService
    {
        public static string[] DivideStringImageToSecrets(string image)
        {
            byte[] imageBytes = Convert.FromBase64String(image);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            string[] secrets = convertOnSecrets(new Bitmap(ms));

            return secrets;
        }

        private static string[] convertOnSecrets(Bitmap bitmap)
        {
            Bitmap secret1 = new Bitmap((bitmap.Width * 2), bitmap.Height);
            Bitmap secret2 = new Bitmap((bitmap.Width * 2), bitmap.Height);
            Random random = new Random();

            int kolor = 0;
            int kolor2 = 0;

            Color transparentColor = Color.FromArgb(0, 255, 255, 255);

            Color colorOfspecificPixel;
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    colorOfspecificPixel = bitmap.GetPixel(j, i);
                    int randomValue = random.Next(0, 100);

                    if (colorOfspecificPixel.A > 0 && colorOfspecificPixel.R == 0 && colorOfspecificPixel.G == 0 && colorOfspecificPixel.B == 0)
                    {
                        kolor++;
                        if (randomValue >= 50)
                        {
                            secret1.SetPixel((j * 2), i, transparentColor);
                            secret1.SetPixel((j * 2) + 1, i, Color.Black);
                            secret2.SetPixel((j * 2), i, Color.Black);
                            secret2.SetPixel((j * 2) + 1, i, transparentColor);
                        }
                        else
                        {
                            secret1.SetPixel(j * 2, i, Color.Black);
                            secret1.SetPixel(j * 2 + 1, i, transparentColor);
                            secret2.SetPixel(j * 2, i, transparentColor);
                            secret2.SetPixel(j * 2 + 1, i, Color.Black);
                        }
                    }
                    else if ((colorOfspecificPixel.R == 255 && colorOfspecificPixel.G == 255 && colorOfspecificPixel.B == 255 )|| colorOfspecificPixel.A == 0)
                    {
                        kolor2++;
                        if (randomValue >= 50)
                        {
                            secret1.SetPixel(j * 2, i, transparentColor);
                            secret1.SetPixel(j * 2 + 1, i, Color.Black);
                            secret2.SetPixel(j * 2, i, transparentColor);
                            secret2.SetPixel(j * 2 + 1, i, Color.Black);
                        }
                        else
                        {
                            secret1.SetPixel(j * 2, i, Color.Black);
                            secret1.SetPixel(j * 2 + 1, i, transparentColor);
                            secret2.SetPixel(j * 2, i, Color.Black);
                            secret2.SetPixel(j * 2 + 1, i, transparentColor);
                        }
                    }
                    else
                    {
                        throw new Exception("Image isn't in gray scale.");
                    }
                }
            }
            List<string> listaString = new List<string>();

            MemoryStream ms = new MemoryStream();
            MemoryStream ms2 = new MemoryStream();
            secret1.Save(ms, ImageFormat.Png);
            secret2.Save(ms2, ImageFormat.Png);

            listaString.Add(Convert.ToBase64String(ms.ToArray()));
            listaString.Add(Convert.ToBase64String(ms2.ToArray()));

            return listaString.ToArray();
        }
    }
}
