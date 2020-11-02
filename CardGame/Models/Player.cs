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

        /// <summary>
        /// Spades - 0, Diamonds - 1, Hearts - 2, Clubs - 3
        /// </summary>
        /// <param name="deckSize"></param>
        /// <returns></returns>
        public string GetCardLabel(bool enableSuits, int deckSize)
        {
            var result = CurrentCard.ToString();

            if (enableSuits)
            {
                var limiter = deckSize / 4;
                var suits = new List<string> { "Spade", "Diamond", "Heart", "Club" };
                for (int i = 0; i < limiter; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        var num = 1 + 4 * i + j;
                        if (CurrentCard == num)
                        {
                            result = $"{i + 1} {suits[j]}";
                            break;
                        }
                    }
                }


                //if (this.CurrentCard <= limiter)
                //{
                //    result = $"{this.CurrentCard} Spade";
                //}
                //else if (this.CurrentCard <= limiter * 2)
                //{
                //    result = $"{this.CurrentCard - limiter} Diamond";
                //}
                //else if (this.CurrentCard <= limiter * 3)
                //{
                //    result = $"{this.CurrentCard - limiter * 2} Heart";
                //}
                //else if (this.CurrentCard <= limiter * 4)
                //{
                //    result = $"{this.CurrentCard - limiter * 3} Club";
                //}
            }


            return result;

        }
    }
}
