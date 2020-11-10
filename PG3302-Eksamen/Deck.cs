using System;
using System.Collections.Generic;
using System.Text;

namespace PG3302_Eksamen
{
    public class Deck
    {
   
        private List<Card> _cards;

        public Card GetNextCard()
        {
            Card card = _cards[0];
            _cards.Remove(card);
            return card;
        }
        
        public void RestoreCard(Card card)
        {
            _cards.Add(card);
        }
        

        public List<Card> GenerateDeck()
        {
            int count = 0;
            _cards = new List<Card>();
            foreach (Value value in Enum.GetValues(typeof(Value)))
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    count++;
                    _cards.Add(new Card(suit, value));
                }
            }
            Console.WriteLine(count);

            return _cards;
        }


        public override string ToString()
        {
            StringBuilder hand = new StringBuilder();

            foreach (Card card in _cards)
            {
                hand.Append(card.Value + " of " + card.Suit);
            }
        
            return hand.ToString();
        }
    }
}
    