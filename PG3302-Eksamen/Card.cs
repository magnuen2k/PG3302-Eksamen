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
        Bombe,
        Gribb,
        Karantene,
        Joker
    }
    
    public class Card
    {
        public Suit Suit { get; set; }
        public Value Value { get; set; }
        public CardType CardType { get; set; }

        public Card(Suit suit, Value value)
        {
            this.Suit = suit;
            this.Value = value;
            CardType = CardType.Normal;
        }
        
        public Card(Suit suit, Value value, CardType cardType) : this(suit, value)
        {
            this.CardType = cardType;
        }

        public override string ToString()
        {
            return Value + " of " + Suit;
        }
    }
}