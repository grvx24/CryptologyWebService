using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.HashFunctions.Keccak
{
    /// <summary>
    /// Defines an object which can provide a hash code for a collection of <typeparamref name="T"/> values.
    /// </summary>
    /// <typeparam name="T">The type of values whose collection to get a hash code for.</typeparam>
    /// <remarks>Implementations should make sure that changing either one value or the order of values will lead to a
    /// distinct hash code.</remarks>
    public interface IHashCoder<T>
    {
        /// <summary>
        /// Returns a hash code for specified collection of values.
        /// </summary>
        /// <param name="values">The collection of values to get a hash code for.</param>
        /// <returns>A hash code for <paramref name="values"/>.</returns>
        int Compute(IEnumerable<T> values);

        /// <summary>
        /// Returns a hash code for specified collection of values.
        /// </summary>
        /// <param name="values">The collection of values to get a hash code for.</param>
        /// <returns>A hash code for <paramref name="values"/>.</returns>
        int Compute(params T[] values);
    }

    /// <summary>
    /// Implements an abstract object which can provide a hash code for a collection of <typeparamref name="T"/> values,
    /// which also provides static properties for quick hash code computing.
    /// </summary>
    /// <typeparam name="T">The type of values whose collection to get a hash code for.</typeparam>
    public abstract class HashCoder<T> : IHashCoder<T>
    {
        /// <summary>
        /// Gets an <see cref="IHashCoder{T}"/> using <see cref="BoostHashCoder{T}"/> algorithm.
        /// </summary>
        public static IHashCoder<T> Boost
        {
            get { return BoostHashCoder<T>.Instance; }
        }

        /// <summary>
        /// Gets an <see cref="IHashCoder{T}"/> using <see cref="DefaultHashCoder{T}"/> algorithm.
        /// </summary>
        public static IHashCoder<T> Default
        {
            get { return DefaultHashCoder<T>.Instance; }
        }

        /// <summary>
        /// Returns a hash code for specified collection of values.
        /// </summary>
        /// <param name="values">The collection of values to get a hash code for.</param>
        /// <returns>A hash code for <paramref name="values"/>.</returns>
        public abstract int Compute(IEnumerable<T> values);

        /// <summary>
        /// Returns a hash code for specified collection of values.
        /// </summary>
        /// <param name="values">The collection of values to get a hash code for.</param>
        /// <returns>A hash code for <paramref name="values"/>.</returns>
        public int Compute(params T[] values)
        {
            return Compute(values.AsEnumerable());
        }
    }

    /// <summary>
    /// Implements an object which can provide a hash code for a collection of <typeparamref name="T"/> values.
    /// </summary>
    /// <typeparam name="T">The type of values whose collection to get a hash code for.</typeparam>
    public sealed class DefaultHashCoder<T> : HashCoder<T>
    {
        private class Singleton
        {
            internal static readonly DefaultHashCoder<T> Instance = new DefaultHashCoder<T>();
            static Singleton() { }
        }

        /// <summary>
        /// Gets the singleton instance of <see cref="DefaultHashCoder{T}"/>.
        /// </summary>
        public static DefaultHashCoder<T> Instance
        {
            get { return Singleton.Instance; }
        }

        private DefaultHashCoder() { }

        /// <summary>
        /// Returns a hash code for specified collection of values.
        /// </summary>
        /// <param name="values">The collection of values to get a hash code for.</param>
        /// <returns>A hash code for <paramref name="values"/>.</returns>
        public override int Compute(IEnumerable<T> values)
        {
            int hash = 27;
            foreach (T value in values)
            {
                hash *= 13;
                hash += value.GetHashCode();
            }
            return hash;
        }
    }

    /// <summary>
    /// Implements an object which can provide a hash code for a collection of <typeparamref name="T"/> values using
    /// Boost library algorithm.
    /// </summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class BoostHashCoder<T> : HashCoder<T>
    {
        private class Singleton
        {
            internal static readonly BoostHashCoder<T> Instance = new BoostHashCoder<T>();
            static Singleton() { }
        }

        /// <summary>
        /// Gets the singleton instance of <see cref="BoostHashCoder{T}"/>.
        /// </summary>
        public static BoostHashCoder<T> Instance
        {
            get { return Singleton.Instance; }
        }

        private BoostHashCoder() { }

        private static void Combine(ref int seed, T value)
        {
            unchecked { seed ^= value.GetHashCode() + (int)0x9e3779b9 + (seed << 6) + (seed >> 2); }
        }

        /// <summary>
        /// Returns a hash code for specified collection of values.
        /// </summary>
        /// <param name="values">The collection of values to get a hash code for.</param>
        /// <returns>A hash code for <paramref name="values"/>.</returns>
        public override int Compute(IEnumerable<T> values)
        {
            int hash = 0;
            foreach (T value in values)
            {
                Combine(ref hash, value);
            }
            return hash;
        }
    }
}