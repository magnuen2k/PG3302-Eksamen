namespace PG3302_Eksamen.Card
{
    public class BombCard : CardDecorator
    {
        private const CardType CardType = PG3302_Eksamen.Card.CardType.Bomb;

        public BombCard(ICard originalCard) : base(originalCard)
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