using CardGame.Interfaces;

namespace CardGameTest.Models
{
    public class RandomizeNone : IRandomize
    {
        public int GetRandomIntForRange(int start, int end)
        {
            int r = start;
            return r;
        }
    }
}
