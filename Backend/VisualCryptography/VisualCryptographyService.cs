using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using CryptoWebService.Models.VisualCryptography;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CryptoWebService.Backend.VisualCryptography
{
    public static class VisualCryptographyService
    {
        public static string[] DivideStringImageToSecrets(SecretsDto secretsDto)
        {
            byte[] imageBytes = Convert.FromBase64String(secretsDto.Image);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            string[] secrets ;

            if (secretsDto.MethodId == 1)
            {
                secrets = convertOnSecrets(new Bitmap(ms));
            }
            else if(secretsDto.MethodId == 2)
            {
                secrets = convertOnSecretsSquare(new Bitmap(ms));
            }
            else
            {
                throw new Exception("somethink went wrong. :C");
            }
                
            return secrets;
        }

        private static string[] convertOnSecrets(Bitmap bitmap)
        {
            Random random = new Random();
            Color colorOfspecificPixel;
            int randomValue = 0;
            List<Bitmap> Secrets = new List<Bitmap>()
            {
                new Bitmap((bitmap.Width * 2), bitmap.Height),
                new Bitmap((bitmap.Width * 2), bitmap.Height)
            };
        
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    colorOfspecificPixel = bitmap.GetPixel(j, i);
                    randomValue = random.Next(0, 2);

                    Secrets[0].SetPixel((j * 2)    , i, SubElements_ExpandingWidthMethod[randomValue].GetColor(0));
                    Secrets[0].SetPixel((j * 2) + 1, i, SubElements_ExpandingWidthMethod[randomValue].GetColor(1));

                    if (IsItBlackColor(colorOfspecificPixel))
                    {
                        Secrets[1].SetPixel((j * 2),     i, SubElements_ExpandingWidthMethod_Opposite[randomValue].GetColor(0));
                        Secrets[1].SetPixel((j * 2) + 1, i, SubElements_ExpandingWidthMethod_Opposite[randomValue].GetColor(1));
                    }
                    else if (IsItWhiteColor(colorOfspecificPixel) || IsItTransparentColor(colorOfspecificPixel))
                    {
                        Secrets[1].SetPixel((j * 2),     i, SubElements_ExpandingWidthMethod[randomValue].GetColor(0));
                        Secrets[1].SetPixel((j * 2) + 1, i, SubElements_ExpandingWidthMethod[randomValue].GetColor(1));
                    }
                    else
                    {
                        throw new Exception("Image isn't in gray scale.");
                    }
                }
            }
            return ConvertBitmapToStrings(Secrets);
        }

        private static string[] convertOnSecretsSquare(Bitmap bitmap)
        {
            Random random = new Random();
            Color colorOfspecificPixel;
            int randomValue = 0;
            List<Bitmap> Secrets = new List<Bitmap>()
            {
                new Bitmap((bitmap.Width * 2), (bitmap.Height*2)),
                new Bitmap((bitmap.Width * 2), (bitmap.Height*2))
            };
            
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    colorOfspecificPixel = bitmap.GetPixel(j, i);
                    randomValue = random.Next(0, 6);

                    Secrets[0].SetPixel((j * 2)    , (i * 2)    , SubElements_DoublesResolutionMethod[randomValue].GetColor(0));
                    Secrets[0].SetPixel((j * 2)    , (i * 2) + 1, SubElements_DoublesResolutionMethod[randomValue].GetColor(1));
                    Secrets[0].SetPixel((j * 2) + 1, (i * 2)    , SubElements_DoublesResolutionMethod[randomValue].GetColor(2));
                    Secrets[0].SetPixel((j * 2) + 1, (i * 2) + 1, SubElements_DoublesResolutionMethod[randomValue].GetColor(3));

                    if (IsItBlackColor(colorOfspecificPixel))
                    {
                        Secrets[1].SetPixel((j * 2)    , (i * 2)    , SubElements_DoublesResolutionMethod_Opposite[randomValue].GetColor(0));
                        Secrets[1].SetPixel((j * 2)    , (i * 2) + 1, SubElements_DoublesResolutionMethod_Opposite[randomValue].GetColor(1));
                        Secrets[1].SetPixel((j * 2) + 1, (i * 2)    , SubElements_DoublesResolutionMethod_Opposite[randomValue].GetColor(2));
                        Secrets[1].SetPixel((j * 2) + 1, (i * 2) + 1, SubElements_DoublesResolutionMethod_Opposite[randomValue].GetColor(3));
                    }
                    else if (IsItWhiteColor(colorOfspecificPixel) || IsItTransparentColor(colorOfspecificPixel))
                    {
                        Secrets[1].SetPixel((j * 2)    , (i * 2)    , SubElements_DoublesResolutionMethod[randomValue].GetColor(0));
                        Secrets[1].SetPixel((j * 2)    , (i * 2) + 1, SubElements_DoublesResolutionMethod[randomValue].GetColor(1));
                        Secrets[1].SetPixel((j * 2) + 1, (i * 2)    , SubElements_DoublesResolutionMethod[randomValue].GetColor(2));
                        Secrets[1].SetPixel((j * 2) + 1, (i * 2) + 1, SubElements_DoublesResolutionMethod[randomValue].GetColor(3));
                    }
                    else
                    {
                        throw new Exception("Image isn't in gray scale.");
                    }
                }
            }
            return ConvertBitmapToStrings(Secrets);
        }

        private static string[] ConvertBitmapToStrings(List<Bitmap> bitmaps)
        {
            List<string> listaString = new List<string>();

            MemoryStream ms;

            foreach (Bitmap bitmap in bitmaps)
            {
                ms = new MemoryStream();
                bitmap.Save(ms, ImageFormat.Png);
                listaString.Add(Convert.ToBase64String(ms.ToArray()));
            }

            return listaString.ToArray();
        }

        #region ColorHelpers

        private static bool IsItBlackColor(Color IsItBlack)
        {
            return (IsItBlack.A > 0 && IsItBlack.R == 0 && IsItBlack.G == 0 && IsItBlack.B == 0);
        }

        private static bool IsItWhiteColor(Color IsItWhite)
        {
            return (IsItWhite.R == 255 && IsItWhite.G == 255 && IsItWhite.B == 255);
        }

        private static bool IsItTransparentColor(Color IsItTransparent)
        {
            return (IsItTransparent.A == 0);
        }

        #endregion

        #region SubElementsConfiguration

        private static SubElement[] SubElements_ExpandingWidthMethod = new SubElement[]
        {
            new SubElement("01"),
            new SubElement("10")
        };

        private static SubElement[] SubElements_ExpandingWidthMethod_Opposite = new SubElement[]
        {
            new SubElement("10"),
            new SubElement("01")
        };

        private static SubElement[] SubElements_DoublesResolutionMethod = new SubElement[]
        {
            new SubElement("1010"),
            new SubElement("0101"),
            new SubElement("1100"),
            new SubElement("0011"),
            new SubElement("1001"),
            new SubElement("0110")
        };

        private static SubElement[] SubElements_DoublesResolutionMethod_Opposite = new SubElement[]
        {
            new SubElement("0101"),
            new SubElement("1010"),
            new SubElement("0011"),
            new SubElement("1100"),
            new SubElement("0110"),
            new SubElement("1001")
        };
        #endregion
    }
}
