using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CryptoWebService.Backend.HashFunctions.Keccak
{
    /// <summary>
    /// Implements a bitstring, a sequence of bits of specific length. It can also be seen as a sequence of blocks of
    /// fixed length whose last block can be shorter.
    /// </summary>
    /// <remarks>
    /// <para><b>Design choices:</b></para>
    /// <para>This class is sealed on purpose, mainly for performance reasons. It is probably best to avoid any virtual
    /// table.</para>
    /// <para>This class implements the <see cref="IEquatable{T}"/> interface. Two distinct instances with same internal
    /// data and length will be considered as equal.</para>
    /// <para>This class implements the <see cref="IEnumerable{T}"/> for <see cref="bool"/>. A bitstring is able to
    /// provide an enumerator for its bits sequence.</para>
    /// </remarks>
    public sealed class Bitstring : IEquatable<Bitstring>, IEnumerable<bool>
    {
        #region Constants

        /// <summary>
        /// The number of bits per block in bitstrings.
        /// </summary>
        public const int BlockBitSize = 8;

        /// <summary>
        /// The number of bytes per block in bitstrings.
        /// </summary>
        public const int BlockByteSize = BlockBitSize >> Shift;

        /// <summary>
        /// The base-2 logarithm of <see cref="BlockBitSize"/>.
        /// </summary>
        public const int Shift = 3;

        #endregion

        #region Fields

        private static readonly Regex _bitstringRegex
            = new Regex(@"^[01\s]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private byte[] _data;
        private int _length;

        #endregion

        #region Static properties

        /// <summary>
        /// Gets a new bitstring composed of a single one.
        /// </summary>
        public static Bitstring One
        {
            get { return new Bitstring("1", 1); }
        }

        /// <summary>
        /// Gets a new bitstring composed of a single zero.
        /// </summary>
        public static Bitstring Zero
        {
            get { return new Bitstring("0", 1); }
        }

        #endregion

        #region Instance properties

        /// <summary>
        /// Gets the number of blocks of <see cref="BlockBitSize"/> of this bitstring.
        /// </summary>
        public int BlockCount
        {
            get { return _data.Length; }
        }

        /// <summary>
        /// Gets the byte array which hold the bits of this bitstring.
        /// </summary>
        public byte[] Bytes
        {
            get { return _data; }
        }

        /// <summary>
        /// Gets the number of bits in this bitstring.
        /// </summary>
        public int Length
        {
            get { return _length; }
        }

        /// <summary>
        /// Gets or sets the bit at specified index in this bitstring.
        /// </summary>
        /// <param name="index">The index in this bitstring of the bit to get or set.</param>
        /// <returns>The bit at <paramref name="index"/> in this bitstring.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is lower than zero, or
        /// <paramref name="index"/> is greater than or equal to <see cref="Length"/>.</exception>
        public bool this[int index]
        {
            get
            {
                if ((index < 0) || (index >= _length))
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                int blockIndex = index >> Shift;
                int bitOffset = index % BlockBitSize;
                byte chunk = _data[blockIndex];
                byte mask = (byte)(1 << bitOffset);
                return ((chunk & mask) == mask);
            }
            set
            {
                if ((index < 0) || (index >= _length))
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                int blockIndex = index >> Shift;
                int bitOffset = index % BlockBitSize;
                byte chunk = _data[blockIndex];
                byte mask = (byte)(1 << bitOffset);
                if (value)
                {
                    _data[blockIndex] |= mask;
                }
                else
                {
                    _data[blockIndex] &= (byte)(~mask & 0xFF);
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new empty <see cref="Bitstring"/>.
        /// </summary>
        public Bitstring()
            : this(0) { }

        /// <summary>
        /// Creates a new <see cref="Bitstring"/> specifying its length in bits.
        /// </summary>
        /// <param name="length">The number of bits in the bitstring.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is lower than zero.</exception>
        public Bitstring(int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            _length = length;
            _data = new byte[(int)Math.Ceiling((double)_length / BlockBitSize)];
        }

        /// <summary>
        /// Creates a new <see cref="Bitstring"/> from an exising one.
        /// </summary>
        /// <param name="bitstring">The bitstring to copy.</param>
        /// <exception cref="ArgumentNullException"><paramref name="bitstring"/> is null.</exception>
        public Bitstring(Bitstring bitstring)
        {
            bitstring = bitstring ?? throw new ArgumentNullException(nameof(bitstring));
            int count = bitstring.BlockCount;
            _data = new byte[count];
            Array.Copy(bitstring._data, 0, _data, 0, count);
            _length = bitstring._length;
        }

        /// <summary>
        /// Creates a new <see cref="Bitstring"/> from an enumeration of bits.
        /// </summary>
        /// <param name="bits">The bits which compose the bitstring.</param>
        /// <exception cref="ArgumentNullException"><paramref name="bits"/> is null.</exception>
        public Bitstring(IEnumerable<bool> bits)
        {
            bits = bits ?? throw new ArgumentNullException(nameof(bits));
            _data = Parse(bits, out _length);
        }

        /// <summary>
        /// Creates a new <see cref="Bitstring"/> from a binary string representation and optional length.
        /// </summary>
        /// <param name="bits">The binary string representation of the bitstring. Must contain only ones, zeroes and
        /// whitespaces.</param>
        /// <param name="length">The length of the bitstring. If no value is specified, or if a negative value is
        /// specified, the length will be determined by <paramref name="bits"/>.</param>
        /// <exception cref="ArgumentException"><paramref name="bits"/> is null, or <paramref name="bits"/> is not a valid
        /// binary string representation.</exception>
        public Bitstring(string bits, int length = -1)
        {
            if (ValidateAndSanitize(ref bits, ref length))
            {
                int count = (int)Math.Ceiling((double)length / BlockBitSize);
                _data = new byte[count];
                int left = bits.Length;
                int i;
                // Stop the loop either when there is less than 8 bits to parse, or when desired length exceeds the size of
                // specified string.
                for (i = 0; (left >= BlockBitSize) && ((i << Shift) < length); i++)
                {
                    _data[i] = ParseByte(bits.Substring(i << Shift, BlockBitSize));
                    left -= BlockBitSize;
                }
                if (left > 0)
                {
                    _data[i] = ParseByte(bits.Substring(i << Shift, left));
                }
                _length = length;
            }
            else
            {
                throw new ArgumentException("Invalid bitstring representation.", nameof(bits));
            }
        }

        /// <summary>
        /// Creates a new <see cref="Bitstring"/> from a byte array and a length.
        /// </summary>
        /// <param name="data">The byte array which holds the bits of the bitstring.</param>
        /// <param name="length">The length of the bitstring. Must be consistent with <paramref name="data"/>. If no value
        /// is specified, or if a negative value is specified, the length is that of <paramref name="data"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is greater than the number of bits in
        /// <paramref name="data"/>.</exception>
        public Bitstring(byte[] data, int length = -1)
        {
            // Sanity checks
            data = data ?? throw new ArgumentNullException(nameof(data));
            int count = data.Length;
            int bitCount = count << Shift;
            _length = (length < 0) ? bitCount : length;
            if (_length > bitCount)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            // If the full range of bits is to be considered, whole process is a lot simpler.
            if (_length != bitCount)
            {
                // How many blocks will we need?
                count = (int)Math.Ceiling((double)_length / BlockBitSize);
                Array.Resize(ref data, count);
                // If the last block is not full, zero the trailing bits which do not belong to the bitstring.
                int remaining = _length % BlockBitSize;
                if (remaining > 0)
                {
                    data[count - 1] &= Bin.LowerMask(remaining);
                }
            }
            _data = data;
        }

        #endregion

        #region Operators

        /// <summary>
        /// <see cref="Bitstring"/> equality operator.
        /// </summary>
        /// <param name="lhs">The bitstring to compare to <paramref name="rhs"/>.</param>
        /// <param name="rhs">The bitstring to compare to <paramref name="lhs"/>.</param>
        /// <returns>True if <paramref name="lhs"/> and <paramref name="rhs"/> have same length and internal data,
        /// otherwise false.
        /// </returns>
        public static bool operator ==(Bitstring lhs, Bitstring rhs)
        {
            return (ReferenceEquals(lhs, rhs) || (!ReferenceEquals(lhs, null) && lhs.Equals(rhs)));
        }

        /// <summary>
        /// <see cref="Bitstring"/> inequality operator.
        /// </summary>
        /// <param name="lhs">The bitstring to compare to <paramref name="rhs"/>.</param>
        /// <param name="rhs">The bitstring to compare to <paramref name="lhs"/>.</param>
        /// <returns>True if <paramref name="lhs"/> and <paramref name="rhs"/> differ either by their length or internal
        /// data, otherwise false.
        public static bool operator !=(Bitstring lhs, Bitstring rhs)
        {
            return (!ReferenceEquals(lhs, rhs) && (ReferenceEquals(lhs, null) || !lhs.Equals(rhs)));
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Returns a new bitstring which is composed exclusively of ones and has specified length.
        /// </summary>
        /// <param name="length">The length of the bitstring.</param>
        /// <returns>A new bitstring composed exclusively of ones and which has <paramref name="length"/> bits.</returns>
        public static Bitstring Ones(int length)
        {
            return (length > 0) ? new Bitstring(new string('1', length)) : new Bitstring();
        }

        /// <summary>
        /// Returns a bitstring composed of random bits.
        /// </summary>
        /// <param name="random">The random source used to generate the bitstring.</param>
        /// <param name="length">The length of the bitstring.</param>
        /// <returns>A bitstring whose bits have been generated with <paramref name="random"/> and which has
        /// <paramref name="length"/>.</returns>
        public static Bitstring Random(Random random, int length)
        {
            int count = (int)Math.Ceiling((double)length / BlockBitSize);
            byte[] data = new byte[count];
            random.NextBytes(data);
            int left = length % BlockBitSize;
            if (left != 0)
            {
                data[count - 1] &= Bin.LowerMask(left);
            }
            return new Bitstring(data, length);
        }

        /// <summary>
        /// Returns a new bitstring which is composed exclusively of zeroes and has specified length.
        /// </summary>
        /// <param name="length">The length of the bitstring.</param>
        /// <returns>A new bitstring composed exclusively of zeroes and which has <paramref name="length"/> bits.
        /// </returns>
        public static Bitstring Zeroes(int length)
        {
            return (length > 0) ? new Bitstring(length) : new Bitstring();
        }

        #endregion

        #region Instance methods

        /// <summary>
        /// Performs a bitwise and operation between this bitstring and equal-sized specified one, and returns the result.
        /// </summary>
        /// <param name="other">The equal-sized bitstring to and with this bitstring.</param>
        /// <returns>This bitstring which has been bitwise and'ed with <paramref name="other"/>.</returns>
        /// <remarks>This method modifies current instance and returns it, so that calls can eventually be chained.
        /// </remarks>
        public Bitstring And(Bitstring other)
        {
            return (other.Length == _length) ? And(other._data) : this;
        }

        /// <summary>
        /// Performs a bitwise and operation between this bitstring and specified byte array, and returns the result.
        /// </summary>
        /// <param name="data">The byte array to and with internal array of this bitstring.</param>
        /// <returns>This bitstring which has been bitwise and'ed with the bits in <paramref name="data"/>.</returns>
        public Bitstring And(byte[] data)
        {
            int count = BlockCount;
            if ((data != null) && (data.Length == count))
            {
                for (int i = 0; i < count; i++)
                {
                    _data[i] &= data[i];
                }
            }
            return this;
        }

        /// <summary>
        /// Appends specified byte array to the end of this bitstring and returns the result.
        /// </summary>
        /// <param name="bytes">The byte array to add to the end of this bitstring.</param>
        /// <returns>This bitstring to which <paramref name="bytes"/> have been appended.</returns>
        /// <remarks>This method modifies current instance and returns it, so that calls can eventually be chained.
        /// </remarks>
        public Bitstring Append(byte[] bytes)
        {
            if (bytes != null)
            {
                if ((_length % BlockBitSize) == 0)
                { // Array copy if aligned data
                    int count = bytes.Length;
                    int oldCount = BlockCount;
                    Array.Resize(ref _data, oldCount + count);
                    Array.Copy(bytes, 0, _data, oldCount, count);
                    _length += count << Shift;
                }
                else
                {                               // Enumeration if unaligned data
                    return Append(new Bitstring(bytes));
                }
            }
            return this;
        }

        /// <summary>
        /// Appends specified bits enumeration to the end of this bitstring and returns the result.
        /// </summary>
        /// <param name="bits">The enumeration of bits to add to the end of this bitstring.</param>
        /// <returns>This bitstring to which has been appended <paramref name="bits"/>.</returns>
        /// <remarks>This method modifies current instance and returns it, so that calls can eventually be chained.
        /// </remarks>
        public Bitstring Append(IEnumerable<bool> bits)
        {
            int count = bits?.Count() ?? 0;
            if (count > 0)
            {
                int blockIndex = _length >> Shift;
                int bitOffset = _length % BlockBitSize;
                _length += count;
                int newBlockCount = (int)Math.Ceiling((double)_length / BlockBitSize);
                if (newBlockCount > BlockCount)
                {
                    Array.Resize(ref _data, newBlockCount);
                }
                foreach (bool bit in bits)
                {
                    if (bit)
                    {
                        _data[blockIndex] |= (byte)(1 << bitOffset);
                    }
                    if (++bitOffset > 7)
                    {
                        bitOffset = 0;
                        blockIndex++;
                    }
                }
            }
            return this;
        }

        /// <summary>
        /// Resets all bits of this bitstring to zero. The length is preserved.
        /// </summary>
        public void Clear()
        {
            int count = BlockCount;
            for (int i = 0; i < count; i++)
            {
                _data[i] = 0;
            }
        }

        /// <summary>
        /// Returns whether specified bitstring equals this bitstring.
        /// </summary>
        /// <param name="other">The bitstring to compare to this bitstring.</param>
        /// <returns>True if this bitstring and <paramref name="other"/> have same length and internal data, otherwise
        /// false.</returns>
        public bool Equals(Bitstring other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return ((_length == other._length) && Enumerable.SequenceEqual(_data, other._data));
        }

        /// <summary>
        /// Returns whether specified object equals this bitstring.
        /// </summary>
        /// <param name="obj">The object to compare to this bitstring.</param>
        /// <returns>True if <paramref name="obj"/> is a bitstring with same length and internal data as this bitstring,
        /// otherwise false.</returns>
        public override bool Equals(object obj)
        {
            return ((obj is Bitstring) && Equals((Bitstring)obj));
        }

        /// <summary>
        /// Returns an enumerator for the bits in this bitstring.
        /// </summary>
        /// <returns>An enumerator for the bits in this bitstring.</returns>
        public IEnumerator<bool> GetEnumerator()
        {
            IEnumerable<bool> bits = GetBits();
            return bits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns a hash code for this bitstring.
        /// </summary>
        /// <returns>A hash code for this bitstring.</returns>
        public override int GetHashCode()
        {
            return HashCoder<int>.Boost.Compute(_length, HashCoder<byte>.Boost.Compute(_data));
        }

        /// <summary>
        /// Returns whether specified index is a valid index in this bitstring.
        /// </summary>
        /// <param name="index">The index to check.</param>
        /// <returns>True if <paramref name="index"/> is greater than negative one and lower than the length of this
        /// bitstring, otherwise false.</returns>
        public bool IsValidIndex(int index)
        {
            return (index > -1) && (index < _length);
        }

        /// <summary>
        /// Performs a bitwise or operation between this bitstring and equal-sized specified one, and returns the result.
        /// </summary>
        /// <param name="other">The equal-sized bitstring to or with this bitstring.</param>
        /// <returns>This bitstring which has been bitwise or'ed with <paramref name="other"/>.</returns>
        /// <remarks>This method modifies current instance and returns it, so that calls can eventually be chained.
        /// </remarks>
        public Bitstring Or(Bitstring other)
        {
            return (other.Length == _length) ? Or(other._data) : this;
        }

        /// <summary>
        /// Performs a bitwise or operation between this bitstring and specified byte array, and returns the result.
        /// </summary>
        /// <param name="data">The byte array to or with internal array of this bitstring.</param>
        /// <returns>This bitstring which has been bitwise or'ed with the bits in <paramref name="data"/>.</returns>
        public Bitstring Or(byte[] data)
        {
            int count = BlockCount;
            if ((data != null) && (data.Length == count))
            {
                for (int i = 0; i < count; i++)
                {
                    _data[i] |= data[i];
                }
            }
            return this;
        }

        /// <summary>
        /// Prepends specified byte array to the start of this bitstring and returns the result.
        /// </summary>
        /// <param name="bytes">The byte array to add to the start of this bitstring.</param>
        /// <returns>This bitstring to which has been prepended <paramref name="bits"/>.</returns>
        /// <remarks>This method modifies current instance and returns it, so that calls can eventually be chained.
        /// </remarks>
        public Bitstring Prepend(byte[] bytes)
        {
            if (bytes != null)
            {
                int count = bytes.Length;
                int oldCount = BlockCount;
                Array.Resize(ref _data, oldCount + count);
                Array.Copy(_data, 0, _data, count, oldCount);
                Array.Copy(bytes, 0, _data, 0, count);
                _length += count << Shift;
            }
            return this;
        }

        /// <summary>
        /// Prepends specified bits enumeration to the start of this bitstring and returns the result.
        /// </summary>
        /// <param name="bits">The enumeration of bits to add to the start of this bitstring.</param>
        /// <returns>This bitstring to which has been prepended <paramref name="bits"/>.</returns>
        /// <remarks>This method modifies current instance and returns it, so that calls can eventually be chained.
        /// </remarks>
        public Bitstring Prepend(IEnumerable<bool> bits)
        {
            Bitstring copy = new Bitstring(this);
            int count = bits?.Count() ?? 0;
            if (count > 0)
            {
                _length += count;
                int newBlockCount = (int)Math.Ceiling((double)_length / BlockBitSize);
                if (newBlockCount > BlockCount)
                {
                    Array.Resize(ref _data, newBlockCount);
                }
                int blockIndex = 0;
                int bitOffset = 0;
                foreach (bool bit in bits)
                {
                    if (bit)
                    {
                        _data[blockIndex] |= (byte)(1 << bitOffset);
                    }
                    else
                    {
                        _data[blockIndex] &= (byte)~(1 << bitOffset);
                    }
                    if (++bitOffset > 7)
                    {
                        bitOffset = 0;
                        blockIndex++;
                    }
                }
                foreach (bool bit in copy)
                {
                    if (bit)
                    {
                        _data[blockIndex] |= (byte)(1 << bitOffset);
                    }
                    else
                    {
                        _data[blockIndex] &= (byte)~(1 << bitOffset);
                    }
                    if (++bitOffset > 7)
                    {
                        bitOffset = 0;
                        blockIndex++;
                    }
                }
            }
            return this;
        }

        /// <summary>
        /// Returns the substring of this bitstring which starts at specified index and has specified length.
        /// </summary>
        /// <param name="index">The index in this bitstring where the substring starts.</param>
        /// <param name="length">The length of the substring starting at <paramref name="index"/>.</param>
        /// <returns>The substring which starts at <paramref name="index"/> and has <paramref name="length"/>.</returns>
        public Bitstring Substring(int index, int length)
        {
            if (IsValidIndex(index))
            {
                if (((index % BlockBitSize) == 0) && ((length % BlockBitSize) == 0))
                {
                    int count = length >> Shift;
                    byte[] data = new byte[count];
                    Array.Copy(_data, index >> Shift, data, 0, count);
                    return new Bitstring(data);
                }
                else
                {
                    return new Bitstring(this.Skip(index).Take(length));
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        /// <summary>
        /// Swaps bits at specified indices and returns this bitstring.
        /// </summary>
        /// <param name="lhs">The index of the bit to swap with the bit at <paramref name="rhs"/>.</param>
        /// <param name="rhs">The index of the bit to swap with the bit at <paramref name="lhs"/>.</param>
        /// <returns>This bitstring with swapped indices.</returns>
        /// <remarks>If either <paramref name="lhs"/> or <paramref name="rhs"/> is not a valid index in this bitstring, or
        /// if <paramref name="lhs"/> equals <paramref name="rhs"/>, this methods does notthing.</remarks>
        public Bitstring SwapBits(int lhs, int rhs)
        {
            if (IsValidIndex(lhs) && IsValidIndex(rhs) && (lhs != rhs))
            {
                // Swapping two values by xoring them
                // http://graphics.stanford.edu/~seander/bithacks.html#SwappingValuesXOR
                this[lhs] ^= this[rhs];
                this[rhs] ^= this[lhs];
                this[lhs] ^= this[rhs];
            }
            return this;
        }

        /// <summary>
        /// Returns the binary representation of this bitstring.
        /// </summary>
        /// <param name="spacing">The number of binary characters in one group. When set to zero, no spacing is done.
        /// </param>
        /// <returns>The string of binary flags representing this bitstring.</returns>
        public string ToBinString(int spacing = 0)
        {
            spacing = Math.Max(0, spacing);
            int length = _length + ((spacing > 0) ? (int)Math.Ceiling((double)_length / spacing) - 1 : 0);
            StringBuilder builder = new StringBuilder(length);
            int i = 0;
            for (; i < BlockCount - 1; i++)
            {
                PrintByte(builder, _data[i]);
            }
            int left = _length % BlockBitSize;
            PrintByte(builder, _data[i], (left == 0) ? 8 : left);
            if (spacing > 0)
            {
                AddSpacing(builder, spacing);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Returns the hexadecimal representation of this bitstring.
        /// </summary>
        /// <param name="spacing">Whether a whitespace should be inserted between each byte.</param>
        /// <param name="uppercase">Whether hexadecimal numbers should be printed uppercase.</param>
        /// <returns>The hexadecimal representation of this bitstring.</returns>
        public string ToHexString(bool spacing = true, bool uppercase = true)
        {
            int count = BlockCount << 1;
            int last = BlockCount - 1;
            int length = count + (spacing ? last : 0);
            StringBuilder builder = new StringBuilder(length);
            count >>= 1;
            for (int i = 0; i < count; i++)
            {
                builder.Append(_data[i].ToString((uppercase) ? "X2" : "x2"));
                if (spacing && (i < last))
                {
                    builder.Append(' ');
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Returns a string representation of this bitstring.
        /// </summary>
        /// <returns>The hexadecimal string representation of this bistring.</returns>
        public override string ToString()
        {
            return $"({_length}) {ToHexString()}";
        }

        /// <summary>
        /// Returns a new bitstring which contains specified number of first bits of this bitstring.
        /// </summary>
        /// <param name="length">The number of first bits of this bitstring to truncate.</param>
        /// <returns>A new bitstring wich contains the <paramref name="length"/> first bits of this bitstring.</returns>
        public Bitstring Truncate(int length)
        {
            length = Math.Min(_length, Math.Max(0, length));
            int count = (int)Math.Ceiling((double)length / BlockBitSize);
            byte[] data = new byte[count];
            Array.Copy(_data, 0, data, 0, count);
            int left = length % BlockBitSize;
            if (left != 0)
            {
                data[count - 1] &= Bin.LowerMask(left);
            }
            return new Bitstring(data, length);
        }

        /// <summary>
        /// Performs a bitwise xor operation between this bitstring and equal-sized specified one, and returns the result.
        /// </summary>
        /// <param name="other">The equal-sized bitstring to xor with this bitstring.</param>
        /// <returns>This bitstring which has been bitwise xor'ed with <paramref name="other"/>.</returns>
        /// <remarks>This method modifies current instance and returns it, so that calls can eventually be chained.
        /// </remarks>
        public Bitstring Xor(Bitstring other)
        {
            return (other.Length == _length) ? Xor(other._data) : this;
        }

        /// <summary>
        /// Performs a bitwise xor operation between this bitstring and specified byte array, and returns the result.
        /// </summary>
        /// <param name="data">The byte array to xor with internal array of this bitstring.</param>
        /// <returns>This bitstring which has been bitwise xor'ed with the bits in <paramref name="data"/>.</returns>
        public Bitstring Xor(byte[] data)
        {
            int count = BlockCount;
            if ((data != null) && (data.Length == count))
            {
                for (int i = 0; i < count; i++)
                {
                    _data[i] ^= data[i];
                }
            }
            return this;
        }

        #endregion

        #region Private implementation

        private static void AddSpacing(StringBuilder builder, int spacing)
        {
            int index = spacing;
            while (index < builder.Length)
            {
                builder.Insert(index, ' ');
                index += spacing + 1;
            }
        }

        private IEnumerable<bool> GetBits()
        {
            for (int i = 0; i < _length; i++)
            {
                yield return this[i];
            }
        }

        private static byte[] Parse(IEnumerable<bool> bits, out int length)
        {
            List<byte> bytes = new List<byte>(200);
            byte value = 0;
            int index = 0;
            bool add = true;
            length = 0;
            foreach (bool bit in bits)
            {
                length++;
                if (bit)
                {
                    value |= (byte)(1 << index);
                }
                if (++index > 7)
                {
                    index = 0;
                    bytes.Add(value);
                    value = 0;
                    add = false;
                }
                else if (!add)
                {
                    add = true;
                }
            }
            if (add)
            {
                bytes.Add(value);
            }
            return bytes.ToArray();
        }

        private static byte ParseByte(string chunk)
        {
            byte result = 0;
            int length = chunk.Length;
            for (int i = 0; i < length; i++)
            {
                if (chunk[i] == '1')
                {
                    result |= (byte)(1 << i);
                }
            }
            return result;
        }

        private static void PrintByte(StringBuilder builder, byte value, int length = 8)
        {
            length = Math.Max(0, Math.Min(8, length));
            for (int i = 0; i < length; i++)
            {
                builder.Append(((value & (1 << i)) != 0) ? '1' : '0');
            }
        }

        private static bool ValidateAndSanitize(ref string bits, ref int length)
        {
            return (ValidateBits(ref bits) && ValidateLength(ref length, bits.Length));
        }

        private static bool ValidateBits(ref string bits)
        {
            bool ok = (bits != null);
            if (ok)
            {
                ok = _bitstringRegex.IsMatch(bits);
                if (ok && bits.Contains(" "))
                {
                    bits = bits.Replace(" ", "");
                }
            }
            return ok;
        }

        private static bool ValidateLength(ref int length, int stringLength)
        {
            if (length < 0)
            {
                length = stringLength;
            }
            return true;
        }

        #endregion
    }
}
