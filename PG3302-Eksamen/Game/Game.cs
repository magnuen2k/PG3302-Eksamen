using System;
using System.Collections.Generic;
using PG3302_Eksamen.GameHandlers;

namespace PG3302_Eksamen.Game
{
    public class Game : IGame
    {
        private readonly int _players;
        
        // Keep track of how many threads running
        private int _activeThreads;
        
        // Using EventHandler to start the game
        public event EventHandler StartGame;

        public Game(int players)
        {
            _players = players;
            this._activeThreads = 0;
        }

        public void Run()
        {
            Dealer.Dealer dealer = Dealer.Dealer.GetDealer();
            List<Player.Player> players = CreatePlayers();
            
            CreateSubscribers(players);
            
            GameMessages.DebugLog("");
            DealInitialHand(players);
            GameMessages.Space();
            
            //---------
            GameMessages.DebugLog("ENTERED DEBUG MODE");
            GameMessages.DebugLog("");
            
            // print hands after cards are dealt for console
            foreach (Player.Player player in players)
            {
                GameMessages.DebugLog(player.ToString());
            }
            
            GameMessages.DebugLog("");
            GameMessages.DebugLog("Deck: " + dealer.GetDeck());
            GameMessages.DebugLog("");
            //---------

            // Start player threads and start game
            InitializeGame(players);
        }
        
        private List<Player.Player> CreatePlayers()
        {
            List<Player.Player> players = new List<Player.Player>();
            for (int i = 0; i < _players; i++)
            {
                players.Add(PlayerFactory.CreatePlayer("Player" + (i + 1), i + 1));
            }

            return players;
        }

        private void CreateSubscribers(List<Player.Player> players)
        {
            Dealer.Dealer dealer = Dealer.Dealer.GetDealer();
            foreach (Player.Player player in players)
            {
                player.ClaimVictory += dealer.ClaimVictory;
            }
            HandleCard.BombIdentified += HandleCard.OnBombIdentified;
            StartGame += dealer.StartGame;
        }

        private void DealInitialHand(List<Player.Player> players)
        {
            Dealer.Dealer dealer = Dealer.Dealer.GetDealer();
            for (int i = 0; i < GameConfig.DefaultMaxHandSize; i++)
            {
                foreach (Player.Player player in players)
                {
                    dealer.DrawNormalCard(player);
                }
            }
        }
        
        private void InitializeGame(List<Player.Player> players)
        {
            for (int i = 0; i < _players; i++)
            {
                players[i].Start();
                players[i].LeaveGame += PlayerLeftGame;
                _activeThreads++;
            }
            OnStartGame();
        }

        private void PlayerLeftGame(object sender, EventArgs e)
        {
            _activeThreads--;
            if (_activeThreads == 0)
            {
                GameMessages.GameEnded();
            }
        }

        protected virtual void OnStartGame()
        {
            StartGame?.Invoke(this, EventArgs.Empty);
        }
    }
}