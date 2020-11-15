using System;
using System.Collections.Generic;

namespace PG3302_Eksamen
{
    public class Game
    {
        private readonly int _players;

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

            GameMessages.DebugLog("");

            // Deal initial hand to players
            for (int i = 0; i < GameConfig.DefaultMaxHandSize; i++)
            {
                foreach (Player player in players)
                {
                   dealer.DrawNormalCard(player);
                }
            }

            GameMessages.DebugLog("");
            // print hands after cards are dealt for console
            foreach (Player player in players)
            {
                GameMessages.DebugLog(player.ToString());
            }
            
            GameMessages.DebugLog("");
            
            // TODO just prints the whole deck at start for debugging
            GameMessages.DebugLog("Deck: " + dealer.GetDeck());
            GameMessages.DebugLog("");

            // Start threads
            for (int i = 0; i < _players; i++)
            {
                players[i].Start();
            }

            dealer.Started = true;
        }
    }
}