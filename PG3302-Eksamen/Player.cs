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
                if (!dealer.GetAccess(this)) continue;
                if (IsQuarantined)
                {
                    Console.WriteLine(Name + " is quarantined, sitting out this round :(\n");
                    IsQuarantined = false;
                    dealer.CloseAccess();
                    return;
                }
                
                // Draw card
                ICard newCard = dealer.GetCard();
                Hand.GiveCard(newCard); // Move this into HandleCard.Handle?
                Console.WriteLine(Name + " drew card: " + newCard);

                Console.WriteLine(this + " (" + Hand.Count() + " cards in hand)"); // TODO this is for debugging

                HandleCard.Handle(this, newCard);

                // Exit condition if we do not wish to consider the hand
                // TODO but how can we do this from handleVulture/handleQuarantine directly??
                if (Game.ShouldWeContinueTheLoop)
                {
                    Game.ShouldWeContinueTheLoop = false;
                    continue;
                }
                
                HandleHand.Handle(this);
            }
            Console.WriteLine(Name + " leaving game");
            Stop();
        }
    }
}