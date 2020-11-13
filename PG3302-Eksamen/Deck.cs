using System;
using System.Collections.Generic;
using System.Text;

namespace PG3302_Eksamen
{
    public class Deck
    {
        public List<ICard> Cards { get; set; }
        private readonly Random _rng = new Random();
        private readonly List<int> _ranNums = new List<int>();
        
        public ICard GetNextCard()
        {
            ICard card = Cards[0];
            Cards.Remove(card);
            return card;
        }

        public void RestoreCard(ICard card)
        {
            Cards.Add(card);
        }

        public void GenerateDeck()
        {
            Cards = DeckFactory.CreateDeck();
        }


        public override string ToString()
        {
            StringBuilder hand = new StringBuilder();

            foreach (ICard card in Cards)
            {
                hand.Append(card.GetValue() + " of " + card.GetSuit() + " of type " + card.GetCardType() + ", ");
            }
        
            return hand.ToString();
        }
    }
}
    