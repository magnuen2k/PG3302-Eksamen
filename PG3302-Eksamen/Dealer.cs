using System;
using System.Threading;

namespace PG3302_Eksamen
{
    class Dealer
    {
        private static Dealer _dealer = null;

        private Deck _deck = null;
        public Boolean Started { set; get;}
        public Boolean GameEnded { set; get; }
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

        private void RandomSleep()
        {
            Random r = new Random();
            Thread.Sleep(r.Next(100));
        }

        public Boolean GetAccess(Player player)
        {
            // Each thread sleeps a random number of milliseconds to randomize access
            RandomSleep();
            
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

        public Deck GetDeck()
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

        public void DrawNormalCard(Player player)
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
                Console.WriteLine(player.Name + " receives card: " + card);
                break;
            }
        }

        public void ClaimVictory(Player player)
        {
            GameMessages.WinningMessage(player);
            GameEnded = true;
        }
    }
}