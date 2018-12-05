namespace CryptoWebService.Models.VisualCryptography
{
    public class SteganographyDto
    {
        public string MethodId { get; set; }
        public string Image { get; set; }
        public string AmountOfBitsValue { get; set; }
        public string GeneratorKey { get; set; }
        public string NaturalNumber { get; set; }
        public string TextToHide { get; set; }
    }
}
