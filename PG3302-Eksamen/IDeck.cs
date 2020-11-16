namespace PG3302_Eksamen
{
    public interface IDeck
    {
        int Size();
        ICard GetNextCard();
        void RestoreCard(ICard card);
        void RestoreCard(ICard card, int position);
    }
}