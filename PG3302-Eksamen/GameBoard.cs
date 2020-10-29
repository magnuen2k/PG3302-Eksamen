using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PG3302_Eksamen
{
    public class GameBoard
    {
        private static Queue<Card> _cards;

        public static Queue<Card> GenerateDeck()
        {
            int count = 0;
            _cards = new Queue<Card>();
            foreach (Value value in Enum.GetValues(typeof(Value)))
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    count++;
                    _cards.Enqueue(new Card()
                    {
                        Suit = suit,
                        Value = value
                    });
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