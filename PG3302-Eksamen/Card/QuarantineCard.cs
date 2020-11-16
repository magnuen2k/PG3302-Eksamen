namespace PG3302_Eksamen.Card
{
    public class QuarantineCard : CardDecorator
    {
        private const CardType CardType = PG3302_Eksamen.Card.CardType.Quarantine;
        
        public QuarantineCard(ICard originalCard) : base(originalCard)
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