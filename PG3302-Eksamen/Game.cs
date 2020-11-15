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
            List<Player> players = CreatePlayers();
            GameMessages.DebugLog("");
            DealInitialHand(players);
            GameMessages.Space();
            
            /*
            GameMessages.DebugLog("");
            // print hands after cards are dealt for console
            foreach (Player player in players)
            {
                GameMessages.DebugLog(player.ToString());
            }
            
            GameMessages.DebugLog("");
            GameMessages.DebugLog("Deck: " + dealer.GetDeck());
            GameMessages.DebugLog("");*/

            StartGame(players);
        }

        private void StartGame(List<Player> players)
        {
            Dealer dealer = Dealer.GetDealer();

            for (int i = 0; i < _players; i++)
            {
                players[i].Start();
            }
            dealer.Started = true;
        }

        private void DealInitialHand(List<Player> players)
        {
            Dealer dealer = Dealer.GetDealer();
            for (int i = 0; i < GameConfig.DefaultMaxHandSize; i++)
            {
                foreach (Player player in players)
                {
                    dealer.DrawNormalCard(player);
                }
            }
        }

        private List<Player> CreatePlayers()
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < _players; i++)
            {
                players.Add(new Player("Player" + (i + 1), i + 1));
            }

            return players;
        }
    }
}