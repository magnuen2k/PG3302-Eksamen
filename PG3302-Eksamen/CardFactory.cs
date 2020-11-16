using PG3302_Eksamen.Card;

namespace PG3302_Eksamen
{
    public static class CardFactory
    {
        // Create a new card
        public static ICard CreateCard(Suit suit, Value value)
        {
            return new Card.Card(suit, value);
        }
    }
}