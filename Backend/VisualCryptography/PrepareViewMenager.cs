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
                new AnimationViewModel("/images/1.bmp", "/images/2.bmp")
                {
                    Width = 300,
                    Height = 200,
                    Amplitude = 2,
                    Period = 20000
                },
                new AnimationViewModel("/images/1.bmp", "/images/2.bmp")
                {
                    Amplitude = 150
                }
            };

            List<ImageViewModel> Images = new List<ImageViewModel>
            {
                new ImageViewModel("/images/SimpleMethodBlackPixel.png","Kodowanie czarnego piksela")
                {
                    Width = 793,
                    Height = 200
                },
                new ImageViewModel("/images/SimpleMethodWhitePixel.png","Kodowanie białęgo piksela")
                {
                    Width = 991,
                    Height = 250
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
                new AnimationViewModel("/images/steganografia_sekret_2.png", "/images/steganografia_sekret_1.png")
                {
                    Width = 600,
                    Height = 300,
                    Amplitude = 3,
                    Period = 40000
                },
            };

            List<ImageViewModel> Images = new List<ImageViewModel>
            {
                new ImageViewModel("/images/SimpleMethodBlackPixel.png","Kodowanie czarnego piksela")
                {
                    Width = 793,
                    Height = 200
                },
                new ImageViewModel("/images/SimpleMethodWhitePixel.png","Kodowanie białęgo piksela")
                {
                    Width = 991,
                    Height = 250
                }
            };
            VieModelViewModel.AnimationList = Animations;
            VieModelViewModel.ImageList = Images;

            return VieModelViewModel;
        }
    }
}