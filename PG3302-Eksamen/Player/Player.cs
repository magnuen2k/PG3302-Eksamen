using System;
using PG3302_Eksamen.Card;
using PG3302_Eksamen.Game;
using PG3302_Eksamen.GameHandlers;
using PG3302_Eksamen.Hand;

namespace PG3302_Eksamen.Player
{
    public class Player : ThreadProxy
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public bool IsQuarantined { get; set; }
        public bool DrewVulture { get; set; }

        public event EventHandler ClaimVictory;

        public readonly IHand Hand;

        public Player(string name, int id)
        {
            Name = name;
            Id = id;
            IsQuarantined = false;

            Hand = HandFactory.CreateHand();
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
            Dealer.Dealer dealer = Dealer.Dealer.GetDealer();
            while (!dealer.GameEnded)
            {
                if (!dealer.GetAccess(this)) 
                    continue;
                
                if (IsQuarantined)
                {
                    GameMessages.PlayerInQuarantine(Name);
                    IsQuarantined = false;
                }
                else
                {
                    DrewVulture = false;
                    HandleAccessGranted();
                }
                
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
            if (HandleHand.Handle(this))
                OnClaimVictory();
        }

        private ICard DrawCard()
        {
            Dealer.Dealer dealer = Dealer.Dealer.GetDealer();
            ICard newCard = dealer.GetCard();
            GameMessages.DrawCard(Name, newCard);
            return newCard;
        }

        protected virtual void OnClaimVictory()
        {
            ClaimVictory?.Invoke(this, EventArgs.Empty);
        }
    }
}