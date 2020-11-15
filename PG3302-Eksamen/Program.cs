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
            GameMessages.WelcomeMessage();
            
            int playerAmount;
            while (!int.TryParse(Console.ReadLine(), out playerAmount) || !(playerAmount >= GameConfig.MinPlayers && playerAmount <= GameConfig.MaxPlayers))
            {
                GameMessages.PlayerLimit();
            }
            GameMessages.SuccessfulInput();

            Game game = new Game(playerAmount);
            game.Run();
        }
    }
}