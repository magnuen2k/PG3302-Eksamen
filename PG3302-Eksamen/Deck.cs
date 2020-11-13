using System;
using System.Collections.Generic;
using System.Text;

namespace PG3302_Eksamen
{
    public class Deck
    {
        private List<ICard> _cards;
        private readonly Random _rng = new Random();
        private readonly List<int> _ranNums = new List<int>();
        
        public ICard GetNextCard()
        {
            ICard card = _cards[0];
            _cards.Remove(card);
            return card;
        }

        private ICard GetRandomCard()
        {
            while (true)
            {
                int num = _rng.Next(_cards.Count);
                if (_ranNums.Contains(num)) continue;
                ICard card = _cards[num];
                _ranNums.Add(num);
                return card;
            }
        }
        
        public void RestoreCard(ICard card)
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
                int k = _rng.Next(n + 1);  
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

                Console.WriteLine(card + " " + card.GetSuit() + " " + card.GetValue());
            }
        }


        public override string ToString()
        {
            StringBuilder hand = new StringBuilder();

            foreach (ICard card in _cards)
            {
                hand.Append(card.GetValue() + " of " + card.GetSuit() + " of type " + card.GetCardType() + ", ");
            }
        
            return hand.ToString();
        }
    }
}
    