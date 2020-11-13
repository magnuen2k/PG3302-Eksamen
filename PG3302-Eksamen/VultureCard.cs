namespace PG3302_Eksamen
{
    public class VultureCard : CardDecorator
    {
        private const CardType CardType = PG3302_Eksamen.CardType.Vulture;

        public VultureCard(ICard originalCard) : base(originalCard)
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