namespace PG3302_Eksamen
{
    public class Card : ICard
    {
        private Suit _suit;
        private readonly Value _value;
        private readonly CardType _cardType;

        public Card(Suit suit, Value value)
        {
            this._suit = suit;
            this._value = value;
            _cardType = CardType.Normal;
        }

        public void SetSuit(Suit suit)
        {
            _suit = suit;
        }

        public Suit GetSuit()
        {
            return _suit;
        }

        public Value GetValue()
        {
            return _value;
        }

        public CardType GetCardType()
        {
            return _cardType;
        }
        
        public override string ToString()
        {
            if (_cardType != CardType.Normal)
            {
                return "The " + _cardType;
            }
            return _value + " of " + _suit + " with type " + _cardType;
        }
    }
}