using System.Collections.Generic;
using System.Linq;

namespace CardGame.Models
{

    public class GameRules
    {
        public GameRules()
        {
            this.Suits = new List<string>();
        }

        public bool EnableSuits
        {
            get
            {
                var result = Suits.Count() == 4;
                return result;
            }
        }
        public List<string> Suits { get; set; }
        public int NumberOfPlayers { get; set; }
        public int DeckSize { get; set; }

        public string Description
        {
            get
            {
                var suits = this.EnableSuits ? string.Join(" ", this.Suits) : "not enabled";
                var result = $"Deck size: {this.DeckSize}, number of players: {this.NumberOfPlayers}, suits: {suits} \r\n";
                return result;
            }
        }
    }


}
