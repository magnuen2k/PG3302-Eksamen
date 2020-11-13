using System;
using System.Linq;

namespace PG3302_Eksamen
{

    class Player : ThreadProxy

    {
       
        public string Name { get; set; }
        public int Id { get; set; }
        private bool IsQuarantined { get; set; }
        
        private readonly Hand _hand;

        public Player(string name, int id)
        {
            Name = name;
            this.Id = id;
            IsQuarantined = false;

            _hand = new Hand();
        }

        public void TakeCard(Card card)
        {
            _hand.GiveCard(card);
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
                if (!dealer.GetAccess(this)) continue;
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
                    
                _hand.GiveCard(newCard);
                Console.WriteLine(Name + " drew card: " + newCard);

                Console.WriteLine(this); // TODO this is for debugging

                int numOfDiamonds = 0;
                int numOfSpades = 0;
                int numOfClubs = 0;
                int numOfHearts = 0;
                    

                foreach (Card card in _hand.GetHand())
                {
                    // Skip normal cards here to avoid checking more if's - redundant?
                    if (card.GetCardType() == CardType.Normal || card.GetCardType() == CardType.Joker)
                    {
                        continue;
                    }
                        
                    if (card.GetCardType() == CardType.Vulture)
                    {
                        while (true)
                        {
                            Card newVultureCard = dealer.GetCard();
                            if (newVultureCard.GetCardType() == CardType.Normal)
                            {
                                _hand.GiveCard(newVultureCard);
                                Console.WriteLine(Name + " drew card: " + newVultureCard);
                                break;
                            }
                        }
                        _hand.RemoveCard(card); // we gain another card so our hand size is 5. Vulture effect is present by not removing a card, but we dont want to count the suit from it
                        _hand.MaxHandSize++;
                        Console.WriteLine("New max hand size: " + _hand.MaxHandSize);
                        Console.WriteLine(this);
                        break;
                    }

                    if (card.GetCardType() == CardType.Quarantine)
                    {
                        IsQuarantined = true;
                        dealer.ReturnCard(card);
                        _hand.RemoveCard(card);
                        Console.WriteLine(Name + " is now quarantined!");
                        Console.WriteLine(Name + ": Spades: " + numOfSpades + ", Clubs: " + numOfClubs + ", Diamonds: " + numOfDiamonds + ", Hearts: " + numOfHearts); // TODO: temp for debugging
                        Console.WriteLine(Name + " returned card: " + card);
                        Console.WriteLine("");
                        dealer.CloseAccess();
                        return;
                    }

                    if (card.GetCardType() == CardType.Bomb)
                    {
                        Console.WriteLine(Name + " has to throw away all his cards :(");
                        /*foreach (Card c in _hand)
                            {
                                Console.WriteLine(c);
                                dealer.ReturnCard(c);
                                //_hand.Remove(c);
                                Console.WriteLine(Name + " returned card: " + c);
                            }*/
                        for (int i = _hand.Count() - 1; i >= 0; i--)
                        {
                            var c = _hand.GetHand()[i];
                            dealer.ReturnCard(c);
                            _hand.RemoveCard(c);
                            Console.WriteLine(Name + " returned card: " + c);
                        }

                        Console.WriteLine(Name + " draws a new hand...");
                            
                        // by hardcoding this to 4, it means bomb will also blow up the vulture card and NOT return it to the deck
                        for (int i = 0; i < _hand.MaxHandSize; i++)
                        {
                            // New hand can not give special cards
                            dealer.DrawNormalCard(this);
                        }

                        Console.WriteLine(this);
                            
                        break;
                    }
                }
                    
                foreach (Card card in _hand.GetHand())
                {
                    // Ignore special cards when counting
                    if (card.GetCardType() != CardType.Normal)
                    {
                        continue;
                    }

                    switch (card.GetSuit())
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

                foreach (Card card in _hand.GetHand())
                {
                    if (card.GetCardType() == CardType.Joker)
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
                                card.SetSuit(Suit.Diamonds);
                                break;
                            case "spades":
                                numOfSpades++;
                                card.SetSuit(Suit.Spades);
                                break;
                            case "clubs":
                                numOfClubs++;
                                card.SetSuit(Suit.Clubs);
                                break;
                            case "hearts":
                                numOfHearts++;
                                card.SetSuit(Suit.Hearts);
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
                    if (newCard.GetCardType() == CardType.Vulture)
                    {
                        Console.WriteLine("Because card drawn was vulture, we don't throw a card!");
                        Console.WriteLine("");
                        dealer.CloseAccess();
                        return;
                    }
                    int minCount = 100;
                    Suit minSuit = _hand.GetHand()[0].GetSuit(); //??
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
                    while (_hand.GetHand()[i].GetSuit() != minSuit)
                    {
                        i++;
                    }
                        
                    Card returnCard = _hand.GetHand()[i];
                    dealer.ReturnCard(returnCard);
                    _hand.RemoveCard(returnCard);
                    Console.WriteLine(Name + " returned card: " + returnCard);
                    Console.WriteLine("");
                    dealer.CloseAccess();
                }
            }
            Console.WriteLine(Name + " leaving game");
            Stop();
        }
    }
}