using System;

namespace PG3302_Eksamen
{
    public static class HandleCard
    {
        public static void Handle(Player player, ICard card)
        {
            player.AddToHand(card);
            GameMessages.DebugLog(player + " (" + player.Hand.Count() + " cards in hand)");
            switch (card.GetCardType())
            {
                case CardType.Bomb:
                    Bomb(player);
                    break;
                case CardType.Quarantine:
                    Quarantine(player, card);
                    break;
                case CardType.Vulture:
                    Vulture(player, card);
                    break;
                case CardType.Joker:
                    break;
                case CardType.Normal:
                    break;
                default:
                    throw new NotImplementedException("You drew a card with a type that cannot be handled. Code needs review.");
            }
        }
        
        private static void Bomb(Player player)
        {
            GameMessages.Bomb(player.Name);
            Hand.DrawNewHand(player);
        }

        private static void Quarantine(Player player, ICard card)
        {
            player.IsQuarantined = true;
            RemoveCard(player, card);
            GameMessages.PlayerGotQuarantined(player.Name);
            GameMessages.ReturnCard(player.Name, card);
        }
        
        private static void Vulture(Player player, ICard card)
        {
            Dealer dealer = Dealer.GetDealer();
            player.Hand.MaxHandSize++;
            GameMessages.Vulture(player);
            dealer.DrawNormalCard(player);
            RemoveCard(player, card); // we gain another card so our hand size is incremented by 1. Vulture effect is present by not removing a card, but we dont want to count the suit from it
            GameMessages.ReturnCard(player.Name, card);
            player.DrewVulture = true;
        }
        
        public static void RemoveCard(Player player, ICard card)
        {
            Dealer dealer = Dealer.GetDealer();
            dealer.ReturnCard(card);
            player.Hand.RemoveCard(card);
        }
    }
}