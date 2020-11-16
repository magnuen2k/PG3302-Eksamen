using PG3302_Eksamen.Card;

namespace PG3302_Eksamen
{
    public static class CardFactory
    {
        public static ICard CreateCard(Suit suit, Value value)
        {
            return new Card.Card(suit, value);
        }
    }
}