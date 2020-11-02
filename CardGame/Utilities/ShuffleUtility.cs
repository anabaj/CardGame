using CardGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame.Utilities
{
    public static class ShuffleUtility
    {
        static Random _random = new Random();

        public static Stack<int> GetShuffledStack(this IEnumerable<int> input)
        {
            var randomizeHelper = new RandomizeUtility(_random);
            var shuffled = Shuffle(input.ToArray(), randomizeHelper);
            var stack = new Stack<int>(shuffled);
            return stack;
        }

        public static T[] Shuffle<T>(T[] array, IRandomize randomizeHelper)
        {
            //take first and swap with random item from all array
            //take second and swap with random item from the rest of array (that is not swapped)
            //take third and swap with random item from the rest of array
            //we loop to n-1 because when it is n in the last loop we get to swapping array[n - 1] with array[n - 1] 
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int r = randomizeHelper.GetRandomIntForRange(i, n); // r is random between [i and n-1]
                //int r = i + _random.Next(n - i); // r is random between [i and n-1]
                //int r = i + (int)(_random.NextDouble() * (n - i));
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
            return array;
        }

    }
}
