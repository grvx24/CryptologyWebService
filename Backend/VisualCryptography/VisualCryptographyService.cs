﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using CryptoWebService.Models;
using CryptoWebService.Models.VisualCryptography;

namespace CryptoWebService.Backend.VisualCryptography
{
    public static class VisualCryptographyService
    {
        public static string[] DivideStringImagesToSecrets(SecretsDto secretsDto)
        {
            byte[] imageBytes = Convert.FromBase64String(secretsDto.Image);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            string[] secrets ;

            if (secretsDto.MethodId == 1)
            {
                secrets = SecretsAlgorithm1(new Bitmap(ms));
            }
            else if(secretsDto.MethodId == 2)
            {
                secrets = SecretsAlgorithm2(new Bitmap(ms));
            }
            else
            {
                throw new Exception("MethodId !=1 && != 2");
            }
                
            return secrets;
        }

        public static string[] VisualSteganography(string[] Images)
        {
            byte[] imageBytes0 = Convert.FromBase64String(Images[0]);
            byte[] imageBytes1 = Convert.FromBase64String(Images[1]);
            byte[] imageBytes2 = Convert.FromBase64String(Images[2]);

            MemoryStream ms0 = new MemoryStream(imageBytes0, 0, imageBytes0.Length);
            MemoryStream ms1 = new MemoryStream(imageBytes1, 0, imageBytes1.Length);
            MemoryStream ms2 = new MemoryStream(imageBytes2, 0, imageBytes2.Length);

            return VisualSteganographyAlgorithm(new Bitmap(ms0), new Bitmap(ms1), new Bitmap(ms2));
        }

        #region Algorithms

        private static string[] VisualSteganographyAlgorithm(Bitmap share1, Bitmap share2, Bitmap ImageToHide)
        {
            Random random = new Random();
            Color colorOfspecificPixel;
            Color share1Color;
            Color share2Color;
            int randomValue = 0;
            List<Bitmap> Secrets = new List<Bitmap>()
            {
                new Bitmap((share1.Width * 2), (share1.Height*2)),
                new Bitmap((share2.Width * 2), (share2.Height*2))
            };

            for (int i = 0; i < share1.Height; i++)
            {
                for (int j = 0; j < share1.Width; j++)
                {
                    colorOfspecificPixel = ImageToHide.GetPixel(j, i);
                    share1Color = share1.GetPixel(j, i);
                    share2Color = share2.GetPixel(j, i);

                    randomValue = random.Next(0, 2);

                    if (IsItBlackColor(colorOfspecificPixel))
                    {
                        if (IsItWhiteOrTransparent(share1Color) && IsItWhiteOrTransparent(share2Color))
                        {
                            Secrets[0].SetPixel((j * 2), (i * 2), SubElements_Black[0 + randomValue, 0] ? Color.Black : Color.Transparent);
                            Secrets[0].SetPixel((j * 2), (i * 2) + 1, SubElements_Black[0 + randomValue, 1] ? Color.Black : Color.Transparent);
                            Secrets[0].SetPixel((j * 2) + 1, (i * 2), SubElements_Black[0 + randomValue, 2] ? Color.Black : Color.Transparent);
                            Secrets[0].SetPixel((j * 2) + 1, (i * 2) + 1, SubElements_Black[0 + randomValue, 3] ? Color.Black : Color.Transparent);

                            Secrets[1].SetPixel((j * 2), (i * 2), SubElements_Black[1 - randomValue, 0] ? Color.Black : Color.Transparent);
                            Secrets[1].SetPixel((j * 2), (i * 2) + 1, SubElements_Black[1 - randomValue, 1] ? Color.Black : Color.Transparent);
                            Secrets[1].SetPixel((j * 2) + 1, (i * 2), SubElements_Black[1 - randomValue, 2] ? Color.Black : Color.Transparent);
                            Secrets[1].SetPixel((j * 2) + 1, (i * 2) + 1, SubElements_Black[1 - randomValue, 3] ? Color.Black : Color.Transparent);
                        }
                        else if ((IsItWhiteOrTransparent(share1Color) && IsItBlackColor(share2Color)) ||
                                 (IsItWhiteOrTransparent(share2Color) && IsItBlackColor(share1Color)))
                        {
                            Secrets[0].SetPixel((j * 2), (i * 2), SubElements_Black[2 + randomValue, 0] ? Color.Black : Color.Transparent);
                            Secrets[0].SetPixel((j * 2), (i * 2) + 1, SubElements_Black[2 + randomValue, 1] ? Color.Black : Color.Transparent);
                            Secrets[0].SetPixel((j * 2) + 1, (i * 2), SubElements_Black[2 + randomValue, 2] ? Color.Black : Color.Transparent);
                            Secrets[0].SetPixel((j * 2) + 1, (i * 2) + 1, SubElements_Black[2 + randomValue, 3] ? Color.Black : Color.Transparent);

                            Secrets[1].SetPixel((j * 2), (i * 2), SubElements_Black[3 - randomValue, 0] ? Color.Black : Color.Transparent);
                            Secrets[1].SetPixel((j * 2), (i * 2) + 1, SubElements_Black[3 - randomValue, 1] ? Color.Black : Color.Transparent);
                            Secrets[1].SetPixel((j * 2) + 1, (i * 2), SubElements_Black[3 - randomValue, 2] ? Color.Black : Color.Transparent);
                            Secrets[1].SetPixel((j * 2) + 1, (i * 2) + 1, SubElements_Black[3 - randomValue, 3] ? Color.Black : Color.Transparent);

                        }
                        else if (IsItBlackColor(share2Color) && IsItBlackColor(share1Color))
                        {
                            Secrets[0].SetPixel((j * 2), (i * 2), SubElements_Black[4 + randomValue, 0] ? Color.Black : Color.Transparent);
                            Secrets[0].SetPixel((j * 2), (i * 2) + 1, SubElements_Black[4 + randomValue, 1] ? Color.Black : Color.Transparent);
                            Secrets[0].SetPixel((j * 2) + 1, (i * 2), SubElements_Black[4 + randomValue, 2] ? Color.Black : Color.Transparent);
                            Secrets[0].SetPixel((j * 2) + 1, (i * 2) + 1, SubElements_Black[4 + randomValue, 3] ? Color.Black : Color.Transparent);

                            Secrets[1].SetPixel((j * 2), (i * 2), SubElements_Black[5 - randomValue, 0] ? Color.Black : Color.Transparent);
                            Secrets[1].SetPixel((j * 2), (i * 2) + 1, SubElements_Black[5 - randomValue, 1] ? Color.Black : Color.Transparent);
                            Secrets[1].SetPixel((j * 2) + 1, (i * 2), SubElements_Black[5 - randomValue, 2] ? Color.Black : Color.Transparent);
                            Secrets[1].SetPixel((j * 2) + 1, (i * 2) + 1, SubElements_Black[5 - randomValue, 3] ? Color.Black : Color.Transparent);
                        }
                        else
                        {
                            throw new ImageIsNotInGrayScaleException();
                        }
                    }
                    else if (IsItWhiteOrTransparent(colorOfspecificPixel))
                    {
                        if (IsItWhiteOrTransparent(share1Color) && IsItWhiteOrTransparent(share2Color))
                        {
                            Secrets[0].SetPixel((j * 2), (i * 2), SubElements_White[0 + randomValue, 0] ? Color.Black : Color.Transparent);
                            Secrets[0].SetPixel((j * 2), (i * 2) + 1, SubElements_White[0 + randomValue, 1] ? Color.Black : Color.Transparent);
                            Secrets[0].SetPixel((j * 2) + 1, (i * 2), SubElements_White[0 + randomValue, 2] ? Color.Black : Color.Transparent);
                            Secrets[0].SetPixel((j * 2) + 1, (i * 2) + 1, SubElements_White[0 + randomValue, 3] ? Color.Black : Color.Transparent);

                            Secrets[1].SetPixel((j * 2), (i * 2), SubElements_White[1 - randomValue, 0] ? Color.Black : Color.Transparent);
                            Secrets[1].SetPixel((j * 2), (i * 2) + 1, SubElements_White[1 - randomValue, 1] ? Color.Black : Color.Transparent);
                            Secrets[1].SetPixel((j * 2) + 1, (i * 2), SubElements_White[1 - randomValue, 2] ? Color.Black : Color.Transparent);
                            Secrets[1].SetPixel((j * 2) + 1, (i * 2) + 1, SubElements_White[1 - randomValue, 3] ? Color.Black : Color.Transparent);
                        }
                        else if ((IsItWhiteOrTransparent(share1Color) && IsItBlackColor(share2Color)) ||
                                 (IsItWhiteOrTransparent(share2Color) && IsItBlackColor(share1Color)))
                        {
                            Secrets[0].SetPixel((j * 2), (i * 2), SubElements_White[2 + randomValue, 0] ? Color.Black : Color.Transparent);
                            Secrets[0].SetPixel((j * 2), (i * 2) + 1, SubElements_White[2 + randomValue, 1] ? Color.Black : Color.Transparent);
                            Secrets[0].SetPixel((j * 2) + 1, (i * 2), SubElements_White[2 + randomValue, 2] ? Color.Black : Color.Transparent);
                            Secrets[0].SetPixel((j * 2) + 1, (i * 2) + 1, SubElements_White[2 + randomValue, 3] ? Color.Black : Color.Transparent);

                            Secrets[1].SetPixel((j * 2), (i * 2), SubElements_White[3 - randomValue, 0] ? Color.Black : Color.Transparent);
                            Secrets[1].SetPixel((j * 2), (i * 2) + 1, SubElements_White[3 - randomValue, 1] ? Color.Black : Color.Transparent);
                            Secrets[1].SetPixel((j * 2) + 1, (i * 2), SubElements_White[3 - randomValue, 2] ? Color.Black : Color.Transparent);
                            Secrets[1].SetPixel((j * 2) + 1, (i * 2) + 1, SubElements_White[3 - randomValue, 3] ? Color.Black : Color.Transparent);

                        }
                        else if (IsItBlackColor(share2Color) && IsItBlackColor(share1Color))
                        {
                            Secrets[0].SetPixel((j * 2), (i * 2), SubElements_White[4 + randomValue, 0] ? Color.Black : Color.Transparent);
                            Secrets[0].SetPixel((j * 2), (i * 2) + 1, SubElements_White[4 + randomValue, 1] ? Color.Black : Color.Transparent);
                            Secrets[0].SetPixel((j * 2) + 1, (i * 2), SubElements_White[4 + randomValue, 2] ? Color.Black : Color.Transparent);
                            Secrets[0].SetPixel((j * 2) + 1, (i * 2) + 1, SubElements_White[4 + randomValue, 3] ? Color.Black : Color.Transparent);

                            Secrets[1].SetPixel((j * 2), (i * 2), SubElements_White[5 - randomValue, 0] ? Color.Black : Color.Transparent);
                            Secrets[1].SetPixel((j * 2), (i * 2) + 1, SubElements_White[5 - randomValue, 1] ? Color.Black : Color.Transparent);
                            Secrets[1].SetPixel((j * 2) + 1, (i * 2), SubElements_White[5 - randomValue, 2] ? Color.Black : Color.Transparent);
                            Secrets[1].SetPixel((j * 2) + 1, (i * 2) + 1, SubElements_White[5 - randomValue, 3] ? Color.Black : Color.Transparent);
                        }
                        else
                        {
                            throw new ImageIsNotInGrayScaleException();
                        }
                    }
                    else
                    {
                        throw new ImageIsNotInGrayScaleException();
                    }
                }
            }
            return ConvertBitmapToStrings(Secrets);
        }

        private static string[] SecretsAlgorithm1(Bitmap bitmap)
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

                    Secrets[0].SetPixel((j * 2), i, SubElements_ExpandingWidthMethod[randomValue, 0] ? Color.Black : Color.Transparent);
                    Secrets[0].SetPixel((j * 2) + 1, i, SubElements_ExpandingWidthMethod[randomValue, 1] ? Color.Black : Color.Transparent);

                    if (IsItBlackColor(colorOfspecificPixel))
                    {
                        Secrets[1].SetPixel((j * 2), i, SubElements_ExpandingWidthMethod_Opposite[randomValue, 0] ? Color.Black : Color.Transparent);
                        Secrets[1].SetPixel((j * 2) + 1, i, SubElements_ExpandingWidthMethod_Opposite[randomValue, 1] ? Color.Black : Color.Transparent);
                    }
                    else if (IsItWhiteColor(colorOfspecificPixel) || IsItTransparentColor(colorOfspecificPixel))
                    {
                        Secrets[1].SetPixel((j * 2), i, SubElements_ExpandingWidthMethod[randomValue, 0] ? Color.Black : Color.Transparent);
                        Secrets[1].SetPixel((j * 2) + 1, i, SubElements_ExpandingWidthMethod[randomValue, 1] ? Color.Black : Color.Transparent);
                    }
                    else
                    {
                        throw new ImageIsNotInGrayScaleException();
                    }
                }
            }
            return ConvertBitmapToStrings(Secrets);
        }

        private static string[] SecretsAlgorithm2(Bitmap bitmap)
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

                    Secrets[0].SetPixel((j * 2), (i * 2), SubElements_Method[randomValue, 0] ? Color.Black : Color.Transparent);
                    Secrets[0].SetPixel((j * 2), (i * 2) + 1, SubElements_Method[randomValue, 1] ? Color.Black : Color.Transparent);
                    Secrets[0].SetPixel((j * 2) + 1, (i * 2), SubElements_Method[randomValue, 2] ? Color.Black : Color.Transparent);
                    Secrets[0].SetPixel((j * 2) + 1, (i * 2) + 1, SubElements_Method[randomValue, 3] ? Color.Black : Color.Transparent);

                    if (IsItBlackColor(colorOfspecificPixel))
                    {
                        Secrets[1].SetPixel((j * 2), (i * 2), SubElements_Method_Opposite[randomValue, 0] ? Color.Black : Color.Transparent);
                        Secrets[1].SetPixel((j * 2), (i * 2) + 1, SubElements_Method_Opposite[randomValue, 1] ? Color.Black : Color.Transparent);
                        Secrets[1].SetPixel((j * 2) + 1, (i * 2), SubElements_Method_Opposite[randomValue, 2] ? Color.Black : Color.Transparent);
                        Secrets[1].SetPixel((j * 2) + 1, (i * 2) + 1, SubElements_Method_Opposite[randomValue, 3] ? Color.Black : Color.Transparent);
                    }
                    else if (IsItWhiteColor(colorOfspecificPixel) || IsItTransparentColor(colorOfspecificPixel))
                    {
                        Secrets[1].SetPixel((j * 2), (i * 2), SubElements_Method[randomValue, 0] ? Color.Black : Color.Transparent);
                        Secrets[1].SetPixel((j * 2), (i * 2) + 1, SubElements_Method[randomValue, 1] ? Color.Black : Color.Transparent);
                        Secrets[1].SetPixel((j * 2) + 1, (i * 2), SubElements_Method[randomValue, 2] ? Color.Black : Color.Transparent);
                        Secrets[1].SetPixel((j * 2) + 1, (i * 2) + 1, SubElements_Method[randomValue, 3] ? Color.Black : Color.Transparent);
                    }
                    else
                    {
                        throw new ImageIsNotInGrayScaleException();
                    }
                }
            }
            return ConvertBitmapToStrings(Secrets);
        }

        #endregion

        #region helpers 

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

        public static ViewModelDto PrepareVisualCryptoraphyView()
        {
            ViewModelDto VieModelDto = new ViewModelDto();

            List<AnimationDto> Animations = new List<AnimationDto>
            {
                new AnimationDto("/images/1.bmp", "/images/2.bmp")
                {
                    Width = 300,
                    Height = 200,
                    Amplitude = 2,
                    Period = 20000
                },
                new AnimationDto("/images/1.bmp", "/images/2.bmp")
                {
                    Amplitude = 150
                }
            };

            List<ImageDto> Images = new List<ImageDto>
            {
                new ImageDto("/images/SimpleMethodBlackPixel.png","Kodowanie czarnego piksela")
                {
                    Width = 793,
                    Height = 200
                },
                new ImageDto("/images/SimpleMethodWhitePixel.png","Kodowanie białęgo piksela")
                {
                    Width = 991,
                    Height = 250
                }
            };
            VieModelDto.AnimationList = Animations;
            VieModelDto.ImageList = Images;

            return VieModelDto;
        }

        #endregion

        #region ColorHelpers

        private static bool IsItWhiteOrTransparent(Color color)
        {
            return (IsItWhiteColor(color) || IsItTransparentColor(color));
        }

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

        private static bool[,] SubElements_ExpandingWidthMethod = new bool[,]
        {
            { false, true },
            { true, false }
        };

        private static bool[,] SubElements_ExpandingWidthMethod_Opposite = new bool[,]
        {
            { true, false },
            { false, true }
        };

        private static bool[,] SubElements_Method = new bool[,]
        {
            { true, false, true, false },
            { false, true, false, true },
            { true, true, false, false },
            { false, false, true, true },
            { true, false, false, true },
            { false, true, true, false }
        };

        private static bool[,] SubElements_Method_Opposite = new bool[,]
        {
            { false, true, false, true },
            { true, false, true, false },
            { false, false, true, true },
            { true, true, false, false },
            { false, true, true, false },
            { true, false, false, true }
        };

        private static bool[,] SubElements_White = new bool[,]
        {
            { false, false, true, true },
            { true, false, true, false },
            { false, false, true, true },
            { true, false, true, true },
            { true, false, true, true },
            { true, false, true, true }
        };

        private static bool[,] SubElements_Black = new bool[,]
        {
            { false, false, true, true },
            { true, true, false, false },
            { false, false, true, true },
            { true, true, false, true },
            { false, true, true, true },
            { true, true, false, true }
        };

        //private static SubElement[] SubElements_Method = new SubElement[]
        //{
        //    new SubElement("1010"),
        //    new SubElement("0101"),
        //    new SubElement("1100"),
        //    new SubElement("0011"),
        //    new SubElement("1001"),
        //    new SubElement("0110")
        //};

        //private static SubElement[] SubElements_DoublesResolutionMethod_Opposite = new SubElement[]
        //{
        //    new SubElement("0101"),
        //    new SubElement("1010"),
        //    new SubElement("0011"),
        //    new SubElement("1100"),
        //    new SubElement("0110"),
        //    new SubElement("1001")
        //};
        #endregion
    }
}