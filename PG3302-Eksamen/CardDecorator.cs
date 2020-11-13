namespace PG3302_Eksamen
{
    public abstract class CardDecorator : ICard
    {
        private readonly ICard _originalCard;

        protected CardDecorator(ICard originalCard)
        {
            _originalCard = originalCard;
        }

        public virtual CardType GetCardType()
        {
            return _originalCard.GetCardType();
        }

        public void SetSuit(Suit suit)
        {
            _originalCard.SetSuit(suit);
        }

        public Suit GetSuit()
        {
            return _originalCard.GetSuit();
        }

        public Value GetValue()
        {
            return _originalCard.GetValue();
        }
    }
}