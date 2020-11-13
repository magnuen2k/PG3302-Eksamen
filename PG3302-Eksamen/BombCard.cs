namespace PG3302_Eksamen
{
    public class BombCard : CardDecorator
    {
        private const CardType CardType = PG3302_Eksamen.CardType.Bomb;

        public BombCard(ICard originalCard) : base(originalCard)
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