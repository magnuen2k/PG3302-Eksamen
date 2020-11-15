using System.Collections.Generic;
using System.Text;

namespace PG3302_Eksamen
{
    public class Hand
    {
        private readonly List<ICard> _hand;
        public int MaxHandSize { get; set; }

        public Hand()
        {
            _hand = new List<ICard>();
            MaxHandSize = GameConfig.DefaultMaxHandSize;
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

        public void GiveCard(ICard card)
        {
            _hand.Add(card);
        }

        public void RemoveCard(ICard card)
        {
            _hand.Remove(card);
        }

        public int Count()
        {
            return _hand.Count;
        }

        public List<ICard> GetHand()
        {
            return _hand;
        }
    }
}