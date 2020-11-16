using System;
using System.Collections.Generic;
using PG3302_Eksamen.Card;
using PG3302_Eksamen.Deck;
using PG3302_Eksamen.Game;

namespace PG3302_Eksamen
{
    public static class DeckFactory
    {
        private static readonly Random Rng = new Random();
        private static readonly List<ICard> Cards = new List<ICard>();

        // Create a new shuffled deck
        public static IDeck CreateDeck()
        {
            GenerateNormalCards();
            GenerateSpecialCards();
            Shuffle(Cards);
            return new Deck.Deck(Cards);
        }

        private static void GenerateNormalCards()
        {
            foreach (Value value in Enum.GetValues(typeof(Value)))
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    Cards.Add(CardFactory.CreateCard(suit, value));
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

                card = type switch
                {
                    CardType.Joker => new JokerCard(card),
                    CardType.Bomb => new BombCard(card),
                    CardType.Vulture => new VultureCard(card),
                    CardType.Quarantine => new QuarantineCard(card),
                    _ => card
                };
                GameMessages.DebugLog("Assigned " + card.GetValue() + " of " + card.GetSuit() + ", " + card);
            }
        }

        private static ICard GetRandomCard()
        {
            while (true)
            {
                int num = Rng.Next(Cards.Count);
                if (Cards[num].GetCardType() == CardType.Normal)
                    return Cards[num];
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