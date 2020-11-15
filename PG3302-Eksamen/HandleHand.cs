using System;
using System.Linq;

namespace PG3302_Eksamen
{
    public static class HandleHand
    {
        public static void Handle(Player player)
        {
 
            // Where is the best place to declare this to make them useable everywhere?
            int numOfDiamonds = 0;
            int numOfSpades = 0;
            int numOfClubs = 0;
            int numOfHearts = 0;
            
            foreach (ICard card in player.Hand.GetHand())
            {
                // Ignore special cards when counting
                // In a scenario where special card would count for max suit count, this would need adjustment
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
                    default:
                        throw new NotImplementedException("You tried to handle a card with a suit that's not considered. Code needs review.");
                }
            }
            
            foreach (ICard card in player.Hand.GetHand())
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
            
            Console.WriteLine(player.Name + ": Spades: " + numOfSpades + ", Clubs: " + numOfClubs + ", Diamonds: " + numOfDiamonds + ", Hearts: " + numOfHearts); // TODO: temp for debugging
            
            Dealer dealer = Dealer.GetDealer();
            
            // Win condition
            if (numOfDiamonds >= GameConfig.WinConditionCount || numOfClubs >= GameConfig.WinConditionCount || numOfHearts >= GameConfig.WinConditionCount || numOfSpades >= GameConfig.WinConditionCount)
            {
                Console.WriteLine(player.Name + " won the game with hand: " + player);
                dealer.GameEnded = true;
            }
            else
            {
                int minCount = 100;
                Suit minSuit = player.Hand.GetHand()[0].GetSuit(); //??
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
                while (player.Hand.GetHand()[i].GetSuit() != minSuit)
                {
                    i++;
                }
                        
                ICard returnCard = player.Hand.GetHand()[i];
                dealer.ReturnCard(returnCard);
                player.Hand.RemoveCard(returnCard);
                Console.WriteLine(player.Name + " returned card: " + returnCard + "\n");
                dealer.CloseAccess();
            }
        }
    }
}