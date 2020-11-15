using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG3302_Eksamen
{
    public class Hand
    {
        private readonly List<ICard> _hand;
        public int MaxHandSize { get; set; }
        public int NumOfDiamonds { get; private set; }
        public int NumOfSpades { get; private set; }
        public int NumOfHearts { get; private set; }
        public int NumOfClubs { get; private set; }
        public bool HasJoker { get; private set; }

        public Hand()
        {
            HasJoker = false;
            NumOfDiamonds = 0;
            NumOfSpades = 0;
            NumOfHearts = 0;
            NumOfClubs = 0;
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

        private void CountSuit(ICard card, bool addCard)
        {
            if (card.GetCardType() == CardType.Joker)
            {
                HasJoker = addCard;
            }
            else if(card.GetCardType() == CardType.Normal)
            {
                int count = 1;
                if (!addCard)
                    count = -1;
                switch (card.GetSuit())
                {
                    case Suit.Clubs:
                        NumOfClubs += count;
                        break;
                    case Suit.Diamonds:
                        NumOfDiamonds += count;
                        break;
                    case Suit.Hearts:
                        NumOfHearts += count;
                        break;
                    case Suit.Spades:
                        NumOfSpades += count;
                        break;
                }
            }
        }

        public Suit BestSuit()
        {
            return new[]
                {
                    Tuple.Create(NumOfDiamonds, Suit.Diamonds),
                    Tuple.Create(NumOfSpades, Suit.Spades),
                    Tuple.Create(NumOfClubs, Suit.Clubs),
                    Tuple.Create(NumOfHearts, Suit.Hearts)
                }.Max()
                .Item2;
        }
        
        public int BestSuitCount()
        {
            return new[]
                {
                    NumOfDiamonds,
                    NumOfSpades,
                    NumOfClubs,
                    NumOfHearts
                }.Max();
        }

        public ICard CardOfWorstSuit()
        {
            int minCount = 100;
            Suit minSuit = _hand[0].GetSuit();
            if (NumOfDiamonds > 0 && NumOfDiamonds < minCount)
            {
                minCount = NumOfDiamonds;
                minSuit = Suit.Diamonds;
            }
                        
            if (NumOfClubs > 0 && NumOfClubs < minCount)
            {
                minCount = NumOfClubs;
                minSuit = Suit.Clubs;
            }
                        
            if (NumOfHearts > 0 && NumOfHearts < minCount)
            {
                minCount = NumOfHearts;
                minSuit = Suit.Hearts;
            }
                        
            if (NumOfSpades > 0 && NumOfSpades < minCount)
            {
                minCount = NumOfSpades;
                minSuit = Suit.Spades;
            }
                
            int i = 0;
            while (_hand[i].GetSuit() != minSuit || _hand[i].GetCardType() != CardType.Normal)
            {
                i++;
            }

            return _hand[i];

        }

        public void GiveCard(ICard card)
        {
            CountSuit(card, true);
            _hand.Add(card);
        }

        public void RemoveCard(ICard card)
        {
            CountSuit(card, false);
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

        public static void DrawNewHand(Player player)
        {
            ReturnFullHand(player);
            Dealer dealer = Dealer.GetDealer();
            GameMessages.DrawNewHand(player.Name);
            for (int i = 0; i < player.Hand.MaxHandSize; i++)
            {
                dealer.DrawNormalCard(player);
            }
        }
        
        private static void ReturnFullHand(Player player)
        {
            Dealer dealer = Dealer.GetDealer();
            for (int i = player.Hand.Count() - 1; i >= 0; i--)
            {
                ICard card = player.Hand.GetHand()[i];
                dealer.ReturnCard(card);
                player.Hand.RemoveCard(card);
            }
        }
    }
}