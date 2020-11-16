using System.Collections.Generic;

namespace PG3302_Eksamen
{
    public interface IHand
    {
        int MaxHandSize { get; set; }
        int NumOfDiamonds { get; }
        int NumOfSpades { get; }
        int NumOfHearts { get; }
        int NumOfClubs { get; }
        bool HasJoker { get; }
        void CountSuit(ICard card, bool addCard);
        Suit BestSuit();
        int BestSuitCount();
        ICard CardOfWorstSuit();
        void GiveCard(ICard card);
        void RemoveCard(ICard card);
        int Count();
        List<ICard> GetHand();
    }
}