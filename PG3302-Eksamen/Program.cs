using System;
using PG3302_Eksamen.Game;

namespace PG3302_Eksamen
{
    class Program
    {
        static void Main(string[] args)
        {
            GameMessages.WelcomeMessage();
            
            int playerAmount;
            
            // Ask for player input until acceptable input is given 
            while (!int.TryParse(Console.ReadLine(), out playerAmount) || !(playerAmount >= GameConfig.MinPlayers && playerAmount <= GameConfig.MaxPlayers))
            {
                GameMessages.PlayerLimit();
            }
            GameMessages.SuccessfulInput();

            // Create game and start it with the selected amount of players
            IGame game = new Game.Game(playerAmount);
            game.Run();
        }
    }
}