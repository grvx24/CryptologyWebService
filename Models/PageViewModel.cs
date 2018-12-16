using CryptoWebService.Models.VisualCryptography;
using System.Collections.Generic;

namespace CryptoWebService.Models
{
    public class PageViewModel
    {
        public List<AnimationViewModel> AnimationList { get; set; }
        public List<ImageViewModel> ImageList { get; set; }
    }
}