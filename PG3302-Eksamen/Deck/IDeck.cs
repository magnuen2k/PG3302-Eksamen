using PG3302_Eksamen.Card;

namespace PG3302_Eksamen.Deck
{
    public interface IDeck
    {
        int Size();
        ICard GetNextCard();
        void RestoreCard(ICard card);
        void RestoreCard(ICard card, int position);
    }
}