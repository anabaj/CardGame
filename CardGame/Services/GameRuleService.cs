using CardGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame.Services
{
    public class GameRuleService
    {
        public GameRules InitializeRules(string[] args)
        {

            var gameRules = new GameRules();

            if (!int.TryParse(GetArgument(args, "--size", "40"), out var deckSize))
            {
                throw new Exception("Deck size must be a number. Game will not start.");
            }
            if (!int.TryParse(GetArgument(args, "--players", "2"), out var numberOfPlayers))
            {
                throw new Exception("Number of players must be a number. Game will not start.");
            }
            var suits = GetArgument(args, "--suits", "");
            if (suits == "")
            {
                gameRules.Suits = new List<string>();

            }
            else if (suits.Split(" ").Length == 4)
            {
                gameRules.Suits = suits.Split(" ").ToList();
            }
            else
            {
                throw new Exception("Suits must be emtpy or list of 4 strings. Game will not start.");
            }
            gameRules.DeckSize = deckSize;
            gameRules.NumberOfPlayers = numberOfPlayers;

            return gameRules;
        }

        private string GetArgument(string[] args, string argumentName, string defaultValue)
        {
            var result = defaultValue;
            if (args.Length >= 2)
            {
                var index = args.ToList().IndexOf(argumentName);
                if (index != -1)
                {
                    if (argumentName == "--suits")
                    {
                        result = $"{args[index + 1]} {args[index + 2]} {args[index + 3]} {args[index + 4]}";  
                    }
                    else
                    {
                        result = args[index + 1];
                    }
                }
            }
            return result;
        }
    }
}
