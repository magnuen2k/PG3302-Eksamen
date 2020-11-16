namespace PG3302_Eksamen.Card
{
    public class VultureCard : CardDecorator
    {
        private const CardType CardType = PG3302_Eksamen.Card.CardType.Vulture;

        public VultureCard(ICard originalCard) : base(originalCard)
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