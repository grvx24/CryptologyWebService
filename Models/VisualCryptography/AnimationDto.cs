namespace CryptoWebService.Models.VisualCryptography
{
    public class AnimationDto
    {
        public AnimationDto(string staticImage, string movingImage, int width, int height)
        {
            this.StaticImage = staticImage;
            this.MovingImage = movingImage;
            this.Width = width;
            this.Height = height;
        }

        public string StaticImage { get; set; }

        public string MovingImage { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
