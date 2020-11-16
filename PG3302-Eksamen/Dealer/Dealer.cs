using System;
using System.Threading;
using PG3302_Eksamen.Card;
using PG3302_Eksamen.Deck;
using PG3302_Eksamen.Game;

namespace PG3302_Eksamen.Dealer
{
    public class Dealer
    {
        private static Dealer _dealer = null;

        private readonly IDeck _deck = null;
        public bool Started { set; get;}
        public bool GameEnded { set; get; }
        private int _activePlayer = 0;
        private readonly object _lock;

        private Dealer()
        {
            _lock = new object();
            _deck = DeckFactory.CreateDeck();
            Started = false;
            GameEnded = false;
        }

        public static Dealer GetDealer()
        {
            if (_dealer == null)
            {
                _dealer = new Dealer();
            }
            return _dealer;
        }

        private void RandomTimeout()
        {
            Random r = new Random();
            Thread.Sleep(r.Next(100));
        }

        public Boolean GetAccess(Player.Player player)
        {
            // Each thread sleeps a random number of milliseconds to randomize access
            RandomTimeout();
            
            lock (_lock)
            {
                if (!Started || GameEnded)
                    return false;
                
                if (_activePlayer != 0 && _activePlayer != player.Id)
                    return false;
                
                // Make each thread sleep before playing round
                Thread.Sleep(GameConfig.GameSpeed);
                _activePlayer = player.Id;
                return true;
            }
        }
        
        public ICard GetCard()
        {
            return _deck.GetNextCard();
        }
        public void ReturnCard(ICard card)
        {
            if (card.GetCardType() != CardType.Normal)
                ReturnSpecialCard(card);
            else
                _deck.RestoreCard(card);
        }

        private void ReturnSpecialCard(ICard card)
        {
            Random r = new Random();
            _deck.RestoreCard(card, r.Next(_deck.Size()));
        }

        public IDeck GetDeck()
        {
            return _deck;
        }
        
        public void CloseAccess()
        {
            lock (_lock)
            {
                _activePlayer = 0;
            }
        }

        public void DrawNormalCard(Player.Player player)
        {
            while (true)
            {
                ICard card = GetCard();
                if (card.GetCardType() != CardType.Normal)
                {
                    ReturnSpecialCard(card);
                    continue;
                }
                player.AddToHand(card);
                GameMessages.ReceiveCard(player.Name, card);
                break;
            }
        }

        public void ClaimVictory(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(Player.Player)) return;
            GameMessages.WinningMessage((Player.Player) sender);
            GameEnded = true;
        }
    }
}