using System;
using System.Collections.Generic;

namespace CryptoWebService.Helpers
{
    public static class RandomHelper
    {
        private static List<E> ShuffleList<E>(List<E> listToShuffle)
        {
            int randomIndex = 0;
            Random r = new Random();
            List<E> randomList = new List<E>();

            while (listToShuffle.Count > 0)
            {
                randomIndex = r.Next(0, listToShuffle.Count);
                randomList.Add(listToShuffle[randomIndex]);
                listToShuffle.RemoveAt(randomIndex);
            }
            return randomList;
        }
    }
}
