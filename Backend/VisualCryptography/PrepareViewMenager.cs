using System.Collections.Generic;
using CryptoWebService.Models;
using CryptoWebService.Models.VisualCryptography;

namespace CryptoWebService.Backend.VisualCryptography
{
    public  class PrepareViewMenager
    {
        public  ViewModelDto PrepareVisualCryptoraphyView()
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

        public  ViewModelDto PrepareVisualSteganographyView()
        {
            ViewModelDto VieModelDto = new ViewModelDto();

            List<AnimationDto> Animations = new List<AnimationDto>
            {
                new AnimationDto("/images/steganografia_sekret_2.png", "/images/steganografia_sekret_1.png")
                {
                    Width = 600,
                    Height = 300,
                    Amplitude = 3,
                    Period = 40000
                },
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
    }
}