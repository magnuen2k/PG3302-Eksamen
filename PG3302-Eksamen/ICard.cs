namespace PG3302_Eksamen
{
    public interface ICard
    {
        CardType GetCardType();
        void SetCardType(CardType cardType);
        void SetSuit(Suit suit);
        Suit GetSuit();
        Value GetValue();
    }
}