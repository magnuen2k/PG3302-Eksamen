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
            
            Game game = new Game(playerAmount);
            game.Run();
        }
    }
}