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

            
            
            /*while (true)
            {
                var c = Console.ReadLine();
                
                if (Int32.TryParse(c, out playerAmount))
                {
                    if (playerAmount >= 2 && playerAmount <= 4)
                    {
                        Console.WriteLine("Good job u know how to get that input slap");
                        break;
                    }
                    Console.WriteLine("2-4 you fool! Try again (2-4)");
                }
                else
                {
                    Console.WriteLine("Nope, try again. How many players? (2-4)");
                }
            }*/
            int playerAmount;

            while (!int.TryParse(Console.ReadLine(), out playerAmount) || !(playerAmount >= 2 && playerAmount <= 4))
            {
                Console.WriteLine("Nope, try again. How many players? (2-4)");
            }
            Console.WriteLine("Good job u know how to get that input slap");

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