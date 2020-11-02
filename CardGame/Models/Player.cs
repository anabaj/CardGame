using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame.Models
{
    public class Player
    {
        public Player()
        {
            Deck = new Stack<int>();
            Discard = new List<int>();
        }
        public Stack<int> Deck { get; set; }
        public List<int> Discard { get; set; }

        public int CurrentCard { get; set; }
        public string Name { get; set; }

        public int Total
        {
            get
            {
                var result = Deck.Count() + Discard.Count();
                return result;
            }
        }

        public string GetCardLabel(GameRules gameRules)
        {
            var result = CurrentCard.ToString();

            if (gameRules.EnableSuits)
            {
                var limiter = gameRules.DeckSize / 4;
                //var suits = new List<string> { "Spade", "Diamond", "Heart", "Club" };
                for (int i = 0; i < limiter; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        var num = 1 + 4 * i + j;
                        if (CurrentCard == num)
                        {
                            result = $"{i + 1} {gameRules.Suits[j]}";
                            break;
                        }
                    }
                }
            }


            return result;

        }
    }
}
