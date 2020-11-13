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

        public void SetCardType(CardType cardType)
        {
            _originalCard.SetCardType(cardType);
        }
    }
}