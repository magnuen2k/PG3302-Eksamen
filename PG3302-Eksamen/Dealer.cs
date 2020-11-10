using System;

namespace PG3302_Eksamen
{
    class Dealer
    {
        private static Dealer _dealer = null;

        private Deck _deck = null;
        public Boolean Started { set; get;}
        public Boolean GameEnded { set; get; }
        private Player _activePlayer = null;
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
            lock (_lock)
            {
                if (!Started)
                    return false;
                
                if (_activePlayer != null && _activePlayer != player)
                    return false;
                _activePlayer = player;
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
                _activePlayer = null;
            }
            return true;
        }
    }
}