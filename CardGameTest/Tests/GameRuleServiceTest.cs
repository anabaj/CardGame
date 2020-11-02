using CardGame.Models;
using CardGame.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CardGameTest.Tests
{
    public class GameRuleServiceTest
    {
        [Fact]
        public void AllArgumentsSpecified()
        {
            var gameRuleService = new GameRuleService();
            var args = new string[] { "--suits", "spade", "diamond", "heart", "club", "--size", "80", "--players", "4" };

            var actual = gameRuleService.InitializeRules(args);
            var expected = new GameRules
            {
                NumberOfPlayers = 4,
                DeckSize = 80,
                Suits = new List<string> { "spade", "diamond", "heart", "club" }
            };

            Assert.Equal(expected.DeckSize, actual.DeckSize);
            Assert.Equal(expected.NumberOfPlayers, actual.NumberOfPlayers);
            Assert.Equal(expected.EnableSuits, actual.EnableSuits);
        }

        [Fact]
        public void OneArgumentSpecified()
        {
            var gameRuleService = new GameRuleService();
            var args = new string[] { "--suits", "spade", "diamond", "heart", "club" };

            var actual = gameRuleService.InitializeRules(args);
            var expected = new GameRules
            {
                NumberOfPlayers = 2,
                DeckSize = 40,
                Suits = new List<string> { "spade", "diamond", "heart", "club" }
            };

            Assert.Equal(expected.DeckSize, actual.DeckSize);
            Assert.Equal(expected.NumberOfPlayers, actual.NumberOfPlayers);
            Assert.Equal(expected.EnableSuits, actual.EnableSuits);
        }

        [Fact]
        public void NoArgumentSpecified()
        {
            var gameRuleService = new GameRuleService();
            var args = new string[] { };

            var actual = gameRuleService.InitializeRules(args);
            var expected = new GameRules { NumberOfPlayers = 2, DeckSize = 40 };

            Assert.Equal(expected.DeckSize, actual.DeckSize);
            Assert.Equal(expected.NumberOfPlayers, actual.NumberOfPlayers);
            Assert.Equal(expected.EnableSuits, actual.EnableSuits);
        }

        [Fact]
        public void WrongArgumentSpecified()
        {
            var gameRuleService = new GameRuleService();
            var args = new string[] { "--suits", "true", "--size", "80", "--players", "wrong!!!" };

            Exception exception = null;
            try
            {
                gameRuleService.InitializeRules(args);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.NotNull(exception);
        }
    }
}
