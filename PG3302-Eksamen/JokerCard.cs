namespace PG3302_Eksamen
{
    public class JokerCard : CardDecorator
    {
        private const CardType CardType = PG3302_Eksamen.CardType.Joker;
        
        public JokerCard(ICard originalCard) : base(originalCard)
        {
            SetCardType(CardType);
        }

        public override CardType GetCardType()
        {
            return CardType;
        }

        public override string ToString()
        {
            return "The " + CardType;
        }
    }
}