using System;
using System.Collections.Generic;

namespace PG3302_Eksamen
{
    public class Game
    {
        private readonly int _players;
        public static bool ShouldWeContinueTheLoop = false;

        public Game(int players)
        {
            _players = players;
        }

        public void Run()
        {
            // create the dealer
            Dealer dealer = Dealer.GetDealer();
            
            // Create players
            List<Player> players = new List<Player>();
            for (int i = 0; i < _players; i++)
            {
                players.Add(new Player("Player" + (i + 1), i + 1));
            }

            Console.WriteLine("");

            // Deal initial hand to players
            for (int i = 0; i < GameConfig.DefaultMaxHandSize; i++)
            {
                foreach (Player player in players)
                {
                   dealer.DrawNormalCard(player);
                }
            }

            Console.WriteLine("");
            // print hands after cards are dealt for console
            foreach (Player player in players)
            {
                Console.WriteLine(player);
            }
            
            Console.WriteLine("");
            Console.WriteLine("Deck: " + dealer.GetDeck());
            Console.WriteLine("");

            // Start threads
            for (int i = 0; i < _players; i++)
            {
                players[i].Start();
            }

            dealer.Started = true;
        }
    }
}