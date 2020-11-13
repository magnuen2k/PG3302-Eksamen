namespace PG3302_Eksamen
{
    public interface ICard
    {
        CardType GetCardType();
        void SetSuit(Suit suit);
        Suit GetSuit();
        Value GetValue();
    }
}