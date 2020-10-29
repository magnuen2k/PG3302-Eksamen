using System;
using System.Collections.Generic;
using System.Text;

namespace PG3302_Eksamen
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hi, and welcome to this wonderful card game! :3");
            Console.WriteLine("How many players? (2-4)");

            int playerAmount = Convert.ToInt32(Console.ReadLine());
            
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

            Queue<Card> cards = GameBoard.GenerateDeck();
            
            StringBuilder hand = new StringBuilder();

            foreach (Card card1 in cards)
            {
                hand.Append(card1.Value + " of " + card1.Suit);
            }

            
            Console.WriteLine(card.ToString());
            Console.WriteLine(player1.ToString());
            Console.WriteLine(hand);
        }
    }
}