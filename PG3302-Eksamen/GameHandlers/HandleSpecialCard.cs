using PG3302_Eksamen.Card;
using PG3302_Eksamen.Game;

namespace PG3302_Eksamen.GameHandlers
{
    public static class HandleSpecialCard
    {
        public static void Bomb(Player.Player player)
        {
            GameMessages.Bomb(player.Name);
            Hand.Hand.DrawNewHand(player);
        }

        public static void Quarantine(Player.Player player, ICard card)
        {
            player.IsQuarantined = true;
            HandleCard.RemoveCard(player, card);
            GameMessages.PlayerGotQuarantined(player.Name);
            GameMessages.ReturnCard(player.Name, card);
        }
        
        public static void Vulture(Player.Player player, ICard card)
        {
            Dealer.Dealer dealer = Dealer.Dealer.GetDealer();
            player.Hand.MaxHandSize++;
            GameMessages.Vulture(player);
            dealer.DrawNormalCard(player);
            HandleCard.RemoveCard(player, card); // we gain another card so our hand size is incremented by 1. Vulture effect is present by not removing a card, but we dont want to count the suit from it
            GameMessages.ReturnCard(player.Name, card);
            player.DrewVulture = true;
        }
    }
}