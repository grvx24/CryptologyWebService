using System;

namespace CryptoWebService.Models.VisualCryptography
{
    public class QuestionViewModel 
    {
        public QuestionViewModel(string staticImage, string movingImage)
        {
            this.StaticImage = staticImage;
            this.MovingImage = movingImage;
            this.Width = 500;
            this.Height = 300;
            this.Amplitude = 50;
            this.Period = 2000;
            this.BorderWidth = 2;
            this.UnicNumber = Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        public string UnicNumber { get; }

        public string StaticImage { get; set; }

        public string MovingImage { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Amplitude { get; set; }

        public int Period { get; set; }

        public int TotalWidth {
            get{
                return Width + (Amplitude * 2);
            }
        }

        public int TotalHeightWithBorders
        {
            get
            {
                return Height + (BorderWidth * 2);
            }
        }

        public int TotalWidthWithBorders
        {
            get
            {
                return TotalWidth + (BorderWidth * 2);
            }
        }

        public int BorderWidth { get; set; }

    }
}
