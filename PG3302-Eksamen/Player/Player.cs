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

        // Using EventHandler to claim victory
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

        // Plays a turn if player got access to play from dealer
        protected override void Play()
        {
            Dealer.Dealer dealer = Dealer.Dealer.GetDealer();
            
            // Try to play as long as the game is not ended
            while (!dealer.GameEnded)
            {
                if (!dealer.GetAccess(this)) 
                    continue;
                
                // Do not play if quarantined
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
                
                // Remove players access to play after turn
                dealer.CloseAccess();
            }
            
            // Leave the game when game is over
            GameMessages.LeavingGame(Name);
            Stop();
        }

        // Handle players turn
        private void HandleAccessGranted()
        {
            ICard newCard = DrawCard();
            HandleCard.Handle(this, newCard);
            if (HandleHand.Handle(this))
                OnClaimVictory();
        }

        // Draw a card
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
         
        public override string ToString()
        {
            return Hand.ToString();
        }
    }
}