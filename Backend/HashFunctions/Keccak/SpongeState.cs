using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.HashFunctions.Keccak
{
    /// <summary>
    /// Implements a valuetype which represents the size of a sponge state.
    /// </summary>
    public struct SpongeSize
    {
        /// <summary>
        /// A <see cref="SpongeSize"/> with <see cref="B"/> = 25, <see cref="W"/> = 1 and <see cref="L"/> = 0.
        /// </summary>
        public static readonly SpongeSize W01 = new SpongeSize(25);

        /// <summary>
        /// A <see cref="SpongeSize"/> with <see cref="B"/> = 50, <see cref="W"/> = 2 and <see cref="L"/> = 1.
        /// </summary>
        public static readonly SpongeSize W02 = new SpongeSize(50);

        /// <summary>
        /// A <see cref="SpongeSize"/> with <see cref="B"/> = 100, <see cref="W"/> = 4 and <see cref="L"/> = 2.
        /// </summary>
        public static readonly SpongeSize W04 = new SpongeSize(100);

        /// <summary>
        /// A <see cref="SpongeSize"/> with <see cref="B"/> = 200, <see cref="W"/> = 8 and <see cref="L"/> = 3.
        /// </summary>
        public static readonly SpongeSize W08 = new SpongeSize(200);

        /// <summary>
        /// A <see cref="SpongeSize"/> with <see cref="B"/> = 400, <see cref="W"/> = 16 and <see cref="L"/> = 4.
        /// </summary>
        public static readonly SpongeSize W16 = new SpongeSize(400);

        /// <summary>
        /// A <see cref="SpongeSize"/> with <see cref="B"/> = 800, <see cref="W"/> = 32 and <see cref="L"/> = 5.
        /// </summary>
        public static readonly SpongeSize W32 = new SpongeSize(800);

        /// <summary>
        /// A <see cref="SpongeSize"/> with <see cref="B"/> = 1600, <see cref="W"/> = 64 and <see cref="L"/> = 6.
        /// </summary>
        public static readonly SpongeSize W64 = new SpongeSize(1600);

        private readonly int _b;

        /// <summary>
        /// Gets the total number of bits in the sponge state.
        /// </summary>
        public int B
        {
            get { return _b; }
        }

        /// <summary>
        /// Gets the base-2 logarithm of <see cref="W"/>.
        /// </summary>
        public int L
        {
            get { return Bin.Log2(W); }
        }

        /// <summary>
        /// Gets <see cref="B"/> divided by 25. It is the depth of the sponge state.
        /// </summary>
        public int W
        {
            get { return _b / 25; }
        }

        internal SpongeSize(int b)
        {
            _b = b;
        }

        /// <summary>
        /// Returns a string representation of this sponge size.
        /// </summary>
        /// <returns>A string representation of this sponge size.</returns>
        public override string ToString()
        {
            return $"B={B}, W={W}, L={L}";
        }
    }

    /// <summary>
    /// Implements an object which allows to store and perform operations on sponge constructions.
    /// </summary>
    public sealed class SpongeState
    {
        /// <summary>
        /// Defines a delegate which is used to perform operations on bits of a sponge state.
        /// </summary>
        /// <param name="x">The sheet coordinate of the bit on which operating.</param>
        /// <param name="y">The plane coordinate of the bit on which operating.</param>
        /// <param name="z">The slice coordinate of the bit on which operating.</param>
        /// <param name="bit">The bit value to use to perform operation on cell at <paramref name="x"/>,
        /// <paramref name="y"/> and <paramref name="z"/>.</param>
        /// <returns>The resulting bit of the operation of <paramref name="bit"/> with cell at <paramref name="x"/>,
        /// <paramref name="y"/> and <paramref name="z"/>.</returns>
        private delegate bool OperationDelegate(int x, int y, int z, bool bit);

        #region Fields

        private readonly int _rate;
        private readonly SpongeSize _size;

        private Bitstring _bitstring;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the bitstring holding the bits of this state.
        /// </summary>
        /// <exception cref="ArgumentException">A null value is provided to the setter, or a bitstring is provided whose
        /// length does not match the width of this state.</exception>
        public Bitstring Bitstring
        {
            get { return _bitstring; }
            set
            {
                if ((value == null) || (value.Length != _size.B))
                {
                    throw new ArgumentException($"Invalid bitstring length {value} instead of {_size.B}", nameof(value));
                }
                _bitstring = value;
            }
        }

        /// <summary>
        /// Gets the capacity of this state, i.e. the number of bits which are not impacted while absorbing messages.
        /// </summary>
        public int Capacity
        {
            get { return _size.B - _rate; }
        }

        /// <summary>
        /// Gets the rate of this state, i.e. the number of bits which are impacted while absorbing messages.
        /// </summary>
        public int Rate
        {
            get { return _rate; }
        }

        /// <summary>
        /// Gets the size of the sponge state, i.e. the total number of bits it contains.
        /// </summary>
        public SpongeSize Size
        {
            get { return _size; }
        }

        /// <summary>
        /// Gets or sets the value of the bit at specified index in underlying bitstring.
        /// </summary>
        /// <param name="index">The index in the bitstring of the bit whose value to get or set.</param>
        /// <returns>The value of the bit at <paramref name="index"/> in underlying bitstring.</returns>
        public bool this[int index]
        {
            get { return _bitstring[index]; }
            set { _bitstring[index] = value; }
        }

        /// <summary>
        /// Gets or sets the value of the bit at specified postion in this sponge state.
        /// </summary>
        /// <param name="x">The sheet index of the bit whose value to get or set.</param>
        /// <param name="y">The plane index of the bit whose value to get or set.</param>
        /// <param name="z">The slice index of the bit whose value to get or set.</param>
        /// <returns>The value of the bit at [<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>] in this
        /// sponge state.</returns>
        public bool this[int x, int y, int z]
        {
            get { return this[GetIndex(x, y, z)]; }
            set { this[GetIndex(x, y, z)] = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="SpongeState"/> from an existing one.
        /// </summary>
        /// <param name="state">The sponge state to copy.</param>
        public SpongeState(SpongeState state)
        {
            _size = state._size;
            _rate = state._rate;
            _bitstring = new Bitstring(state._bitstring);
        }

        /// <summary>
        /// Creates a new <see cref="SpongeState"/> specifying its size and rate.
        /// </summary>
        /// <param name="size">The size of the sponge state.</param>
        /// <param name="rate">The rate of the sponge state, i.e. the number of bits which are impacted while absorbing
        /// messages.</param>
        /// <exception cref="ArgumentException"><paramref name="rate"/> is lower than one, or <paramref name="rate"/> is
        /// greater than or equal to <paramref name="size"/>'s <see cref="SpongeSize.B"/>.</exception>
        /// <remarks>
        /// <para>The capacity of the state, i.e. the number of bits which are not impacted while absorbing messages, will
        /// be <paramref name="size"/>.B - <paramref name="rate"/>.</para>
        /// </remarks>
        public SpongeState(SpongeSize size, int rate)
        {
            int b = size.B;
            if ((rate < 1) || (rate >= b))
            {
                throw new ArgumentException($"Invalid rate {rate} for width {b}.", nameof(rate));
            }
            _size = size;
            _rate = rate;
            _bitstring = Bitstring.Zeroes(b);
        }

        /// <summary>
        /// Creates a new <see cref="SpongeState"/> from an existing bitstring and specified rate.
        /// </summary>
        /// <param name="bitstring">The bitstring which holds the bits of the sponge state.</param>
        /// <param name="rate">The number of bits which are impacted while aborbing messages.</param>
        /// <exception cref="ArgumentNullException"><paramref name="bitstring"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="bitstring"/> is empty, or <paramref name="rate"/> is lower
        /// than one, or <paramref name="rate"/> is greater than or equal to <paramref name="size"/>'s
        /// <see cref="SpongeSize.B"/>.</exception>
        /// <remarks>
        /// <para>The capacity of the state, i.e. the number of bits which are not impacted while absorbing messages, will
        /// be <see cref="bitstring"/>.Length - <paramref name="rate"/>.</para>
        /// </remarks>
        public SpongeState(Bitstring bitstring, int rate)
        {
            _bitstring = bitstring ?? throw new ArgumentNullException(nameof(bitstring));
            int length = _bitstring.Length;
            if (length < 1)
            {
                throw new ArgumentException("Bitstring cannot be empty.", nameof(bitstring));
            }
            _size = new SpongeSize(length);
            if ((rate < 1) || (rate >= _size.B))
            {
                throw new ArgumentException($"Invalid rate {rate} for width {_size.B}.", nameof(rate));
            }
            _rate = rate;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Clears this sponge state to zero.
        /// </summary>
        public void Clear()
        {
            _bitstring.Clear();
        }

        /// <summary>
        /// Enumerates the bits in this sponge state.
        /// </summary>
        /// <returns>An enumeration of the bits in this sponge state.</returns>
        public IEnumerable<bool> GetBits()
        {
            return _bitstring;
        }

        /// <summary>
        /// Returns the index in underlying bitstring of specified bit.
        /// </summary>
        /// <param name="bit">The bit whose index in underlying bitstring to get.</param>
        /// <returns>The index of <paramref name="bit"/> in underlying bitstring.</returns>
        public int GetIndex(Bit bit)
        {
            return GetIndex(bit.X, bit.Y, bit.Z);
        }

        /// <summary>
        /// Gets the index in underlying bitstring of the bit with specified coordinates.
        /// </summary>
        /// <param name="x">The sheet index of the bit.</param>
        /// <param name="y">The plane index of the bit.</param>
        /// <param name="z">The slice index of the bit.</param>
        /// <returns>The index in underlying bitstring of the bit at [<paramref name="x"/>, <paramref name="y"/>,
        /// <paramref name="z"/>].</returns>
        public int GetIndex(int x, int y, int z)
        {
            return _size.W * (5 * y + x) + z;
        }

        /// <summary>
        /// Returns the bitstring representation of the bitstring in this sponge state.
        /// </summary>
        /// <param name="spacing">The number of binary characters in one group. When set to zero, no spacing is done.
        /// </param>
        /// <returns>The string of binary flags representing the bitstring in this sponge state.</returns>
        public string ToBinString(int spacing = 0)
        {
            return _bitstring.ToBinString(spacing);
        }

        /// <summary>
        /// Returns the hexadecimal representation of the bitstring in this sponge state.
        /// </summary>
        /// <param name="spacing">Whether a whitespace should be inserted between each byte.</param>
        /// <param name="uppercase">Whether hexadecimal numbers should be printed uppercase.</param>
        /// <returns>The hexadecimal representation of the bitstring in this sponge state.</returns>
        public string ToHexString(bool spacing = true, bool uppercase = true)
        {
            return _bitstring.ToHexString(spacing, uppercase);
        }

        /// <summary>
        /// Returns a string representation of this sponge state.
        /// </summary>
        /// <returns>A string representation of this sponge state.</returns>
        public override string ToString()
        {
            return $"State ({_size}): {_bitstring.ToHexString()}";
        }

        #region State elements

        /// <summary>
        /// Returns the bit corresponding to specified index in underlying bitstring.
        /// </summary>
        /// <param name="index">The index in underlying bitstring of the bit to get.</param>
        /// <returns>The bit at <paramref name="index"/> in underlying bitstring of this sponge state.</returns>
        public Bit GetBit(int index)
        {
            // index = _size.W * (5 * y + x) + z
            int w = _size.W;
            int wCount = index / w;
            // wCount = 5 * y + x
            int y = wCount / 5;
            int x = wCount - 5 * y;
            int z = Bin.Mod(index, w);
            return new Bit(this, x, y, z);
        }

        /// <summary>
        /// Returns the column at specified position in this sponge state.
        /// </summary>
        /// <param name="x">The sheet index of the column.</param>
        /// <param name="z">The slice index of the column.</param>
        /// <returns>The column at sheet <paramref name="x"/> and slice <paramref name="z"/> in this sponge state.
        /// </returns>
        public Column GetColumn(int x, int z)
        {
            return new Column(this, x, z);
        }

        /// <summary>
        /// Enumerates the columns of this sponge state.
        /// </summary>
        /// <returns>An enumeration of the columns of this sponge state.</returns>
        public IEnumerable<Column> GetColumns()
        {
            int w = _size.W;
            for (int x = 0; x < 5; x++)
            {
                for (int z = 0; z < w; z++)
                {
                    yield return new Column(this, x, z);
                }
            }
        }

        /// <summary>
        /// Returns the lane at specified position in this sponge state.
        /// </summary>
        /// <param name="x">The sheet index of the lane.</param>
        /// <param name="y">The plane index of the lane.</param>
        /// <returns>The lane at sheet <paramref name="x"/> and plane <paramref name="y"/> in this sponge state.</returns>
        public Lane GetLane(int x, int y)
        {
            return new Lane(this, x, y);
        }

        /// <summary>
        /// Enumerates the lanes of this sponge state.
        /// </summary>
        /// <returns>An enumeration of the lanes of this sponge state.</returns>
        public IEnumerable<Lane> GetLanes()
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    yield return new Lane(this, x, y);
                }
            }
        }

        /// <summary>
        /// Returns the plane at specified index in this sponge state.
        /// </summary>
        /// <param name="y">The index of the plane.</param>
        /// <returns>The plane at <paramref name="y"/> in this state.</returns>
        public Plane GetPlane(int y)
        {
            return new Plane(this, y);
        }

        /// <summary>
        /// Enumerates the planes of this sponge state.
        /// </summary>
        /// <returns>An enumeration of the planes of this sponge state.</returns>
        public IEnumerable<Plane> GetPlanes()
        {
            for (int y = 0; y < 5; y++)
            {
                yield return new Plane(this, y);
            }
        }

        /// <summary>
        /// Returns the row at specified position in this sponge state.
        /// </summary>
        /// <param name="y">The plane index of the row.</param>
        /// <param name="z">The slice index of the row.</param>
        /// <returns>The row at plane <paramref name="y"/> and slice <paramref name="z"/> in this sponge state.</returns>
        public Row GetRow(int y, int z)
        {
            return new Row(this, y, z);
        }

        /// <summary>
        /// Enumerates the rows of this sponge state.
        /// </summary>
        /// <returns>An enumeration of the rows of this sponge state.</returns>
        public IEnumerable<Row> GetRows()
        {
            int w = _size.W;
            for (int y = 0; y < 5; y++)
            {
                for (int z = 0; z < w; z++)
                {
                    yield return new Row(this, y, z);
                }
            }
        }

        /// <summary>
        /// Returns the sheet at specified index in this sponge state.
        /// </summary>
        /// <param name="x">The index of the sheet.</param>
        /// <returns>The plane at <paramref name="x"/> in this sponge state.</returns>
        public Sheet GetSheet(int x)
        {
            return new Sheet(this, x);
        }

        /// <summary>
        /// Enumerates the sheets of this sponge state.
        /// </summary>
        /// <returns>An enumeration of the sheets of this sponge state.</returns>
        public IEnumerable<Sheet> GetSheets()
        {
            for (int x = 0; x < 5; x++)
            {
                yield return new Sheet(this, x);
            }
        }

        /// <summary>
        /// Returns the slice at specified index in this sponge state.
        /// </summary>
        /// <param name="z">The index of the slice.</param>
        /// <returns>The slice at <paramref name="z"/> in this sponge state.</returns>
        public Slice GetSlice(int z)
        {
            return new Slice(this, z);
        }

        /// <summary>
        /// Enumerates the slices of this sponge state.
        /// </summary>
        /// <returns>An enumeration of the slices of this sponge state.</returns>
        public IEnumerable<Slice> GetSlices()
        {
            int w = _size.W;
            for (int z = 0; z < w; z++)
            {
                yield return new Slice(this, z);
            }
        }

        #endregion

        #region Modification

        /// <summary>
        /// Sets the bits of specified column to specified values.
        /// </summary>
        /// <param name="column">The column whose bits to set.</param>
        /// <param name="bits">An enumeration of new values for the bits of <paramref name="column"/>.</param>
        public void SetColumn(Column column, IEnumerable<bool> bits)
        {
            ColumnOperation((x, y, z, bit) => { return bit; }, column, bits);
        }

        /// <summary>
        /// Sets the bits of columns using specified enumeration of bits.
        /// </summary>
        /// <param name="bits">An enumeration of new values for columns bits.</param>
        public void SetColumns(IEnumerable<bool> bits)
        {
            ColumnsOperation((x, y, z, bit) => { return bit; }, bits);
        }

        /// <summary>
        /// Sets the bits of specified lane to specified values.
        /// </summary>
        /// <param name="lane">The lane whose bits to set.</param>
        /// <param name="bits">An enumeration of new values for the bits of <paramref name="lane"/>.</param>
        public void SetLane(Lane lane, IEnumerable<bool> bits)
        {
            LaneOperation((x, y, z, bit) => { return bit; }, lane, bits);
        }

        /// <summary>
        /// Sets the bits of lanes using specified enumeration of bits.
        /// </summary>
        /// <param name="bits">An enumeration of new values for lanes bits.</param>
        public void SetLanes(IEnumerable<bool> bits)
        {
            LanesOperation((x, y, z, bit) => { return bit; }, bits);
        }

        /// <summary>
        /// Sets the bits of specified plane to specified values.
        /// </summary>
        /// <param name="plane">The plane whose bits to set.</param>
        /// <param name="bits">An enumeration of new values for the bits of <paramref name="plane"/>.</param>
        public void SetPlane(Plane plane, IEnumerable<bool> bits)
        {
            PlaneOperation((x, y, z, bit) => { return bit; }, plane, bits);
        }

        /// <summary>
        /// Sets the bits of planes using specified enumeration of bits.
        /// </summary>
        /// <param name="bits">An enumeration of new values for planes bits.</param>
        public void SetPlanes(IEnumerable<bool> bits)
        {
            PlanesOperation((x, y, z, bit) => { return bit; }, bits);
        }

        /// <summary>
        /// Sets the bits of specified row to specified values.
        /// </summary>
        /// <param name="row">The row whose bits to set.</param>
        /// <param name="bits">An enumeration of new values for the bits of <paramref name="row"/>.</param>
        public void SetRow(Row row, IEnumerable<bool> bits)
        {
            RowOperation((x, y, z, bit) => { return bit; }, row, bits);
        }

        /// <summary>
        /// Sets the bits of rows using specified enumeration of bits.
        /// </summary>
        /// <param name="bits">An enumeration of new values for rows bits.</param>
        public void SetRows(IEnumerable<bool> bits)
        {
            RowsOperation((x, y, z, bit) => { return bit; }, bits);
        }

        /// <summary>
        /// Sets the bits of specified sheet to specified values.
        /// </summary>
        /// <param name="sheet">The sheet whose bits to set.</param>
        /// <param name="bits">An enumeration of new values for the bits of <paramref name="sheet"/>.</param>
        public void SetSheet(Sheet sheet, IEnumerable<bool> bits)
        {
            SheetOperation((x, y, z, bit) => { return bit; }, sheet, bits);
        }

        /// <summary>
        /// Sets the bits of sheets using specified enumeration of bits.
        /// </summary>
        /// <param name="bits">An enumeration of new values for sheets bits.</param>
        public void SetSheets(IEnumerable<bool> bits)
        {
            SheetsOperation((x, y, z, bit) => { return bit; }, bits);
        }

        /// <summary>
        /// Sets the bits of specified slice to specified values.
        /// </summary>
        /// <param name="slice">The slice whose bits to set.</param>
        /// <param name="bits">An enumeration of new values for the bits of <paramref name="slice"/>.</param>
        public void SetSlice(Slice slice, IEnumerable<bool> bits)
        {
            SliceOperation((x, y, z, bit) => { return bit; }, slice, bits);
        }

        /// <summary>
        /// Sets the bits of slices using specified enumeration of bits.
        /// </summary>
        /// <param name="bits">An enumeration of new values for slices bits.</param>
        public void SetSlices(IEnumerable<bool> bits)
        {
            SlicesOperation((x, y, z, bit) => { return bit; }, bits);
        }

        /// <summary>
        /// Performs a bitwwise xor operation with specified byte array.
        /// </summary>
        /// <param name="data">The byte array to xor with.</param>
        public void Xor(byte[] data)
        {
            _bitstring.Xor(data);
        }

        /// <summary>
        /// Performs a bitwise xor operation on specified column with specified bits.
        /// </summary>
        /// <param name="column">The column whose bits to xor with <paramref name="bits"/>.</param>
        /// <param name="bits">An enumeration of bits to xor with bits of <paramref name="column"/>.</param>
        public void XorColumn(Column column, IEnumerable<bool> bits)
        {
            ColumnOperation((x, y, z, bit) => { return this[x, y, z] ^ bit; }, column, bits);
        }

        /// <summary>
        /// Performs a bitwise xor operation on columns with specified bits.
        /// </summary>
        /// <param name="bits">The bits with which performing a xor operation on columns.</param>
        public void XorColumns(IEnumerable<bool> bits)
        {
            ColumnsOperation((x, y, z, bit) => { return this[x, y, z] ^ bit; }, bits);
        }

        /// <summary>
        /// Performs a bitwise xor operation on specified lane with specified bits.
        /// </summary>
        /// <param name="lane">The lane whose bits to xor with <paramref name="bits"/>.</param>
        /// <param name="bits">An enumeration of bits to xor with bits of <paramref name="lane"/>.</param>
        public void XorLane(Lane lane, IEnumerable<bool> bits)
        {
            LaneOperation((x, y, z, bit) => { return this[x, y, z] ^ bit; }, lane, bits);
        }

        /// <summary>
        /// Performs a bitwise xor operation on lanes with specified bits.
        /// </summary>
        /// <param name="bits">The bits with which performing a xor operation on lanes.</param>
        public void XorLanes(IEnumerable<bool> bits)
        {
            LanesOperation((x, y, z, bit) => { return this[x, y, z] ^ bit; }, bits);
        }

        /// <summary>
        /// Performs a bitwise xor operation on specified plane with specified bits.
        /// </summary>
        /// <param name="plane">The plane whose bits to xor with <paramref name="bits"/>.</param>
        /// <param name="bits">An enumeration of bits to xor with bits of <paramref name="plane"/>.</param>
        public void XorPlane(Plane plane, IEnumerable<bool> bits)
        {
            PlaneOperation((x, y, z, bit) => { return this[x, y, z] ^ bit; }, plane, bits);
        }

        /// <summary>
        /// Performs a bitwise xor operation on planes with specified bits.
        /// </summary>
        /// <param name="bits">The bits with which performing a xor operation on planes.</param>
        public void XorPlanes(IEnumerable<bool> bits)
        {
            PlanesOperation((x, y, z, bit) => { return this[x, y, z] ^ bit; }, bits);
        }

        /// <summary>
        /// Performs a bitwise xor operation on specified row with specified bits.
        /// </summary>
        /// <param name="row">The row whose bits to xor with <paramref name="bits"/>.</param>
        /// <param name="bits">An enumeration of bits to xor with bits of <paramref name="row"/>.</param>
        public void XorRow(Row row, IEnumerable<bool> bits)
        {
            RowOperation((x, y, z, bit) => { return this[x, y, z] ^ bit; }, row, bits);
        }

        /// <summary>
        /// Performs a bitwise xor operation on rows with specified bits.
        /// </summary>
        /// <param name="bits">The bits with which performing a xor operation on rows.</param>
        public void XorRows(IEnumerable<bool> bits)
        {
            RowsOperation((x, y, z, bit) => { return this[x, y, z] ^ bit; }, bits);
        }

        /// <summary>
        /// Performs a bitwise xor operation on specified sheet with specified bits.
        /// </summary>
        /// <param name="sheet">The sheet whose bits to xor with <paramref name="bits"/>.</param>
        /// <param name="bits">An enumeration of bits to xor with bits of <paramref name="sheet"/>.</param>
        public void XorSheet(Sheet sheet, IEnumerable<bool> bits)
        {
            SheetOperation((x, y, z, bit) => { return this[x, y, z] ^ bit; }, sheet, bits);
        }

        /// <summary>
        /// Performs a bitwise xor operation on sheets with specified bits.
        /// </summary>
        /// <param name="bits">The bits with which performing a xor operation on sheets.</param>
        public void XorSheets(IEnumerable<bool> bits)
        {
            SheetsOperation((x, y, z, bit) => { return this[x, y, z] ^ bit; }, bits);
        }

        /// <summary>
        /// Performs a bitwise xor operation on specified slice with specified bits.
        /// </summary>
        /// <param name="slice">The slice whose bits to xor with <paramref name="bits"/>.</param>
        /// <param name="bits">An enumeration of bits to xor with bits of <paramref name="slice"/>.</param>
        public void XorSlice(Slice slice, IEnumerable<bool> bits)
        {
            SliceOperation((x, y, z, bit) => { return this[x, y, z] ^ bit; }, slice, bits);
        }

        /// <summary>
        /// Performs a bitwise xor operation on slices with specified bits.
        /// </summary>
        /// <param name="bits">The bits with which performing a xor operation on slices.</param>
        public void XorSlices(IEnumerable<bool> bits)
        {
            SlicesOperation((x, y, z, bit) => { return this[x, y, z] ^ bit; }, bits);
        }

        #endregion

        #endregion

        #region Private implementation

        private void ColumnOperation(OperationDelegate function, Column column, IEnumerable<bool> bits)
        {
            int y = 0;
            foreach (bool bit in bits)
            {
                this[GetIndex(column.X, y, column.Z)] = function(column.X, y, column.Z, bit);
                y++;
            }
        }

        private void ColumnsOperation(OperationDelegate function, IEnumerable<bool> bits)
        {
            int w = _size.W;
            int x = 0;
            int y = 0;
            int z = 0;
            foreach (bool bit in bits)
            {
                this[GetIndex(x, y, z)] = function(x, y, z, bit);
                if (++y == 5)
                {
                    y = 0;
                    z++;
                }
                if (z == w)
                {
                    z = 0;
                    x++;
                }
            }
        }

        private void LaneOperation(OperationDelegate function, Lane lane, IEnumerable<bool> bits)
        {
            int z = 0;
            foreach (bool bit in bits)
            {
                this[GetIndex(lane.X, lane.Y, z)] = function(lane.X, lane.Y, z, bit);
                z++;
            }
        }

        private void LanesOperation(OperationDelegate function, IEnumerable<bool> bits)
        {
            int w = _size.W;
            int x = 0;
            int y = 0;
            int z = 0;
            foreach (bool bit in bits)
            {
                this[GetIndex(x, y, z)] = function(x, y, z, bit);
                if (++z == w)
                {
                    z = 0;
                    x++;
                }
                if (x == 5)
                {
                    x = 0;
                    y++;
                }
            }
        }

        private void PlaneOperation(OperationDelegate function, Plane plane, IEnumerable<bool> bits)
        {
            int w = _size.W;
            int x = 0;
            int z = 0;
            foreach (bool bit in bits)
            {
                this[GetIndex(x, plane.Y, z)] = function(x, plane.Y, z, bit);
                if (++z == w)
                {
                    z = 0;
                    x++;
                }
            }
        }

        private void PlanesOperation(OperationDelegate function, IEnumerable<bool> bits)
        {
            int w = _size.W;
            int x = 0;
            int y = 0;
            int z = 0;
            foreach (bool bit in bits)
            {
                this[GetIndex(x, y, z)] = function(x, y, z, bit);
                if (++z == w)
                {
                    z = 0;
                    x++;
                }
                if (x == 5)
                {
                    x = 0;
                    y++;
                }
            }
        }

        private void RowOperation(OperationDelegate function, Row row, IEnumerable<bool> bits)
        {
            int x = 0;
            foreach (bool bit in bits)
            {
                this[GetIndex(x, row.Y, row.Z)] = function(x, row.Y, row.Z, bit);
                x++;
            }
        }

        private void RowsOperation(OperationDelegate function, IEnumerable<bool> bits)
        {
            int w = _size.W;
            int x = 0;
            int y = 0;
            int z = 0;
            foreach (bool bit in bits)
            {
                this[GetIndex(x, y, z)] = function(x, y, z, bit);
                if (++x == 5)
                {
                    x = 0;
                    z++;
                }
                if (z == w)
                {
                    z = 0;
                    y++;
                }
            }
        }

        private void SheetOperation(OperationDelegate function, Sheet sheet, IEnumerable<bool> bits)
        {
            int w = _size.W;
            int y = 0;
            int z = 0;
            foreach (bool bit in bits)
            {
                this[GetIndex(sheet.X, y, z)] = function(sheet.X, y, z, bit);
                if (++z == w)
                {
                    z = 0;
                    y++;
                }
            }
        }

        private void SheetsOperation(OperationDelegate function, IEnumerable<bool> bits)
        {
            int w = _size.W;
            int x = 0;
            int y = 0;
            int z = 0;
            foreach (bool bit in bits)
            {
                this[GetIndex(x, y, z)] = function(x, y, z, bit);
                if (++z == w)
                {
                    z = 0;
                    y++;
                }
                if (y == 5)
                {
                    y = 0;
                    x++;
                }
            }
        }

        private void SliceOperation(OperationDelegate function, Slice slice, IEnumerable<bool> bits)
        {
            int x = 0;
            int y = 0;
            foreach (bool bit in bits)
            {
                this[GetIndex(x, y, slice.Z)] = function(x, y, slice.Z, bit);
                if (++x == 5)
                {
                    x = 0;
                    y++;
                }
            }
        }

        private void SlicesOperation(OperationDelegate function, IEnumerable<bool> bits)
        {
            int w = _size.W;
            int x = 0;
            int y = 0;
            int z = 0;
            foreach (bool bit in bits)
            {
                this[GetIndex(x, y, z)] = function(x, y, z, bit);
                if (++x == 5)
                {
                    x = 0;
                    y++;
                }
                if (y == 5)
                {
                    y = 0;
                    z++;
                }
            }
        }

        /// <summary>
        /// Sets the bitstring holding the bits of this state to specified value.
        /// </summary>
        /// <param name="bitstring">The bitstring by which replacing this state's internal bitstring.</param>
        /// <remarks>Unchecked version of <see cref="Bitstring"/> setter. Internal use only.</remarks>
        internal void SetBitstring(Bitstring bitstring)
        {
            _bitstring = bitstring;
        }

        #endregion
    }

    /// <summary>
    /// Implements a valuetype which represents a single bit in a sponge state.
    /// </summary>
    public struct Bit
    {
        /// <summary>
        /// The sponge state which contains this bit.
        /// </summary>
        public readonly SpongeState State;

        /// <summary>
        /// The sheet coordinate of this bit.
        /// </summary>
        public readonly int X;

        /// <summary>
        /// The plane coordinate of this bit.
        /// </summary>
        public readonly int Y;

        /// <summary>
        /// The slice coordinate of this bit.
        /// </summary>
        public readonly int Z;

        /// <summary>
        /// Gets the value of this bit.
        /// </summary>
        public bool Value
        {
            get { return State[State.GetIndex(this)]; }
        }

        internal Bit(SpongeState state, int x, int y, int z)
        {
            State = state;
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static bool operator true(Bit bit)
        {
            return bit.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static bool operator false(Bit bit)
        {
            return !(bit.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bit"></param>
        public static implicit operator bool(Bit bit)
        {
            return bit.Value;
        }

        /// <summary>
        /// Returns a string representation of this bit.
        /// </summary>
        /// <returns>A string representation of this bit.</returns>
        public override string ToString()
        {
            return $"Bit (X={X}, Y={Y}, Z={Z}) : {Value}";
        }
    }

    /// <summary>
    /// Implements a valuetype which represents a single row in a sponge state.
    /// </summary>
    public struct Row
    {
        /// <summary>
        /// The sponge state which contains this row.
        /// </summary>
        public readonly SpongeState State;

        /// <summary>
        /// The plane coordinate of this row.
        /// </summary>
        public readonly int Y;

        /// <summary>
        /// The slice coordinate of this row.
        /// </summary>
        public readonly int Z;

        internal Row(SpongeState state, int y, int z)
        {
            State = state;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Enumerates the bits of this row.
        /// </summary>
        /// <returns>An enumeration of the bits of this row.</returns>
        public IEnumerable<bool> GetBits()
        {
            for (int x = 0; x < 5; x++)
            {
                yield return State[State.GetIndex(x, Y, Z)];
            }
        }

        /// <summary>
        /// Returns a string representation of this row.
        /// </summary>
        /// <returns>A string representation of this row.</returns>
        public override string ToString()
        {
            return $"Row (Y={Y}, Z={Z})";
        }
    }

    /// <summary>
    /// Implements a valuetype which represents a single column in a sponge state.
    /// </summary>
    public struct Column
    {
        /// <summary>
        /// The sponge state which contains this column.
        /// </summary>
        public readonly SpongeState State;

        /// <summary>
        /// The sheet coordinate of this column.
        /// </summary>
        public readonly int X;

        /// <summary>
        /// The slice coordinate of this column.
        /// </summary>
        public readonly int Z;

        internal Column(SpongeState state, int x, int z)
        {
            State = state;
            X = x;
            Z = z;
        }

        /// <summary>
        /// Enumerates the bits of this column.
        /// </summary>
        /// <returns>An enumeration of the bits of this column.</returns>
        public IEnumerable<bool> GetBits()
        {
            for (int y = 0; y < 5; y++)
            {
                yield return State[State.GetIndex(X, y, Z)];
            }
        }

        /// <summary>
        /// Returns a string representation of this column.
        /// </summary>
        /// <returns>A string representation of this column.</returns>
        public override string ToString()
        {
            return $"Column (X={X}, Z={Z})";
        }
    }

    /// <summary>
    /// Implements a valuetype which represents a single lane in a sponge state.
    /// </summary>
    public struct Lane
    {
        /// <summary>
        /// The sponge state which contains this lane.
        /// </summary>
        public readonly SpongeState State;

        /// <summary>
        /// The sheet coordinate of this lane.
        /// </summary>
        public readonly int X;

        /// <summary>
        /// The plane coordinate of this lane.
        /// </summary>
        public readonly int Y;

        /// <summary>
        /// Gets the number of bits in this lane.
        /// </summary>
        public int Depth
        {
            get { return State.Size.W; }
        }

        internal Lane(SpongeState state, int x, int y)
        {
            State = state;
            X = x;
            Y = y;
        }

        /// <summary>
        /// Enumerates the bits of this lane.
        /// </summary>
        /// <returns>An enumeration of the bits of this lane.</returns>
        public IEnumerable<bool> GetBits()
        {
            int w = State.Size.W;
            for (int z = 0; z < w; z++)
            {
                yield return State[State.GetIndex(X, Y, z)];
            }
        }

        /// <summary>
        /// Returns a string representation of this lane.
        /// </summary>
        /// <returns>A string representation of this lane.</returns>
        public override string ToString()
        {
            return $"Lane (X={X}, Y={Y})";
        }
    }

    /// <summary>
    /// Implements a valuetype which represents a single plane in a sponge state.
    /// </summary>
    public struct Plane
    {
        /// <summary>
        /// The sponge state which contains this plane.
        /// </summary>
        public readonly SpongeState State;

        /// <summary>
        /// The coordinate of this plane.
        /// </summary>
        public readonly int Y;

        /// <summary>
        /// Gets the depth of this plane.
        /// </summary>
        public int Depth
        {
            get { return State.Size.W; }
        }

        internal Plane(SpongeState state, int y)
        {
            State = state;
            Y = y;
        }

        /// <summary>
        /// Enumerates the lanes of this plane.
        /// </summary>
        /// <returns>An enumeration of the lanes of this plane.</returns>
        public IEnumerable<Lane> GetLanes()
        {
            for (int x = 0; x < 5; x++)
            {
                yield return new Lane(State, x, Y);
            }
        }

        /// <summary>
        /// Enumerates the rows of this plane.
        /// </summary>
        /// <returns>An enumeration of the rows of this plane.</returns>
        public IEnumerable<Row> GetRows()
        {
            int w = State.Size.W;
            for (int z = 0; z < w; z++)
            {
                yield return new Row(State, Y, z);
            }
        }

        /// <summary>
        /// Returns a string representation of this plane.
        /// </summary>
        /// <returns>A string representation of this plane.</returns>
        public override string ToString()
        {
            return $"Plane (Y={Y})";
        }
    }

    /// <summary>
    /// Implements a valuetype which represents a single sheet in a sponge state.
    /// </summary>
    public struct Sheet
    {
        /// <summary>
        /// The sponge state which contains this sheet.
        /// </summary>
        public readonly SpongeState State;

        /// <summary>
        /// The coordinate of this sheet.
        /// </summary>
        public readonly int X;

        /// <summary>
        /// Gets the depth of this sheet.
        /// </summary>
        public int Depth
        {
            get { return State.Size.W; }
        }

        internal Sheet(SpongeState state, int x)
        {
            State = state;
            X = x;
        }

        /// <summary>
        /// Enumerates the columns of this sheet.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Column> GetColumns()
        {
            int w = State.Size.W;
            for (int z = 0; z < w; z++)
            {
                yield return new Column(State, X, z);
            }
        }

        /// <summary>
        /// Enumerates the lanes of this sheet.
        /// </summary>
        /// <returns>An enumeration of the lanes of this sheet.</returns>
        public IEnumerable<Lane> GetLanes()
        {
            for (int y = 0; y < 5; y++)
            {
                yield return new Lane(State, X, y);
            }
        }

        /// <summary>
        /// Returns a string representation of this sheet.
        /// </summary>
        /// <returns>A string representation of this sheet.</returns>
        public override string ToString()
        {
            return $"Sheet (X={X})";
        }
    }

    /// <summary>
    /// Implements a valuetype which represents a single slice in a sponge state.
    /// </summary>
    public struct Slice
    {
        /// <summary>
        /// The sponge state which contains this slice.
        /// </summary>
        public readonly SpongeState State;

        /// <summary>
        /// The coordinate of this slice.
        /// </summary>
        public readonly int Z;

        internal Slice(SpongeState state, int z)
        {
            State = state;
            Z = z;
        }

        /// <summary>
        /// Enumerates the columns of this slice.
        /// </summary>
        /// <returns>An enumeration of the columns of this slice.</returns>
        public IEnumerable<Column> GetColumns()
        {
            for (int x = 0; x < 5; x++)
            {
                yield return new Column(State, x, Z);
            }
        }

        /// <summary>
        /// Enumerates the rows of this slice.
        /// </summary>
        /// <returns>An enumeration of the rows of this slice.</returns>
        public IEnumerable<Row> GetRows()
        {
            for (int y = 0; y < 5; y++)
            {
                yield return new Row(State, y, Z);
            }
        }

        /// <summary>
        /// Returns a string representation of this slice.
        /// </summary>
        /// <returns>A string representation of this slice.</returns>
        public override string ToString()
        {
            return $"Slice (Z={Z})";
        }
    }
}
