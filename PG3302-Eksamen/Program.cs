using System;
using PG3302_Eksamen.Game;
using PG3302_Eksamen.GameHandlers;

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

            IGame game = new Game.Game(playerAmount);
            //HandleCard.BombIdentified += HandleCard.OnBombIdentified;
            game.Run();
        }
    }
}