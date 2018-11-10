using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.HashFunctions
{
    public class MD5_Visualization
    {
        uint A = 1732584193;
        uint B = 4023233417;
        uint C = 2562383102;
        uint D = 271733878;
        string message;
        uint[] X = new uint[16];

        public void set_X(int position, uint value)
        {
            this.X[position] = value;
        }

        public MD5_Visualization()
        {

        }


        readonly static uint[] T = new uint[64]
        {3614090360, 3905402710, 606105819, 3250441966, 4118548399, 1200080426, 2821735955, 4249261313,
         1770035416, 2336552879, 4294925233, 2304563134, 1804603682,4254626195, 2792965006, 1236535329,
         4129170786, 3225465664, 643717713, 3921069994, 3593408605, 38016083, 3634488961, 3889429448,
         568446438, 3275163606, 4107603335, 1163531501, 2850285829, 4243563512, 1735328473, 2368359562,
         4294588738, 2272392833, 1839030562, 4259657740, 2763975236, 1272893353, 4139469664, 3200236656,
         681279174, 3936430074, 3572445317, 76029189, 3654602809, 3873151461, 530742520, 3299628645,
         4096336452, 1126891415, 2878612391, 4237533241, 1700485571, 2399980690, 4293915773, 2240044497,
         1873313359, 4264355552, 2734768916, 1309151649, 4149444226, 3174756917, 718787259, 3951481745

        };

        static private uint F(uint x, uint y, uint z)
        {
            return (((x) & (y)) | ((~x) & (z)));
        }
        static private uint G(uint x, uint y, uint z)
        {
            return (((x) & (z)) | ((y) & (~z)));
        }
        static private uint H(uint x, uint y, uint z)
        {
            return ((x) ^ (y) ^ (z));
        }
        static private uint I(uint x, uint y, uint z)
        {
            return ((y) ^ ((x) | (~z)));
        }

        static private uint ROTATE_LEFT(uint x, byte n)
        {
            return (((x) << (n)) | ((x) >> (32 - (n))));
        }

        static private void FF(ref uint a, uint b, uint c, uint d, uint k, byte s, uint i, uint[] X)
        {
            uint pom = a + F(b, c, d) + X[k] + T[i - 1];
            pom = ROTATE_LEFT(pom, s);
            ulong mod = 4294967296;
            a = (uint)((ulong)(b + pom) % mod);
        }

        static private void GG(ref uint a, uint b, uint c, uint d, uint k, byte s, uint i, uint[] X)
        {
            uint pom = a + G(b, c, d) + X[k] + T[i - 1];
            pom = ROTATE_LEFT(pom, s);
            ulong mod = 4294967296;
            a = (uint)((ulong)(b + pom) % mod);
        }

        static private void HH(ref uint a, uint b, uint c, uint d, uint k, byte s, uint i, uint[] X)
        {
            uint pom = a + H(b, c, d) + X[k] + T[i - 1];
            pom = ROTATE_LEFT(pom, s);
            ulong mod = 4294967296;
            a = (uint)((ulong)(b + pom) % mod);
        }

        static private void II(ref uint a, uint b, uint c, uint d, uint k, byte s, uint i, uint[] X)
        {
            uint pom = a + I(b, c, d) + X[k] + T[i - 1];
            pom = ROTATE_LEFT(pom, s);
            ulong mod = 4294967296;
            a = (uint)((ulong)(b + pom) % mod);
        }

        protected byte[] CreatePaddedBuffer(byte[] _byteInput)
        {
            uint pad;       //no of padding bits for 448 mod 512
            byte[] bMsg;    //buffer to hold bits
            ulong sizeMsg;      //64 bit size pad
            uint sizeMsgBuff;   //buffer size in multiple of bytes
            int temp = (448 - ((_byteInput.Length * 8) % 512)); //temporary

            pad = (uint)((temp + 512) % 512);       //getting no of bits to  be pad
            if (pad == 0)               ///pad is in bits
                pad = 512;          //at least 1 or max 512 can be added

            sizeMsgBuff = (uint)((_byteInput.Length) + (pad / 8) + 8);
            sizeMsg = (ulong)_byteInput.Length * 8;
            bMsg = new byte[sizeMsgBuff];   ///no need to pad with 0 coz new bytes
            // are already initialize to 0 :)

            ////copying string to buffer
            for (int i = 0; i < _byteInput.Length; i++)
                bMsg[i] = _byteInput[i];

            bMsg[_byteInput.Length] |= 0x80;       ///making first bit of padding 1,

            //wrting the size value
            for (int i = 8; i > 0; i--)
                bMsg[sizeMsgBuff - i] = (byte)(sizeMsg >> ((8 - i) * 8) & 0x00000000000000ff);


            return bMsg;
        }

        public MD5_Visualization(string message)
        {
            this.message = message;
            set_all_X();

        }
    

        void set_all_X()
        {
            byte[] msg = ASCIIEncoding.ASCII.GetBytes(message);
            byte[] paddedbuffer = CreatePaddedBuffer(msg);
            string[] str = new string[4];
            StringBuilder strBld = new StringBuilder();
            int counter = 0;
            int s = 0;
            for (int i = 0; i < paddedbuffer.Length; i++)
            {
                str[counter] = (Int32.Parse(Convert.ToString(paddedbuffer[i], 2)).ToString("00000000"));
                counter++;
                if (counter > 3)
                {
                    strBld.Append(str[3]);
                    strBld.Append(str[2]);
                    strBld.Append(str[1]);
                    strBld.Append(str[0]);
                    uint number = (uint)Convert.ToInt32(strBld.ToString(), 2);
                    set_X(s, number);
                    strBld.Length = 0;
                    s++;
                    counter = 0;
                }
            }
        }

        public int Get_MessageLength(string msg)
        {
            int messageLength = msg.Length * 8;
            return messageLength;
        }

        public int Get_PaddedBits(string msg)
        {

            int messageLength = Get_MessageLength(msg);
            int paddedBits = 448 - messageLength;
            int modulo = 448;
            if (messageLength > 448)
            {
                do
                {
                    modulo = modulo + 512;
                } while (messageLength >= modulo);
            }

            if (messageLength == modulo)
            {
                paddedBits = 512;
            }
            else
            {
                paddedBits = modulo - messageLength;
            }

            return paddedBits;

        }

        public string Get_binary(string msg)
        {
            byte[] msg_byte = ASCIIEncoding.ASCII.GetBytes(msg);
            byte[] paddedbuffer = CreatePaddedBuffer(msg_byte);
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < paddedbuffer.Length; i++)
            {
                strBuilder.Append(Int32.Parse(Convert.ToString(paddedbuffer[i], 2)).ToString("00000000"));
            }
            return AddSeparator(strBuilder.ToString(), 8, ' ');
        }

        public string Get_binary_length(string msg)
        {
            byte[] msg_byte = ASCIIEncoding.ASCII.GetBytes(msg);
            byte[] paddedbuffer = CreatePaddedBuffer(msg_byte);
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < paddedbuffer.Length; i++)
            {
                strBuilder.Append(Int32.Parse(Convert.ToString(paddedbuffer[i], 2)).ToString("00000000"));
            }
            int begin = strBuilder.Length;
            string length = strBuilder.ToString().Remove(0, begin - 64);
            return AddSeparator(length, 8, ' ');
        }

        public static string AddSeparator(string str, int chunk_len, char separator)
        {
            StringBuilder builder = new StringBuilder();
            for (var index = 0; index < str.Length; index += chunk_len)
            {
                builder.Append(str, index, chunk_len);
                builder.Append(separator);
            }
            return builder.ToString();
        }

        public string StringToBinary(string msg)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in msg.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }

            return AddSeparator(sb.ToString(), 8, ' ');
        }

    }
}
