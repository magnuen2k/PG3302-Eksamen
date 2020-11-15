using System;

namespace PG3302_Eksamen
{
    public static class HandleSpecialCard
    {
        public static void Bomb(Player player)
        {
            Console.WriteLine(player.Name + " has to throw away all his cards :(");
            Hand.ReturnFullHand(player);
            Hand.DrawNewHand(player);
        }

        public static void Quarantine(Player player, ICard card)
        {
            Dealer dealer = Dealer.GetDealer();
            player.IsQuarantined = true;
            dealer.ReturnSpecialCard(card);
            player.Hand.RemoveCard(card);
            Console.WriteLine(player.Name + " is now quarantined!");
            Console.WriteLine(player.Name + " returned card: " + card +"\n");
            Game.ShouldWeContinueTheLoop = true;
            Console.WriteLine("Deck: " + dealer.GetDeck()); // TODO temp debugging
            dealer.CloseAccess();
        }

        public static void Vulture(Player player, ICard card)
        {
            Dealer dealer = Dealer.GetDealer();
            Console.WriteLine("Laying spider mines...");
            dealer.DrawNormalCard(player);
            dealer.ReturnSpecialCard(card);
            player.Hand.RemoveCard(card); // we gain another card so our hand size is incremented by 1. Vulture effect is present by not removing a card, but we dont want to count the suit from it
            //dealer.ReturnCard(card); // TODO - allow vulture to go back in deck? but then it needs to be in random spot, or shuffle?
            player.Hand.MaxHandSize++;
            Console.WriteLine("New max hand size: " + player.Hand.MaxHandSize + "\n");
            Console.WriteLine("Deck: " + dealer.GetDeck()); // TODO temp debugging
            Game.ShouldWeContinueTheLoop = true;
            dealer.CloseAccess(); 
        }
    }
}