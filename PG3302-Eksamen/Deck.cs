using System;
using System.Collections.Generic;
using System.Text;

namespace PG3302_Eksamen
{
    public class Deck
    {
        private List<ICard> _cards;
        private readonly Random _rng = new Random();
        private readonly List<int> _ranNums = new List<int>();
        
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

        public void GenerateDeck()
        {
            _cards = DeckFactory.CreateDeck();
        }


        public override string ToString()
        {
            StringBuilder hand = new StringBuilder();

            foreach (ICard card in _cards)
            {
                hand.Append(card.GetValue() + " of " + card.GetSuit() + " of type " + card.GetCardType() + ", ");
            }
        
            return hand.ToString();
        }
    }
}
    