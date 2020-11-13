using System;
using System.Collections.Generic;
using System.Text;

namespace PG3302_Eksamen
{
    public class Deck
    {
   
        private List<ICard> _cards;
        private readonly Random rng = new Random();

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
        
        private void RestoreSpecialCard(ICard card)
        {
            _cards.Add(card);
        }

        public void GenerateDeck()
        {
            int count = 0;
            _cards = new List<ICard>();
            foreach (Value value in Enum.GetValues(typeof(Value)))
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    count++;
                    _cards.Add(new Card(suit, value));
                }
            }
            Shuffle(_cards);

            // generates special cards based on cards in the Enum type
            GenerateSpecialCards();
      
            Shuffle(_cards);
            Console.WriteLine(count);
        }
        

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
            /*foreach (CardType type in Enum.GetValues(typeof(CardType)))
            {
                if (type == CardType.Normal)
                    continue;

                Card card = GetNextCard();
                card.SetCardType(type);
                Console.WriteLine(card);
                RestoreCard(card);
            }*/
            
            foreach (CardType type in Enum.GetValues(typeof(CardType)))
            {
                if (type == CardType.Normal)
                    continue;
                
                ICard card = GetNextCard();

                if (type == CardType.Joker)
                {
                    card = new JokerCard(card);
                    RestoreSpecialCard(card);
                }
                
                
                Console.WriteLine(card + " " + card.GetSuit() + " " + card.GetValue());
            }
        }


        public override string ToString()
        {
            StringBuilder hand = new StringBuilder();

            foreach (var card in _cards)
            {
                hand.Append(card.GetValue() + " of " + card.GetSuit() + " of type " + card.GetCardType() + ", ");
            }
        
            return hand.ToString();
        }
    }
}
    