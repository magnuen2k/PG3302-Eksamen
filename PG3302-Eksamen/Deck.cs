using System;
using System.Collections.Generic;
using System.Text;

namespace PG3302_Eksamen
{
    public class Deck
    {
   
        private List<Card> _cards;

        public Card GetNextCard()
        {
            Card card = _cards[0];
            _cards.Remove(card);
            return card;
        }
        
        public void RestoreCard(Card card)
        {
            _cards.Add(card);
        }
        

        public void GenerateDeck()
        {
            int count = 0;
            _cards = new List<Card>();
            foreach (Value value in Enum.GetValues(typeof(Value)))
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    count++;
                    _cards.Add(new Card(suit, value));
                }
            }
            Console.WriteLine(count);
            Shuffle(_cards);

            // generates special cards based on cards in the Enum type
            GenerateSpecialCards();
            /*foreach (CardType type in Enum.GetValues(typeof(CardType)))
            {
                if (type == CardType.Normal)
                    continue;
                
                Card card = GetNextCard();
                card.CardType = type;
                Console.WriteLine(card); // print the special card assigned
                RestoreCard(card);
            }*/

            Shuffle(_cards);
            //return _cards;
        }
        
        private Random rng = new Random();

        private void Shuffle<T>(IList<T> list)  
        {
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = rng.Next(n + 1);  
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }
        }

        private void GenerateSpecialCards()
        {
            foreach (CardType type in Enum.GetValues(typeof(CardType)))
            {
                if (type == CardType.Normal)
                {
                    continue;
                }

                Card card = GetNextCard();
                card.CardType = type;
                Console.WriteLine(card);
                RestoreCard(card);
            }
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
    