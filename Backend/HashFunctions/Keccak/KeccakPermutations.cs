using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.HashFunctions.Keccak
{
    /// <summary>
    /// 
    /// </summary>
    public class KeccakPermutation : SpongeConstruction
    {
        private struct RoundT : IEquatable<RoundT>
        {
            public readonly int Round;
            public readonly int T;

            public RoundT(int round, int t)
            {
                Round = round;
                T = t;
            }

            public static bool operator ==(RoundT lhs, RoundT rhs)
            {
                return lhs.Equals(rhs);
            }

            public static bool operator !=(RoundT lhs, RoundT rhs)
            {
                return !lhs.Equals(rhs);
            }

            public bool Equals(RoundT other)
            {
                return ((Round == other.Round) && (T == other.T));
            }

            public override bool Equals(object obj)
            {
                return ((obj is RoundT) && Equals((RoundT)obj));
            }

            public override int GetHashCode()
            {
                return HashCoder<int>.Boost.Compute(Round, T);
            }
        }

        private static readonly Dictionary<int, bool> _roundConstants = new Dictionary<int, bool> {
            { 0, true }
        };
        private static readonly Dictionary<RoundT, bool> _roundTConstants = new Dictionary<RoundT, bool>();

        private readonly int _roundCount;

        /// <summary>
        /// Gets the number of iterations of a single internal permutation.
        /// </summary>
        public int RoundCount
        {
            get { return _roundCount; }
        }

        /// <summary>
        /// Creates a new <see cref="KeccakPermutation"/> specifying its size, rate and number of iterations of a single
        /// permutation.
        /// </summary>
        /// <param name="size">The size of the permutation.</param>
        /// <param name="rate">The rate of the permutation.</param>
        /// <param name="roundCount">The number of internal iterations of a single permutation.</param>
        protected KeccakPermutation(SpongeSize size, int rate, int roundCount)
            : base(size, rate)
        {
            _roundCount = roundCount;
        }

        /// <summary>
        /// Applies a single permutation or transformation to the sponge construction.
        /// </summary>
        protected override void Function()
        {
            int start = 12 + (State.Size.L << 1);
            for (int round = start - _roundCount; round < start; round++)
            {
                Iota(Khi(Pi(Rho(Theta(State)))), round);
            }
        }

        /// <summary>
        /// Pads input bitstrings to the sponge construction to a number of bits which is a multiple of the rate.
        /// </summary>
        /// <param name="r">The rate of the sponge construction.</param>
        /// <param name="m">The length of the bitstring whose length to pad to a multiple of <paramref name="r"/>.</param>
        /// <returns>A padding suffix for a bitstring of length <paramref name="m"/> being processed in a sponge
        /// construction with a rate <paramref name="r"/>.</returns>
        /// <remarks>Keccak-p permutations use a pad10*1 (multirate) padding rule.</remarks>
        protected override Bitstring GetPadding(int r, int m)
        {
            int j = Bin.Mod(-m - 2, r);
            Bitstring pad = new Bitstring(j + 2);
            pad[0] = true;
            pad[pad.Length - 1] = true;
            return pad;
        }

        private static SpongeState Theta(SpongeState state)
        {
            int w = state.Size.W;
            bool[,] c = new bool[5, w];
            for (int x = 0; x < 5; x++)
            {
                for (int z = 0; z < w; z++)
                {
                    c[x, z] = state.GetColumn(x, z).GetBits().Aggregate((bool lhs, bool rhs) => { return lhs ^ rhs; });
                }
            }
            bool[,] d = new bool[5, w];
            for (int x = 0; x < 5; x++)
            {
                for (int z = 0; z < w; z++)
                {
                    d[x, z] = c[Bin.Mod(x - 1, 5), z] ^ c[Bin.Mod(x + 1, 5), Bin.Mod(z - 1, w)];
                }
            }
            for (int x = 0; x < 5; x++)
            {
                for (int z = 0; z < w; z++)
                {
                    bool bit = d[x, z];
                    for (int y = 0; y < 5; y++)
                    {
                        state[x, y, z] ^= bit;
                    }
                }
            }
            return state;
        }

        private static SpongeState Rho(SpongeState state)
        {
            SpongeState newState = new SpongeState(state.Size, state.Rate);
            int w = state.Size.W;
            newState.SetLane(newState.GetLane(0, 0), state.GetLane(0, 0).GetBits());
            int x = 1;
            int y = 0;
            int u, oldX;
            for (int t = 0; t < 24; t++)
            {
                u = ((t + 1) * (t + 2)) >> 1;
                for (int z = 0; z < w; z++)
                {
                    newState[x, y, z] = state[x, y, Bin.Mod(z - u, w)];
                }
                oldX = x;
                x = y;
                y = Bin.Mod(2 * oldX + 3 * y, 5);
            }
            state.SetBitstring(newState.Bitstring);
            return state;
        }

        private static SpongeState Pi(SpongeState state)
        {
            SpongeState newState = new SpongeState(state.Size, state.Rate);
            int w = state.Size.W;
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    for (int z = 0; z < w; z++)
                    {
                        newState[x, y, z] = state[Bin.Mod(x + 3 * y, 5), x, z];
                    }
                }
            }
            state.SetBitstring(newState.Bitstring);
            return state;
        }

        private static SpongeState Khi(SpongeState state)
        {
            SpongeState newState = new SpongeState(state.Size, state.Rate);
            int w = state.Size.W;
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    for (int z = 0; z < w; z++)
                    {
                        newState[x, y, z] = state[x, y, z]
                            ^ ((state[Bin.Mod(x + 1, 5), y, z] ^ true) && state[Bin.Mod(x + 2, 5), y, z]);
                    }
                }
            }
            state.SetBitstring(newState.Bitstring);
            return state;
        }

        private static SpongeState Iota(SpongeState state, int round)
        {
            int w = state.Size.W;
            int l = state.Size.L;
            Bitstring rc = Bitstring.Zeroes(w);
            RoundT roundT;
            int t;
            int rnd = 7 * round;
            for (int j = 0; j <= l; j++)
            {
                t = j + rnd;
                roundT = new RoundT(round, t);
                if (!_roundTConstants.ContainsKey(roundT))
                {
                    _roundTConstants.Add(roundT, RoundConstant(t));
                }
                rc[(1 << j) - 1] = _roundTConstants[roundT];
            }
            state.XorLane(state.GetLane(0, 0), rc);
            return state;
        }

        private static bool RoundConstant(int t)
        {
            t = Bin.Mod(t, 255);
            if (_roundConstants.ContainsKey(t))
            {
                return _roundConstants[t];
            }
            Bitstring r = new Bitstring("10000000", 8);
            for (int i = 0; i < t; i++)
            {
                r.Prepend(Bitstring.Zero);
                r[0] ^= r[8];
                r[4] ^= r[8];
                r[5] ^= r[8];
                r[6] ^= r[8];
                r = r.Truncate(8);
            }
            bool bit = r[0];
            _roundConstants.Add(t, bit);
            return bit;
        }
    }

    /// <summary>
    /// Implements a Keccak-f[b] permutation, which is a Keccak-p[b,nr] permutation specialized to the case where
    /// nr = 12 + 2 * L.
    /// </summary>
    public class KeccakFunction : KeccakPermutation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="rate"></param>
        protected KeccakFunction(SpongeSize size, int rate)
            : base(size, rate, 12 + (size.L << 1)) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static KeccakFunction F25(int rate)
        {
            return new KeccakFunction(SpongeSize.W01, rate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static KeccakFunction F50(int rate)
        {
            return new KeccakFunction(SpongeSize.W02, rate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static KeccakFunction F100(int rate)
        {
            return new KeccakFunction(SpongeSize.W04, rate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static KeccakFunction F200(int rate)
        {
            return new KeccakFunction(SpongeSize.W08, rate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static KeccakFunction F400(int rate)
        {
            return new KeccakFunction(SpongeSize.W16, rate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static KeccakFunction F800(int rate)
        {
            return new KeccakFunction(SpongeSize.W32, rate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static KeccakFunction F1600(int rate)
        {
            return new KeccakFunction(SpongeSize.W64, rate);
        }
    }

    /// <summary>
    /// Implements a Keccak[c] permutation, which is a Keccak-f[1600] permutation where the rate is determined by the
    /// capacity. A Keccak[c] permutation is then a sponge construction with Keccak-p[1600,24] as underlying permutation,
    /// pad10*1 as padding rule, and a rate defined by 1600 - c.
    /// </summary>
    public class Keccak : KeccakFunction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity"></param>
        public Keccak(int capacity)
            : base(SpongeSize.W64, 1600 - capacity) { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Keccak Keccak224()
        {
            return new Keccak(448);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Keccak Keccak256()
        {
            return new Keccak(512);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Keccak Keccak384()
        {
            return new Keccak(768);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Keccak Keccak512()
        {
            return new Keccak(1024);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class Sha3Permutation : Keccak
    {
        public int Width
        {
            get { return Capacity >> 1; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity"></param>
        private Sha3Permutation(int capacity)
            : base(capacity) { }

        /// <summary>
        /// 
        /// </summary>
        public static Sha3Permutation Sha3_224()
        {
            return new Sha3Permutation(448);
        }

        /// <summary>
        /// 
        /// </summary>
        public static Sha3Permutation Sha3_256()
        {
            return new Sha3Permutation(512);
        }

        /// <summary>
        /// 
        /// </summary>
        public static Sha3Permutation Sha3_384()
        {
            return new Sha3Permutation(768);
        }

        /// <summary>
        /// 
        /// </summary>
        public static Sha3Permutation Sha3_512()
        {
            return new Sha3Permutation(1024);
        }

        /// <summary>
        /// Returns the suffix which will be appended to input bitstrings before being padded to a multiple of the rate.
        /// </summary>
        protected override Bitstring Suffix()
        {
            return new Bitstring("01");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class RawShakePermutation : Keccak
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity"></param>
        private RawShakePermutation(int capacity)
            : base(capacity) { }

        /// <summary>
        /// 
        /// </summary>
        public static RawShakePermutation RawShake128()
        {
            return new RawShakePermutation(256);
        }

        /// <summary>
        /// 
        /// </summary>
        public static RawShakePermutation RawShake256()
        {
            return new RawShakePermutation(512);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Bitstring Suffix()
        {
            return new Bitstring("11");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ShakePermutation : Keccak
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity"></param>
        private ShakePermutation(int capacity)
            : base(capacity) { }

        /// <summary>
        /// 
        /// </summary>
        public static ShakePermutation Shake128()
        {
            return new ShakePermutation(256);
        }

        /// <summary>
        /// 
        /// </summary>
        public static ShakePermutation Shake256()
        {
            return new ShakePermutation(512);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Bitstring Suffix()
        {
            return new Bitstring("1111");
        }
    }
}
