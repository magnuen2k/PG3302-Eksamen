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
            _deck = new Deck();
            _deck.GenerateDeck();
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

        public Boolean GetAccess(Player player)
        {
            // TODO add this to its own method?
            // Each thread sleeps a random number of milliseconds to randomize access
            Random r = new Random();
            Thread.Sleep(r.Next(100));
            
            lock (_lock)
            {
                if (!Started)
                    return false;
                
                if (_activePlayer != 0 && _activePlayer != player.id)
                    return false;
                
                // Make each thread sleep before playing round
                Thread.Sleep(500);
                _activePlayer = player.id;
                return true;
            }
        }

        public Card GetCard()
        {
            lock (_lock)
            {
                return _deck.GetNextCard();
            }
        }
        public Boolean ReturnCard(Card card)
        {
            
            lock (_lock)
            {
                _deck.RestoreCard(card);
            }
            return true;
        }
        
        public void CloseAccess()
        {
            lock (_lock)
            {
                _activePlayer = 0;
            }
            
        }
    }
}