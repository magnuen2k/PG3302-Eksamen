using System;
using System.Linq;

namespace PG3302_Eksamen
{
    public static class HandleHand
    {

        
        
        public static void Handle(Player player)
        {
            Dealer dealer = Dealer.GetDealer();
            int bestSuitCount = player.Hand.BestSuitCount();
            
            if (player.Hand.HasJoker)
                bestSuitCount++;
            
            
            Console.WriteLine(player.Name + ": Spades: " + player.Hand.NumOfSpades + ", Clubs: " + player.Hand.NumOfClubs + ", Diamonds: " + player.Hand.NumOfDiamonds + ", Hearts: " + player.Hand.NumOfHearts); // TODO: temp for debugging
            Console.WriteLine(player.Hand.BestSuit() + " - " + bestSuitCount + " (Including joker if u have)");
            
            // Win condition
            if (bestSuitCount >= GameConfig.WinConditionCount)
            {
                GameMessages.WinningMessage(player);
                dealer.GameEnded = true;
            }
            else
            {
                ICard returnCard = player.Hand.CardOfWorstSuit();
                HandleCard.RemoveCard(player, returnCard);
                GameMessages.ReturnCard(player.Name, returnCard);
                dealer.CloseAccess();
            }
        }
    }
}