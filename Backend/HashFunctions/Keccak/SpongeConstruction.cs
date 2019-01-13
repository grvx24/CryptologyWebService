using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.HashFunctions.Keccak
{
    /// <summary>
    /// Defines a sponge construction.
    /// </summary>
    public interface ISpongeConstruction
    {
        /// <summary>
        /// Gets the capacity of the sponge construction, i.e. the number of bits which are not impacted while absorbing
        /// messages.
        /// </summary>
        int Capacity { get; }

        /// <summary>
        /// Gets the rate of the sponge construction, i.e. the number of bits which are impacted while absorbing messages.
        /// </summary>
        int Rate { get; }

        /// <summary>
        /// Gets the size of the sponge construction.
        /// </summary>
        SpongeSize Size { get; }

        /// <summary>
        /// Returns a byte array holding the bits resulting from the processing of specified stream through the sponge
        /// construction.
        /// </summary>
        /// <param name="bytes">The bytes to process.</param>
        /// <param name="outputLength">The number of desired output bits.</param>
        /// <param name="inputLength">When specified, limits the number of bits of <paramref name="bytes"/> which will be
        /// considered.</param>
        /// <returns>A byte array holding the bits resulting from the processing of <paramref name="bytes"/> through the
        /// sponge construction.</returns>
        byte[] Process(byte[] bytes, int outputLength, int inputLength = -1);
    }

    /// <summary>
    /// Implements an abstract sponge construction.
    /// </summary>
    public abstract class SpongeConstruction : ISpongeConstruction
    {
        /// <summary>
        /// The underlying sponge state holding the bits.
        /// </summary>
        protected readonly SpongeState State;

        /// <summary>
        /// Gets the capacity of this sponge construction, i.e. the number of bits which are not impacted while absorbing
        /// messages.
        /// </summary>
        public int Capacity
        {
            get { return State.Capacity; }
        }

        /// <summary>
        /// Gets the rate of this sponge construction, i.e. the number of bits which are impacted while absorbing
        /// messages.
        /// </summary>
        public int Rate
        {
            get { return State.Rate; }
        }

        /// <summary>
        /// Gets the size of this sponge construction.
        /// </summary>
        public SpongeSize Size
        {
            get { return State.Size; }
        }

        /// <summary>
        /// Creates a new <see cref="SpongeConstruction"/>, specifying the size of the construction and its rate.
        /// </summary>
        /// <param name="size">The size of the sponge construction.</param>
        /// <param name="rate">The rate of the sponge construction.</param>
        protected SpongeConstruction(SpongeSize size, int rate)
        {
            State = new SpongeState(size, rate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="length"></param>
        protected virtual void Absorb(byte[] bytes, int length)
        {
            State.Clear();
            Bitstring message = new Bitstring(bytes, length);
            int rate = State.Rate;
            message.Append(Suffix());
            message.Append(GetPadding(rate, message.Length));
            int n = message.Length / rate;
            Bitstring zeroes = new Bitstring(Capacity);
            Bitstring chunk;
            for (int i = 0; i < n; i++)
            {
                chunk = message.Substring(rate * i, rate);
                chunk.Append(zeroes);
                State.Bitstring.Xor(chunk);
                Function();
            }
        }

        /// <summary>
        /// Applies a single permutation or transformation to the sponge construction.
        /// </summary>
        protected abstract void Function();

        /// <summary>
        /// Pads input bitstrings to the sponge construction to a number of bits which is a multiple of the rate.
        /// </summary>
        /// <param name="r">The rate of the sponge construction.</param>
        /// <param name="m">The length of the bitstring whose length to pad to a multiple of <paramref name="r"/>.</param>
        /// <returns>A padding suffix for a bitstring of length <paramref name="m"/> being processed in a sponge
        /// construction with a rate <paramref name="r"/>.</returns>
        protected abstract Bitstring GetPadding(int r, int m);

        /// <summary>
        /// Returns a byte array holding the bits resulting from the processing of specified stream through the sponge
        /// construction.
        /// </summary>
        /// <param name="bytes">The bytes to process.</param>
        /// <param name="outputLength">The number of desired output bits.</param>
        /// <param name="inputLength">When specified, limits the number of bits of <paramref name="bytes"/> which will be
        /// considered.</param>
        /// <returns>A byte array holding the bits resulting from the processing of <paramref name="bytes"/> through the
        /// sponge construction.</returns>
        public virtual byte[] Process(byte[] bytes, int outputLength, int inputLength = -1)
        {
            byte[] result = null;
            if (bytes != null)
            {
                inputLength = (inputLength > -1) ? inputLength : bytes.Length << Bitstring.Shift;
                Absorb(bytes, inputLength);
                result = Squeeze(outputLength);
            }
            return result;
        }

        /// <summary>
        /// Squeezes internal state until at least specified number of bits is produced, and returns these bits.
        /// </summary>
        /// <param name="outputLength">The number of desired output bits.</param>
        /// <returns>The byte array holding resulting bits.</returns>
        protected virtual byte[] Squeeze(int outputLength)
        {
            int rate = State.Rate;
            Bitstring q = new Bitstring();
            while (true)
            {
                q.Append(State.Bitstring.Truncate(rate));
                if (q.Length >= outputLength)
                {
                    return (q.Length == outputLength) ? q.Bytes : q.Truncate(outputLength).Bytes;
                }
                Function();
            }
        }
        //}

        /// <summary>
        /// Returns the suffix which will be appended to input bitstrings before being padded to a multiple of the rate.
        /// </summary>
        protected virtual Bitstring Suffix()
        {
            return new Bitstring();
        }
    }
}
