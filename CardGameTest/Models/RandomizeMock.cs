using CardGame.Interfaces;

namespace CardGameTest.Models
{
    public class RandomizeMock : IRandomize
    {
        private int _returnValue;

        public RandomizeMock(int returnValue)
        {
            _returnValue = returnValue;
        }

        public int GetRandomIntForRange(int start, int end)
        {
            return _returnValue;
        }
    }
}
