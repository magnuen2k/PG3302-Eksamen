using System;

namespace PG3302_Eksamen
{
    public static class HandleSpecialCard
    {
        public static void Bomb(Player player)
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

        public static void Quarantine(Player player, ICard card)
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

        public static void Vulture(Player player, ICard card)
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
    }
}