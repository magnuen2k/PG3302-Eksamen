using System;

namespace PG3302_Eksamen
{
    public static class HandleCard
    {
        public static void Handle(Player player, ICard card)
        {
            player.AddToHand(card);
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
                    break;
                case CardType.Normal:
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
            for (int i = player.Hand.Count() - 1; i >= 0; i--)
            {
                ICard c = player.Hand.GetHand()[i];
                dealer.ReturnCard(c);
                player.Hand.RemoveCard(c);
                Console.WriteLine(player.Name + " returned card: " + c);
            }

            Console.WriteLine(player.Name + " draws a new hand...");
                            
            // draws amount of new cards depending if 
            for (int i = 0; i < player.Hand.MaxHandSize; i++)
            {
                dealer.DrawNormalCard(player);
            }
        }

        private static void HandleQuarantine(Player player, ICard card)
        {
            Dealer dealer = Dealer.GetDealer();
            player.IsQuarantined = true;
            dealer.ReturnCard(card);
            player.Hand.RemoveCard(card);
            Console.WriteLine(player.Name + " is now quarantined!");
            Console.WriteLine(player.Name + " returned card: " + card +"\n");
            Game.ShouldWeContinueTheLoop = true;
            dealer.CloseAccess();
        }

        private static void HandleVulture(Player player, ICard card)
        {
            Dealer dealer = Dealer.GetDealer();
            Console.WriteLine("Laying spider mines...");
            dealer.DrawNormalCard(player);
            player.Hand.RemoveCard(card); // we gain another card so our hand size is incremented by 1. Vulture effect is present by not removing a card, but we dont want to count the suit from it
            //dealer.ReturnCard(card); // TODO - allow vulture to go back in deck? but then it needs to be in random spot, or shuffle?
            player.Hand.MaxHandSize++;
            Console.WriteLine("New max hand size: " + player.Hand.MaxHandSize + "\n");
            Game.ShouldWeContinueTheLoop = true;
            dealer.CloseAccess(); 
        }

        private static void HandleJoker(Player player, ICard card)
        {
            Console.WriteLine("Handling a Joker card - not yet extracted to Handle");
        }

        private static void HandleNormalCard(Player player, ICard card)
        {
            Console.WriteLine("Handling a normal card - not yet extracted to Handle");
        }
    }
}