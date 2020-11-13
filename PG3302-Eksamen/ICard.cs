namespace PG3302_Eksamen
{
    public enum Suit
    {
        Diamonds,
        Hearts,
        Spades,
        Clubs
    }
    
    public enum Value
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    public enum CardType
    {
        Normal,
        Bomb,
        Vulture,
        Quarantine,
        Joker
    }

    public interface ICard
    {
        CardType GetCardType();
        void SetSuit(Suit suit);
        void SetCardType(CardType cardType);
        Suit GetSuit();
        Value GetValue();
    }
}