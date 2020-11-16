namespace PG3302_Eksamen.Card
{
    public class JokerCard : CardDecorator
    {
        private const CardType CardType = PG3302_Eksamen.Card.CardType.Joker;
        
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