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
            
            Console.WriteLine(card.ToString());
        }
    }
}