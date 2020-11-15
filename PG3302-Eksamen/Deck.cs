using System;
using System.Collections.Generic;
using System.Text;

namespace PG3302_Eksamen
{
    public class Deck
    {
        private readonly List<ICard> _cards;

        // To make sure you cannot new empty deck
        private Deck()
        {
        }

        public Deck(List<ICard> cards)
        {
            _cards = cards;
        }
        
        public ICard GetNextCard()
        {
            ICard card = _cards[0];
            _cards.Remove(card);
            return card;
        }

        public void RestoreCard(ICard card)
        {
            _cards.Add(card);
        }

        public override string ToString()
        {
            StringBuilder hand = new StringBuilder();

            foreach (ICard card in _cards)
            {
                //hand.Append(card.GetValue() + " of " + card.GetSuit() + " of type " + card.GetCardType() + ", ");
                hand.Append(card + ", ");
            }
            return hand.ToString();
        }
    }
}
    