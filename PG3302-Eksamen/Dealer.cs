using System;
using System.Collections.Generic;
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
                
                if (_activePlayer != 0 && _activePlayer != player.Id)
                    return false;
                
                // Make each thread sleep before playing round
                Thread.Sleep(500);
                _activePlayer = player.Id;
                return true;
            }
        }
        

        public ICard GetCard()
        {
            lock (_lock)
            {
                return _deck.GetNextCard();
            }
        }
        public void ReturnCard(ICard card)
        {
            
            lock (_lock)
            {
                _deck.RestoreCard(card);
            }
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
                if (card.GetCardType() != CardType.Normal) continue;
                player.TakeCard(card);
                Console.WriteLine(player.Name + " receives card: " + card);
                break;
            }
        }
    }
}