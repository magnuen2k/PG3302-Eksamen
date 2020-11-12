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
        public bool IsQuarantined { get; set; }

        private readonly List<Card> _hand = new List<Card>();

        public Player(string name, int id)
        {
            this.Name = name;
            this.id = id;
            IsQuarantined = false;
        }

        public List<Card> GetHand()
        {
            return _hand;
        }

        public void GiveCard(Card card)
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
                    
                    _hand.Add(newCard);
                    Console.WriteLine(Name + " drew card: " + newCard);

                    Console.WriteLine(this); // TODO this is for debugging

                    int numOfDiamonds = 0;
                    int numOfSpades = 0;
                    int numOfClubs = 0;
                    int numOfHearts = 0;
                    

                    foreach (Card card in _hand)
                    {
                        // Ignore special cards when counting
                        if (card.CardType == CardType.Joker || card.CardType == CardType.Quarantine || card.CardType == CardType.Bomb)
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

                    foreach (Card card in _hand)
                    {
                        // Skip normal cards here to avoid checking more if's - redundant?
                        if (card.CardType == CardType.Normal)
                        {
                            continue;
                        }
                        
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

                        if (card.CardType == CardType.Quarantine)
                        {
                            IsQuarantined = true;
                            dealer.ReturnCard(card);
                            _hand.Remove(card);
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
                            Console.WriteLine(Name + ": Spades: " + numOfSpades + ", Clubs: " + numOfClubs + ", Diamonds: " + numOfDiamonds + ", Hearts: " + numOfHearts); // TODO: temp for debugging
                            /*foreach (Card c in _hand)
                            {
                                Console.WriteLine(c);
                                dealer.ReturnCard(c);
                                //_hand.Remove(c);
                                Console.WriteLine(Name + " returned card: " + c);
                            }*/
                            for (int i = _hand.Count - 1; i >= 0; i--)
                            {
                                var c = _hand[i];
                                dealer.ReturnCard(c);
                                //Console.WriteLine("Returns card " + c + " to dealer");
                                _hand.Remove(c);
                                Console.WriteLine(Name + " returned card: " + c);
                            }

                            Console.WriteLine(Name + " draws a new hand...");
                            for (int i = 0; i < 4; i++)
                            {
                                Card newCardAfterBomb = dealer.GetCard();
                                _hand.Add(newCardAfterBomb);
                                Console.WriteLine(Name + " drew card: " + newCardAfterBomb);

                            }
                            
                            Console.WriteLine(this);
                            
                            foreach (Card cardX in _hand)
                            {
                                // Ignore special cards when counting
                                /*if (cardX.CardType == CardType.Joker || cardX.CardType == CardType.Quarantine || cardX.CardType == CardType.Bomb)
                                {
                                    continue;
                                }*/

                                switch (cardX.Suit)
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
                            //Console.WriteLine(_hand[i]);
                            i++;
                        }
                        
                        Card returnCard = _hand[i];
                        dealer.ReturnCard(returnCard);
                        _hand.Remove(returnCard);
                        Console.WriteLine(Name + ": Spades: " + numOfSpades + ", Clubs: " + numOfClubs + ", Diamonds: " + numOfDiamonds + ", Hearts: " + numOfHearts); // TODO: temp for debugging
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