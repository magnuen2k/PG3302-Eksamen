namespace PG3302_Eksamen
{
    public interface ICard
    {
        Suit GetSuit();
        Value GetValue();
        CardType GetCardType();
    }
}