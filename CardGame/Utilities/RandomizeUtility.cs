using CardGame.Interfaces;
using System;
using System.Collections.Generic;

namespace CardGame.Utilities
{
    public class RandomizeUtility : IRandomize
    {
        Random _random = new Random();
        public RandomizeUtility(Random random)
        {
            _random = random;
        }
        public int GetRandomIntForRange(int start, int end)
        {
            int r = start + _random.Next(end - start);
            return r;
        }
    }
}
