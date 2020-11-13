using System;
using System.Collections.Generic;

namespace PG3302_Eksamen
{
    static class DeckFactory
    {
        private static readonly Random Rng = new Random();
        private static readonly List<ICard> Cards = new List<ICard>();
        private static readonly List<int> RanNums = new List<int>();

        public static List<ICard> CreateDeck()
        {
            GenerateNormalCards();
            GenerateSpecialCards();
            Shuffle(Cards);
            return Cards;
        }

        private static void GenerateNormalCards()
        {
            foreach (Value value in Enum.GetValues(typeof(Value)))
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    Cards.Add(new Card(suit, value));
                }
            }
        }

        private static void GenerateSpecialCards()
        {
            foreach (CardType type in Enum.GetValues(typeof(CardType)))
            {
                if (type == CardType.Normal)
                    continue;
                
                ICard card = GetRandomCard();

                if (type == CardType.Joker)
                    card = new JokerCard(card);
                else if (type == CardType.Bomb)
                    card = new BombCard(card);
                else if (type == CardType.Vulture)
                    card = new VultureCard(card);
                else if (type == CardType.Quarantine)
                    card = new QuarantineCard(card);

                Console.WriteLine(card + " " + card.GetSuit() + " " + card.GetValue());
            }
        }

        private static ICard GetRandomCard()
        {
            while (true)
            {
                int num = Rng.Next(Cards.Count);
                if (RanNums.Contains(num)) continue;
                ICard card = Cards[num];
                RanNums.Add(num);
                return card;
            }
        }
        
        private static void Shuffle<T>(IList<T> list)  
        {
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = Rng.Next(n + 1);  
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }
        }
    }
}