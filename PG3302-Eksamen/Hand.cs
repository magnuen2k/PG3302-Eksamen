using System.Collections.Generic;
using System.Text;

namespace PG3302_Eksamen
{
    class Hand
    {
        private readonly List<Card> _hand;
        public int MaxHandSize { get; set; }

        public Hand()
        {
            _hand = new List<Card>();
            MaxHandSize = 4;
        }


        public override string ToString()
        {
            StringBuilder hand = new StringBuilder();
            
            foreach (var c in _hand)
            {
                hand.Append(c + ", ");
            }
            return hand.ToString().TrimEnd(',', ' ');
        }

        public void GiveCard(Card card)
        {
            _hand.Add(card);
        }

        public void RemoveCard(Card card)
        {
            _hand.Remove(card);
        }

        public int Count()
        {
            return _hand.Count;
        }

        public List<Card> GetHand()
        {
            return _hand;
        }
    }
}