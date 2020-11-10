namespace PG3302_Eksamen
{
    public class Card
    {
        public Suit Suit { get; set; }
        public Value Value { get; set; }

        public override string ToString()
        {
            return Value + " of " + Suit;
        }
    }
}