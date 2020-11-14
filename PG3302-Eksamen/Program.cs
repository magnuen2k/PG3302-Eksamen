using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PG3302_Eksamen
{
    class Program
    {
        static void Main(string[] args)
        {
            //These values could be changed if you want to add fewer or more players
            const int minPlayers = 2;
            const int maxPlayers = 4;                     
                                    
            Console.WriteLine
            ("  ,___________________________________\n" +
             " /      _____ _  _       poki <3      \\\n" +
             "||     / ____| || |                   ||\n" +
             "||    | |    | || |_                  ||\n" +
             "||    | |    |__   _|                 ||\n" +
             "||    | |____   | |      The game!    ||\n" +
             "||     \\_____|  |_|                   ||\n" +
             " \\   ______________________________  //\n" +
             "  -=|          by Team RP          |=-\n" +
             "    |______________________________|\n" +
             "Hi, and welcome to this wonderful card game! :3\n" +
             $"How many players? ({minPlayers}-{maxPlayers})"
            );
            
            int playerAmount;
            while (!int.TryParse(Console.ReadLine(), out playerAmount) || !(playerAmount >= minPlayers && playerAmount <= maxPlayers))
            {
                Console.WriteLine($"Nope, try again. How many players? ({minPlayers}-{maxPlayers})");
            }
            Console.WriteLine("Good job u know how to get that input slap\n");

            Game game = new Game(playerAmount);
            game.Run();
        }
    }
}