using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG3302_Eksamen
{

    class Player : ThreadProxy

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
        
        protected override void Play()
        {
            Dealer dealer = Dealer.GetDealer();
            while (!dealer.GameEnded)
            {
                if (dealer.GetAccess(this))
                {
                    // Draw card
                    Card newCard = dealer.GetCard();
                    
                    // TODO handle special cards
                    
                    _hand.Add(newCard);
                    Console.WriteLine(Name + " drew card: " + newCard);

                    int numOfDiamonds = 0;
                    int numOfSpades = 0;
                    int numOfClubs = 0;
                    int numOfHearts = 0;

                    foreach (Card card in _hand)
                    {
                        if (card.CardType == CardType.Joker)
                        {
                            numOfDiamonds++;
                            numOfClubs++;
                            numOfHearts++;
                            numOfSpades++;
                        }
                        else
                        {
                            switch (card.Suit)
                            {
                                case Suit.Clubs:
                                    numOfClubs++;
                                    break;
                                case Suit.Diamonds:
                                    numOfDiamonds++;
                                    break;
                                case Suit.Hearts:
                                    numOfHearts++;
                                    break;
                                case Suit.Spades:
                                    numOfSpades++;
                                    break;
                            }
                        }
                    }

                    // If player has 4 or more cards of same suit
                    if (numOfDiamonds > 3 || numOfClubs > 3 || numOfHearts > 3 || numOfSpades > 3)
                    {
                        Console.WriteLine(Name + " won the game with hand: " + ToString());
                        dealer.GameEnded = true;
                    }
                    else
                    {
                        int minCount = 100;
                        Suit minSuit = _hand[0].Suit;
                        if (numOfDiamonds > 0 && numOfDiamonds < minCount)
                        {
                            minCount = numOfDiamonds;
                            minSuit = Suit.Diamonds;
                        }
                        
                        if (numOfClubs > 0 && numOfClubs < minCount)
                        {
                            minCount = numOfClubs;
                            minSuit = Suit.Clubs;
                        }
                        
                        if (numOfHearts > 0 && numOfHearts < minCount)
                        {
                            minCount = numOfHearts;
                            minSuit = Suit.Hearts;
                        }
                        
                        if (numOfSpades > 0 && numOfSpades < minCount)
                        {
                            minCount = numOfSpades;
                            minSuit = Suit.Spades;
                        }

                        int i = 0;
                        while (_hand[i].Suit != minSuit)
                        {
                            i++;
                        }

                        Card returnCard = _hand[i];
                        dealer.ReturnCard(returnCard);
                        _hand.Remove(returnCard);
                        Console.WriteLine(Name + " returned card: " + returnCard);
                    }
                }
            }
            Console.WriteLine(Name + " leaving game");
            Stop();
        }
    }
}