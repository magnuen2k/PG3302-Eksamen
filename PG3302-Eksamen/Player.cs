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

        public ArrayList _hand = new ArrayList();

        public Player(string name)
        {
            this.Name = name;
        }

        public ArrayList getHand()
        {
            return _hand;
        }

        public void setHand(Card card)
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