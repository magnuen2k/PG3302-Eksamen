using System.Collections.Generic;
using System.Text;
using PG3302_Eksamen.Card;

namespace PG3302_Eksamen.Deck
{
    public class Deck : IDeck
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

        public int Size()
        {
            return _cards.Count;
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

        public void RestoreCard(ICard card, int position)
        {
            _cards.Insert(position, card);
        }

        public override string ToString()
        {
            StringBuilder hand = new StringBuilder();

            foreach (ICard card in _cards)
            {
                hand.Append(card + ", ");
            }
            return hand.ToString();
        }
    }
}
    