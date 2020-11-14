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
            Id = id;
            IsQuarantined = false;

            _hand = new Hand();
        }

        public void TakeCard(ICard card)
        {
            _hand.GiveCard(card);
        }
        

        public override string ToString()
        {
            return Name + " has hand: " + _hand;
        }

        private static void HandleCard(Player player, ICard card)
        {
            switch (card.GetCardType())
            {
                case CardType.Bomb:
                    HandleBomb(player);
                    break;
                case CardType.Quarantine:
                    HandleQuarantine(player, card);
                    break;
                case CardType.Vulture:
                    HandleVulture(player, card);
                    break;
                case CardType.Joker:
                    HandleJoker(player, card);
                    break;
                case CardType.Normal:
                    HandleNormalCard(player, card);
                    break;
                default:
                    throw new NotImplementedException("You drew a card with a type that cannot be handled. Code needs review.");
            }
        }

        private static void HandleBomb(Player player)
        {
            Dealer dealer = Dealer.GetDealer();
            
            // TODO extract different operations to own functions (SRP)
            
            Console.WriteLine(player.Name + " has to throw away all his cards :(");
            for (int i = player._hand.Count() - 1; i >= 0; i--)
            {
                ICard c = player._hand.GetHand()[i];
                dealer.ReturnCard(c);
                player._hand.RemoveCard(c);
                Console.WriteLine(player.Name + " returned card: " + c);
            }

            Console.WriteLine(player.Name + " draws a new hand...");
                            
            // draws amount of new cards depending if 
            for (int i = 0; i < player._hand.MaxHandSize; i++)
            {
                // New hand can not give special cards
                dealer.DrawNormalCard(player);
            }
        }

        private static void HandleQuarantine(Player player, ICard card)
        {
            Dealer dealer = Dealer.GetDealer();
            player.IsQuarantined = true;
            dealer.ReturnCard(card);
            player._hand.RemoveCard(card);
            Console.WriteLine(player.Name + " is now quarantined!");
            Console.WriteLine(player.Name + " returned card: " + card +"\n");
            dealer.CloseAccess();
        }

        private static void HandleVulture(Player player, ICard card)
        {
            Console.WriteLine("Laying spider mines...");
            Dealer dealer = Dealer.GetDealer();
            dealer.DrawNormalCard(player);
            player._hand.RemoveCard(card); // we gain another card so our hand size is incremented by 1. Vulture effect is present by not removing a card, but we dont want to count the suit from it
            //dealer.ReturnCard(card); // TODO - allow vulture to go back in deck? but then it needs to be in random spot, or shuffle?
            player._hand.MaxHandSize++;
            Console.WriteLine("New max hand size: " + player._hand.MaxHandSize);
        }

        private static void HandleJoker(Player player, ICard card)
        {
            Console.WriteLine("Handling a Joker card - not yet extracted to Handle");
        }

        private static void HandleNormalCard(Player player, ICard card)
        {
            Console.WriteLine("Handling a normal card - not yet extracted to Handle");
        }


        protected override void Play()
        {
            Dealer dealer = Dealer.GetDealer();
            while (!dealer.GameEnded)
            {
                if (!dealer.GetAccess(this)) continue;
                if (IsQuarantined)
                {
                    Console.WriteLine(Name + " is quarantined, sitting out this round :(\n");
                    IsQuarantined = false;
                    dealer.CloseAccess();
                    return;
                }

                /*if (IsQuarantined)
                    IsQuarantined = false;
                else
                    handleAccess();
                dealer.CloseAccess();*/
                    
                // Draw card
                ICard newCard = dealer.GetCard();
                    
                // TODO handle special cards
                    
                _hand.GiveCard(newCard);
                Console.WriteLine(Name + " drew card: " + newCard);

                Console.WriteLine(this); // TODO this is for debugging

                int numOfDiamonds = 0;
                int numOfSpades = 0;
                int numOfClubs = 0;
                int numOfHearts = 0;

                HandleCard(this, newCard);
                
                /*
                 HandleHand(); // handles which suit has the most
                  
                 */

                foreach (ICard card in _hand.GetHand())
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

                foreach (ICard card in _hand.GetHand())
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
                        Console.WriteLine("Joker handling, Max: " + highestSuit);

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
                        Console.WriteLine("Because card drawn was vulture, we don't throw a card!\n");
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
                        
                    ICard returnCard = _hand.GetHand()[i];
                    dealer.ReturnCard(returnCard);
                    _hand.RemoveCard(returnCard);
                    Console.WriteLine(Name + " returned card: " + returnCard + "\n");
                    dealer.CloseAccess();
                }
            }
            Console.WriteLine(Name + " leaving game");
            Stop();
        }
    }
}