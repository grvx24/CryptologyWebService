using CryptoWebService.Models.VisualCryptography;
using System.Collections.Generic;

namespace CryptoWebService.Models
{
    public class ViewModelDto
    {
        public List<AnimationDto> AnimationList { get; set; }
        public List<ImageDto> ImageList { get; set; }
    }
}