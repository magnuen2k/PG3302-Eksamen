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
        public int id { get; set; }
        private bool IsQuarantined { get; set; }


        private Hand _hand;

        public Player(string name, int id)
        {
            Name = name;
            this.id = id;
            IsQuarantined = false;

            _hand = new Hand();
        }

        public List<Card> GetHand()
        {
            return _hand._hand;
        }

        public void GiveCard(Card card)
        {
            _hand.Add(card);
        }

        public void RemoveCard(Card card)
        {
            _hand._hand.Remove(card);
        }

        public override string ToString()
        {
            return Name + " has hand: " + _hand;
        }

        protected override void Play()
        {
            Dealer dealer = Dealer.GetDealer();
            while (!dealer.GameEnded)
            {
                if (dealer.GetAccess(this))
                {
                    if (IsQuarantined)
                    {
                        Console.WriteLine(Name + " is quarantined, sitting out this round :(");
                        Console.WriteLine("");
                        IsQuarantined = false;
                        dealer.CloseAccess();
                        return;
                    }
                    
                    // Draw card
                    Card newCard = dealer.GetCard();
                    
                    // TODO handle special cards
                    
                    GiveCard(newCard);
                    Console.WriteLine(Name + " drew card: " + newCard);

                    Console.WriteLine(this); // TODO this is for debugging

                    int numOfDiamonds = 0;
                    int numOfSpades = 0;
                    int numOfClubs = 0;
                    int numOfHearts = 0;
                    

                    foreach (Card card in _hand._hand)
                    {
                        // Skip normal cards here to avoid checking more if's - redundant?
                        if (card.CardType == CardType.Normal || card.CardType == CardType.Joker)
                        {
                            continue;
                        }
                        
                        if (card.CardType == CardType.Vulture)
                        {
                            while (true)
                            {
                                Card newVultureCard = dealer.GetCard();
                                if (newVultureCard.CardType == CardType.Normal)
                                {
                                    GiveCard(newVultureCard);
                                    Console.WriteLine(Name + " drew card: " + newVultureCard);
                                    break;
                                }
                            }
                            RemoveCard(card); // we gain another card so our hand size is 5. Vulture effect is present by not removing a card, but we dont want to count the suit from it
                            _hand.MaxHandSize++;
                            Console.WriteLine("New max hand size: " + _hand.MaxHandSize);
                            Console.WriteLine(this);
                            break;
                        }

                        if (card.CardType == CardType.Quarantine)
                        {
                            IsQuarantined = true;
                            dealer.ReturnCard(card);
                            RemoveCard(card);
                            Console.WriteLine(Name + " is now quarantined!");
                            Console.WriteLine(Name + ": Spades: " + numOfSpades + ", Clubs: " + numOfClubs + ", Diamonds: " + numOfDiamonds + ", Hearts: " + numOfHearts); // TODO: temp for debugging
                            Console.WriteLine(Name + " returned card: " + card);
                            Console.WriteLine("");
                            dealer.CloseAccess();
                            return;
                        }

                        if (card.CardType == CardType.Bomb)
                        {
                            Console.WriteLine(Name + " has to throw away all his cards :(");
                            /*foreach (Card c in _hand)
                            {
                                Console.WriteLine(c);
                                dealer.ReturnCard(c);
                                //_hand.Remove(c);
                                Console.WriteLine(Name + " returned card: " + c);
                            }*/
                            for (int i = _hand._hand.Count - 1; i >= 0; i--)
                            {
                                var c = _hand._hand[i];
                                dealer.ReturnCard(c);
                                RemoveCard(c);
                                Console.WriteLine(Name + " returned card: " + c);
                            }

                            Console.WriteLine(Name + " draws a new hand...");
                            
                            // by hardcoding this to 4, it means bomb will also blow up the vulture card and NOT return it to the deck
                            for (int i = 0; i < _hand.MaxHandSize; i++)
                            {
                                // New hand can not give special cards
                                while (true)
                                {
                                    Card newCardAfterBomb = dealer.GetCard();
                                    if (newCardAfterBomb.CardType == CardType.Normal)
                                    {
                                        GiveCard(newCardAfterBomb);
                                        Console.WriteLine(Name + " drew card: " + newCardAfterBomb);
                                        break;
                                    }
                                }
                            }

                            Console.WriteLine(this);
                            
                            break;
                        }
                    }
                    
                    foreach (Card card in _hand._hand)
                    {
                        // Ignore special cards when counting
                        if (card.CardType != CardType.Normal)
                        {
                            continue;
                        }

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

                    foreach (Card card in _hand._hand)
                    {
                        if (card.CardType == CardType.Joker)
                        {
                            var highestSuit = new[]
                                {
                                    Tuple.Create(numOfDiamonds, "diamonds"),
                                    Tuple.Create(numOfSpades, "spades"),
                                    Tuple.Create(numOfClubs, "clubs"),
                                    Tuple.Create(numOfHearts, "hearts")
                                }.Max()
                                .Item2;
                            Console.WriteLine("Max: " + highestSuit);

                            switch (highestSuit)
                            {
                                case "diamonds":
                                    numOfDiamonds++;
                                    card.Suit = Suit.Diamonds;
                                    break;
                                case "spades":
                                    numOfSpades++;
                                    card.Suit = Suit.Spades;
                                    break;
                                case "clubs":
                                    numOfClubs++;
                                    card.Suit = Suit.Clubs;
                                    break;
                                case "hearts":
                                    numOfHearts++;
                                    card.Suit = Suit.Hearts;
                                    break;
                            }
                        }
                    }
                    
                    Console.WriteLine(Name + ": Spades: " + numOfSpades + ", Clubs: " + numOfClubs + ", Diamonds: " + numOfDiamonds + ", Hearts: " + numOfHearts); // TODO: temp for debugging

                    // If player has 4 or more cards of same suit
                    if (numOfDiamonds > 3 || numOfClubs > 3 || numOfHearts > 3 || numOfSpades > 3)
                    {
                        Console.WriteLine(Name + " won the game with hand: " + ToString());
                        dealer.GameEnded = true;
                    }
                    else
                    {
                        if (newCard.CardType == CardType.Vulture)
                        {
                            Console.WriteLine("Because card drawn was vulture, we don't throw a card!");
                            Console.WriteLine("");
                            dealer.CloseAccess();
                            return;
                        }
                        int minCount = 100;
                        Suit minSuit = _hand._hand[0].Suit;
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
                        while (_hand._hand[i].Suit != minSuit)
                        {
                            //Console.WriteLine(_hand[i]);
                            i++;
                        }
                        
                        Card returnCard = _hand._hand[i];
                        dealer.ReturnCard(returnCard);
                        RemoveCard(returnCard);
                        Console.WriteLine(Name + " returned card: " + returnCard);
                        Console.WriteLine("");
                        dealer.CloseAccess();
                    }
                }
            }
            Console.WriteLine(Name + " leaving game");
            Stop();
        }
    }
}