namespace CryptoWebService.Models.VisualCryptography
{
    public class SteganographyLsbDto
    {
        public string Image { get; set; }
        public string GreenBits { get; set; }
        public string RedBits { get; set; }
        public string BlueBits { get; set; }
        public string TextToHide { get; set; }
    }
}
