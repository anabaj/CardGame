using CardGame.Models;
using CardGame.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame.Services
{
    public class CardGameService
    {
        #region Members

        private readonly GameRules _gameRules;
        private readonly IEnumerable<int> _initialDeck;
        public readonly List<Player> Players;
        private readonly List<int> _tempDeck;


        public bool GameEnded
        {
            get
            {
                var result = Players.Any(player => player.Total == 0);
                return result;
            }
        }

        public Player Winner
        {
            get
            {
                var result = Players.OrderByDescending(player => player.Total).First();
                return result;
            }
        }

        #endregion

        #region Constructor

        public CardGameService(GameRules gameRules)
        {
            _gameRules = gameRules;
            _tempDeck = new List<int>();
            Players = InitializePlayers();
            _initialDeck = InitializeDeck();
        }

        #endregion

        #region Methods

        public void StartGame()
        {
            Console.WriteLine(_gameRules.Description);

            var deck = _initialDeck.GetShuffledStack();
            InitializePlayerDecks(deck);
            while (!GameEnded)
            {
                DrawCard();
                CalculateDrawOutcome();
            }

            Console.WriteLine($"{Winner.Name} wins the game");
        }

        public List<Player> InitializePlayers()
        {
            var players = new List<Player>();
            for (int i = 0; i < _gameRules.NumberOfPlayers; i++)
            {
                var player = new Player();
                player.Name = $"Player {i + 1}";
                players.Add(player);
            }
            return players;
        }

        public IEnumerable<int> InitializeDeck()
        {
            var max = _gameRules.DeckSize / 4; 
            var result = new List<int>();
            if (_gameRules.EnableSuits)
            {
                for (int i = 1; i <= _gameRules.DeckSize; i++)
                {
                    result.Add(i);
                }
            }
            else
            {
                for (int i = 1; i <= max; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        result.Add(i);
                    }
                }
            }

            return result;
        }

        public void InitializePlayerDecks(Stack<int> deck)
        {
            var playerDeckSize = _gameRules.DeckSize / _gameRules.NumberOfPlayers;
            for (int i = 0; i < _gameRules.NumberOfPlayers; i++)
            {
                Players[i].Deck = new Stack<int>(deck.Skip(i * playerDeckSize).Take(playerDeckSize));
            }
        }

        public void CheckPlayersDecks()
        {
            for (int i = 0; i < Players.Count(); i++)
            {
                var playerDeckIsEmpty = !Players[i].Deck.Any();
                if (playerDeckIsEmpty)
                {
                    ReinitializePlayerDecks(i);
                }
            }
        }

        public void ReinitializePlayerDecks(int i)
        {
            Players[i].Deck = Players[i].Discard.GetShuffledStack();
            Players[i].Discard = new List<int>();
        }

        public void DrawCard()
        {
            CheckPlayersDecks();

            Players.ForEach(player =>
            {
                player.CurrentCard = player.Deck.Pop();
                _tempDeck.Add(player.CurrentCard);

                Console.WriteLine($"{player.Name} ({player.Deck.Count() + 1} cards): {player.GetCardLabel(_gameRules)}");
                //Console.WriteLine($"{player.Name} ({player.Total + 1} cards): {player.CurrentCard}");
            });
        }

        public void CalculateDrawOutcome()
        {
            var winnerCard = Players.Max(p => p.CurrentCard);
            var winners = Players.Where(p => p.CurrentCard == winnerCard);
            var isDraw = winners.Count() > 1;
            if (isDraw)
            {
                Console.WriteLine($"No winner in this round\r\n");
            }
            else
            {
                var winner = winners.First();
                winner.Discard.AddRange(_tempDeck);

                _tempDeck.Clear();

                Console.WriteLine($"{winner.Name} wins this round\r\n");
            }

        }

        #endregion
    }
}
