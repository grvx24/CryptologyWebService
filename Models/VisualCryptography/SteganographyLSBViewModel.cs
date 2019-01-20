namespace CryptoWebService.Models.VisualCryptography
{
    public class SteganographyLsbViewModel
    {
        public int PurposeId { get; set; }
        public string Image { get; set; }
        public string GreenBits { get; set; }
        public string RedBits { get; set; }
        public string BlueBits { get; set; }
        public string TextToHide { get; set; }
    }
}
