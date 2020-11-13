namespace PG3302_Eksamen
{
    public class QuarantineCard : CardDecorator
    {
        private const CardType CardType = PG3302_Eksamen.CardType.Quarantine;
        
        public QuarantineCard(ICard originalCard) : base(originalCard)
        {
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