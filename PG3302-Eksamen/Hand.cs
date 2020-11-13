using System.Collections.Generic;
using System.Text;

namespace PG3302_Eksamen
{
    class Hand
    {
        public List<Card> _hand;
        public int MaxHandSize { get; set; }

        public Hand()
        {
            _hand = new List<Card>();
            MaxHandSize = 4;
        }


        public override string ToString()
        {
            StringBuilder hand = new StringBuilder();
            
            for (int i = 0; i < _hand.Count; i++)
            {
                hand.Append(_hand[i] + ", ");
                
            }
            return hand.ToString().TrimEnd(',', ' ');
        }
    }
}