using CardGame.Models;
using CardGame.Services;
using CardGame.Utilities;
using CardGameTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CardGameTest.Tests
{
    public class CardGameServiceTest
    {
        readonly GameRules _gameRules = new GameRules
        {
            DeckSize = 40,
            Suits = new List<string> { "spade", "diamond", "heart", "club" },
            NumberOfPlayers = 2
        };

        [Fact]
        public void ValidateNewDeck()
        {
            var cardGameService = new CardGameService(_gameRules);
            var allCards = cardGameService.InitializeDeck();

            Assert.Contains(allCards, item => item >= 1 && item <= _gameRules.DeckSize / 4);
            Assert.Equal(_gameRules.DeckSize, allCards.Count());
        }

        [Fact]
        public void NewDeckIsShuffled()
        {
            var dackManager = new CardGameService(_gameRules);
            var ordered = new Stack<int>(dackManager.InitializeDeck());
            var shuffled = dackManager.InitializeDeck().GetShuffledStack();

            Assert.Contains(shuffled, item => item >= 1 && item <= _gameRules.DeckSize / 4);
            Assert.Equal(_gameRules.DeckSize, shuffled.Count());
            Assert.NotEqual(ordered, shuffled);
        }

        [Fact]
        public void CheckNewPlayersDeckSizes()
        {
            var cardGameService = new CardGameService(_gameRules);
            var deck = cardGameService.InitializeDeck().GetShuffledStack();
            cardGameService.InitializePlayerDecks(deck);
            var expected = _gameRules.DeckSize / _gameRules.NumberOfPlayers;

            cardGameService.Players.ForEach(player =>
            {
                Assert.Equal(expected, player.Deck.Count());
            });
        }

        [Fact]
        public void SuffleMockFirst()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var expected = new int[] { 9, 1, 2, 3, 4, 5, 6, 7, 8, 10 };
            var randomizeHelper = new RandomizeMock(0);
            var result = ShuffleUtility.Shuffle(array, randomizeHelper);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SuffleMockSecond()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var expected = new int[] { 9, 1, 2, 3, 4, 5, 6, 7, 8, 10 };
            var randomizeHelper = new RandomizeMock(8);
            var result = ShuffleUtility.Shuffle(array, randomizeHelper);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SuffleMockNone()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var randomizeHelper = new RandomizeNone();
            var result = ShuffleUtility.Shuffle(array, randomizeHelper);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void FirstPlayerHasEmptyDeck()
        {
            var expected = new int[] { 1, 2, 3, 4, 5 };

            var cardGameService = new CardGameService(_gameRules);
            cardGameService.Players[0].Deck = new Stack<int>();
            cardGameService.Players[1].Deck = new Stack<int>(new int[] { 8, 9, 10 });
            cardGameService.Players[0].Discard = new List<int>(expected);
            cardGameService.Players[1].Discard = new List<int>(new int[] { 6, 7 });

            cardGameService.CheckPlayersDecks();

            Assert.Equal(new Stack<int>(expected), cardGameService.Players[0].Deck.OrderByDescending(x => x));
        }

        [Fact]
        public void FirstPlayerWin2Cards()
        {
            var cardGameService = new CardGameService(_gameRules);
            cardGameService.Players[0].Deck = new Stack<int>(new int[] { 1, 2, 3, 4, 10 });
            cardGameService.Players[1].Deck = new Stack<int>(new int[] { 1, 2, 3, 4, 5 });

            cardGameService.DrawCard();
            cardGameService.CalculateDrawOutcome();

            var expected = new List<int> { 5, 10 };

            Assert.Equal(expected, cardGameService.Players[0].Discard.OrderBy(x => x));
        }

        [Fact]
        public void FirstPlayerWin4Cards()
        {
            var cardGameService = new CardGameService(_gameRules);
            cardGameService.Players[0].Deck = new Stack<int>(new int[] { 1, 2, 3, 9, 5 });
            cardGameService.Players[1].Deck = new Stack<int>(new int[] { 1, 2, 3, 4, 5 });

            for (int i = 0; i < 2; i++)
            {
                cardGameService.DrawCard();
                cardGameService.CalculateDrawOutcome();
            }

            var expected = new List<int> { 4, 5, 5, 9 };

            Assert.Equal(expected, cardGameService.Players[0].Discard.OrderBy(x => x));
        }

        [Fact]
        public void FirstPlayerWin6Cards()
        {
            var cardGameService = new CardGameService(_gameRules);
            cardGameService.Players[0].Deck = new Stack<int>(new int[] { 1, 2, 3, 9, 5, 6 });
            cardGameService.Players[1].Deck = new Stack<int>(new int[] { 1, 2, 3, 4, 5, 6 });

            for (int i = 0; i < 3; i++)
            {
                cardGameService.DrawCard();
                cardGameService.CalculateDrawOutcome();
            }

            var expected = new List<int> { 4, 5, 5, 6, 6, 9 };

            Assert.Equal(expected, cardGameService.Players[0].Discard.OrderBy(x => x));
        }

        [Fact]
        public void FirstPlayerWinTheGame()
        {
            var cardGameService = new CardGameService(_gameRules);
            cardGameService.Players[0].Deck = new Stack<int>(new int[] { 10, 2, 3, 4, 5, 6 });
            cardGameService.Players[1].Deck = new Stack<int>(new int[] { 1, 2, 3, 4, 5, 6 });

            while (!cardGameService.GameEnded)
            {
                cardGameService.DrawCard();
                cardGameService.CalculateDrawOutcome();
            }

            Assert.Equal(cardGameService.Players[0], cardGameService.Winner);
        }

        [Fact]
        public void SecondPlayerWinTheGame()
        {
            var cardGameService = new CardGameService(_gameRules);
            cardGameService.Players[0].Deck = new Stack<int>(new int[] { 1, 2, 3, 4, 5, 6 });
            cardGameService.Players[1].Deck = new Stack<int>(new int[] { 1, 2, 10, 4, 5, 6 });

            while (!cardGameService.GameEnded)
            {
                cardGameService.DrawCard();
                cardGameService.CalculateDrawOutcome();
            }

            Assert.Equal(cardGameService.Players[1], cardGameService.Winner);
        }

        [Fact]
        public void DeckSizePersistedThroughTheGame()
        {
            var cardGameService = new CardGameService(_gameRules);

            while (!cardGameService.GameEnded)
            {
                cardGameService.DrawCard();
                cardGameService.CalculateDrawOutcome();
                Assert.Equal(_gameRules.DeckSize, cardGameService.Players.Sum(p => p.Total));
            }
        }
    }
}
