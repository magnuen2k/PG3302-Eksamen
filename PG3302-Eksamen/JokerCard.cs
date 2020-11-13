namespace PG3302_Eksamen
{
    public class JokerCard : CardDecorator
    {
        public JokerCard(ICard originalCard) : base(originalCard)
        {
        }

        public override CardType GetCardType()
        {
            SetCardType(CardType.Joker);
            return CardType.Joker;
        }

        public override string ToString()
        {
            return "The " + GetCardType();
        }
    }
}