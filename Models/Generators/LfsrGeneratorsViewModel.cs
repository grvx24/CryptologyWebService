using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Models.Generators
{
    public enum SeriesMode
    {
        Binary,
        Hex,
        Base64
    }

    public class LfsrGeneratorsViewModel
    {
        public string[] Registers { get; set; }
        public int Length { get; set; }
        public int Mode { get; set; }
        public string[] FeedbackFunctions { get; set; }
        public int? K_value { get; set; }
        public int? D_value { get; set; }
    }
}
