using System;

namespace PG3302_Eksamen
{
    public static class HandleCard
    {
        public static void Handle(Player player, ICard card)
        {
            // TODO why does the line above not work?
            //player.Hand.GiveCard(card);
            switch (card.GetCardType())
            {
                case CardType.Bomb:
                    HandleSpecialCard.Bomb(player);
                    break;
                case CardType.Quarantine:
                    HandleSpecialCard.Quarantine(player, card);
                    break;
                case CardType.Vulture:
                    HandleSpecialCard.Vulture(player, card);
                    break;
                case CardType.Joker:
                    break;
                case CardType.Normal:
                    break;
                default:
                    throw new NotImplementedException("You drew a card with a type that cannot be handled. Code needs review.");
            }
        }
    }
}