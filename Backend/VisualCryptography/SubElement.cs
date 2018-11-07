using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.VisualCryptography
{
    public class SubElement
    {
        private bool[] Element;

        public SubElement(string value)
        {
            this.Element = value.Select(c => c == '1').ToArray();
        }

        public Color GetColor(int index)
        {
            return Element[index] ? Color.Black : Color.Transparent;
        }
    }
}
