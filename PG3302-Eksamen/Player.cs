using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG3302_Eksamen
{
    public class Player
    {
       
        public string Name { get; set; }

        private readonly List<Card> _hand = new List<Card>();

        public Player(string name)
        {
            this.Name = name;
        }

        public List<Card> GetHand()
        {
            return _hand;
        }

        public void SetHand(Card card)
        {
            _hand.Add(card);
        }

        public override string ToString()
        {
            StringBuilder hand = new StringBuilder();
            
            for (int i = 0; i < _hand.Count; i++)
            {
                hand.Append(_hand[i]);
                
                if (i < (_hand.Count - 1))
                {
                    hand.Append(", ");
                }
            }
            return Name + " has hand: " + hand;
        }
    }
}