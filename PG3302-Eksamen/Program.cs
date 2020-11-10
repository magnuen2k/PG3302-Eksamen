﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PG3302_Eksamen
{
    class Program
    {
        static void Main(string[] args)
        {
            
            const int minPlayers = 2;
            const int maxPlayers = 4;                     
                                    
            Console.WriteLine("  ,___________________________________");
            Console.WriteLine(" /      _____ _  _                    \\");
            Console.WriteLine("||     / ____| || |                   ||");
            Console.WriteLine("||    | |    | || |_                  ||");
            Console.WriteLine("||    | |    |__   _|                 ||");
            Console.WriteLine("||    | |____   | |      The game!    ||");
            Console.WriteLine("||     \\_____|  |_|                   ||");
            Console.WriteLine(" \\   ______________________________   /");
            Console.WriteLine("  -=|          by Team RP          |=-");
            Console.WriteLine("    |______________________________|");
            Console.WriteLine("");
            
            Console.WriteLine("Hi, and welcome to this wonderful card game! :3");
            Console.WriteLine($"How many players? ({minPlayers}-{maxPlayers})");



            while (!int.TryParse(Console.ReadLine(), out var playerAmount) || !(playerAmount >= minPlayers && playerAmount <= maxPlayers))
            {
                Console.WriteLine($"Nope, try again. How many players? ({minPlayers}-{maxPlayers})");
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