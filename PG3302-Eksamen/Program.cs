using System;

namespace PG3302_Eksamen
{
    class Program
    {
        static void Main(string[] args)
        {
            Card card = new Card();
            card.Suit = Suit.Clubs;
            card.Value = Value.King;
            
            Card card2 = new Card();
            card2.Suit = Suit.Diamonds;
            card2.Value = Value.Queen;
            
            Player player1 = new Player();
            player1.Name = "Yionk";
            player1.setHand(card);
            player1.setHand(card2);
            
            Console.WriteLine(card.ToString());
            Console.WriteLine(player1.ToString());
        }
    }
}