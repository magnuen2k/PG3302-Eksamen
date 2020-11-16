﻿using System;
using PG3302_Eksamen.Card;
using PG3302_Eksamen.Game;

namespace PG3302_Eksamen.GameHandlers
{
    public static class HandleHand
    {
        public static bool Handle(Player.Player player)
        {
            int bestSuitCount = player.Hand.BestSuitCount();
            
            if (player.Hand.HasJoker)
                bestSuitCount++;
            
            GameMessages.DebugLog(player.Name + ": Spades: " + player.Hand.NumOfSpades + ", Clubs: " + player.Hand.NumOfClubs + ", Diamonds: " + player.Hand.NumOfDiamonds + ", Hearts: " + player.Hand.NumOfHearts); // TODO: temp for debugging
            GameMessages.DebugLog(player.Hand.BestSuit() + " - " + bestSuitCount + " (Including joker if u have)");
            
            // Win condition
            if (bestSuitCount >= GameConfig.WinConditionCount)
                return true;
            if (!player.IsQuarantined && !player.DrewVulture)
                ReturnCard(player);
            
            return false;
        }

        private static void ReturnCard(Player.Player player)
        {
            ICard returnCard = player.Hand.CardOfWorstSuit();
            HandleCard.RemoveCard(player, returnCard);
            GameMessages.ReturnCard(player.Name, returnCard);
        }
    }
}