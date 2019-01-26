using System.Collections.Generic;
using CryptoWebService.Models;
using CryptoWebService.Models.VisualCryptography;

namespace CryptoWebService.Backend.VisualCryptography
{
    public  class PrepareViewMenager
    {
        public  PageViewModel PrepareVisualCryptoraphyView()
        {
            PageViewModel VieModelViewModel = new PageViewModel();

            List<AnimationViewModel> Animations = new List<AnimationViewModel>
            {
                new AnimationViewModel("/images/Description/VisualCryptography/VisualCryptography/s_1.png",
                "/images/Description/VisualCryptography/VisualCryptography/s_2.png")
                {
                    Width = 200,
                    Height = 200,
                    Amplitude = 100,
                    Period = 50000
                },
                new AnimationViewModel("/images/1.bmp", "/images/2.bmp")
                {
                    Amplitude = 150
                }
            };

            List<ImageViewModel> Images = new List<ImageViewModel>
            {
                new ImageViewModel("/images/Description/VisualCryptography/VisualCryptography/SubPiksel.png","Kodowanie czarnego piksela")
                {
                    Width = 420,
                    Height = 99
                },
                new ImageViewModel("/images/Description/VisualCryptography/VisualCryptography/WhitePixel.png","Kodowanie białęgo piksela")
                {
                    Width = 742,
                    Height = 220
                },
                new ImageViewModel("/images/Description/VisualCryptography/VisualCryptography/BlackPixel.png","Kodowanie białęgo piksela")
                {
                    Width = 742,
                    Height = 220
                },
                new ImageViewModel("/images/Description/VisualCryptography/VisualCryptography/macierze.png","Kodowanie białęgo piksela")
                {
                    Width = 657,
                    Height = 112
                }
            };
            VieModelViewModel.AnimationList = Animations;
            VieModelViewModel.ImageList = Images;

            return VieModelViewModel;
        }

        public  PageViewModel PrepareVisualSteganographyView()
        {
            PageViewModel VieModelViewModel = new PageViewModel();

            List<AnimationViewModel> Animations = new List<AnimationViewModel>
            {
                new AnimationViewModel("/images/Description/VisualCryptography/steganografiawizualna/1545245985425_sekret_1.png",
                "/images/Description/VisualCryptography/steganografiawizualna/1545245985430_sekret_2.png")
                {
                    Width = 426,
                    Height = 250,
                    Amplitude = 100,
                    Period = 2500
                },
            };

            List<ImageViewModel> Images = new List<ImageViewModel>
            {
                new ImageViewModel("/images/Description/VisualCryptography/steganografiawizualna/model.png","schemat działania")
                {
                    Width = 602,
                    Height = 355
                },
                new ImageViewModel("/images/Description/VisualCryptography/steganografiawizualna/tabela.png","Kodowanie białęgo piksela")
                {
                    Width = 566,
                    Height = 447
                }
            };
            VieModelViewModel.AnimationList = Animations;
            VieModelViewModel.ImageList = Images;

            return VieModelViewModel;
        }
    }
}