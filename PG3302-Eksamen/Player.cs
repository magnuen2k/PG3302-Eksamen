using System;

namespace PG3302_Eksamen
{
    public class Player : ThreadProxy
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public bool IsQuarantined { get; set; }

        public readonly Hand Hand;

        public Player(string name, int id)
        {
            Name = name;
            Id = id;
            IsQuarantined = false;

            Hand = new Hand();
        }

        public void AddToHand(ICard card)
        {
            Hand.GiveCard(card);
        }
        
        public override string ToString()
        {
            return Name + " has hand: " + Hand;
        }
        
        protected override void Play()
        {
            Dealer dealer = Dealer.GetDealer();
            while (!dealer.GameEnded)
            {
                if (!dealer.GetAccess(this)) 
                    continue;
                
                if (IsQuarantined)
                {
                    GameMessages.PlayerInQuarantine(Name);
                    IsQuarantined = false;
                    dealer.CloseAccess();
                    return;
                }
                
                HandleAccessGranted();
                dealer.CloseAccess();
            }
            GameMessages.LeavingGame(Name);
            Stop();
        }

        private void HandleAccessGranted()
        {
            // Draw card
            ICard newCard = DrawCard();
            HandleCard.Handle(this, newCard);

            // Exit condition if we do not wish to consider the hand
            // TODO but how can we do this from handleVulture/handleQuarantine directly??
            if (Game.ShouldWeContinueTheLoop)
            {
                Game.ShouldWeContinueTheLoop = false;
                return;
            }

            HandleHand.Handle(this);
        }

        private ICard DrawCard()
        {
            Dealer dealer = Dealer.GetDealer();
            ICard newCard = dealer.GetCard();
            GameMessages.DrawCard(Name, newCard);
            return newCard;
        }
    }
}