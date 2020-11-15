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
            
            GameMessages.DebugLog(player.Name + ": Spades: " + player.Hand.NumOfSpades + ", Clubs: " + player.Hand.NumOfClubs + ", Diamonds: " + player.Hand.NumOfDiamonds + ", Hearts: " + player.Hand.NumOfHearts); // TODO: temp for debugging
            GameMessages.DebugLog(player.Hand.BestSuit() + " - " + bestSuitCount + " (Including joker if u have)");
            
            // Win condition
            if (bestSuitCount >= GameConfig.WinConditionCount)
                dealer.ClaimVictory(player);
            else if (!player.IsQuarantined && !player.DrewVulture)
                ReturnCard(player);
        }

        private static void ReturnCard(Player player)
        {
            ICard returnCard = player.Hand.CardOfWorstSuit();
            HandleCard.RemoveCard(player, returnCard);
            GameMessages.ReturnCard(player.Name, returnCard);
        }
    }
}