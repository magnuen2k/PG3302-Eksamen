﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PG3302_Eksamen
{
    class Game
    {
        private static readonly Object _thisLock = new Object();
        private static int cardsInDeck = 52;
        
        public static void FightForCards()
        {
            for (int i = 0; i < 100; i++)
            {
                TakeCard();
            }
        }

        public static void TakeCard()
        {
            lock (_thisLock)
            {
                if (cardsInDeck <= 0)
                {
                    return;
                    //throw new Exception("No more cards");
                }
                
                Console.WriteLine(Thread.CurrentThread.Name + " is pulling a card!");
                cardsInDeck--;
                Console.WriteLine($"New amount of cards in deck: {cardsInDeck}");
                Console.WriteLine(Thread.CurrentThread.Name + " is sleeping a bit :)");
                Thread.Sleep(1500);
                Console.WriteLine("");
            }
        }
    }
    class Program
    {
 
        
        static void Main(string[] args)
        {
            
            const int minPlayers = 2;
            const int maxPlayers = 4;                     
                                    
            Console.WriteLine("  ,___________________________________");
            Console.WriteLine(" /      _____ _  _       poki <3      \\");
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


            int playerAmount;
            while (!int.TryParse(Console.ReadLine(), out playerAmount) || !(playerAmount >= minPlayers && playerAmount <= maxPlayers))
            {
                Console.WriteLine($"Nope, try again. How many players? ({minPlayers}-{maxPlayers})");
            }
            Console.WriteLine("Good job u know how to get that input slap");
            Console.WriteLine("");

            List<Player> players = new List<Player>();
            for (int i = 0; i < playerAmount; i++)
            {
                players.Add(new Player("player" + (i + 1)));
            }
            
            Thread[] threads = new Thread[playerAmount];
            for (int i = 0; i < players.Count; i++)
            {
                Thread t = new Thread(new ThreadStart(Game.FightForCards));
                //t.Name = "player" + (i + 1);
                t.Name = players[i].Name;
                threads[i] = t;
            }

            for (int i = 0; i < playerAmount; i++)
            {
                threads[i].Start();
            }

            /*Card card = new Card();
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
            Console.WriteLine(hand);*/
        }
    }
}