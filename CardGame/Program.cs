using CardGame.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CardGame
{
    partial class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var gameRuleService = new GameRuleService();
                var gameRules = gameRuleService.InitializeRules(args);

                var cardGameService = new CardGameService(gameRules);
                cardGameService.StartGame();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
