using System;

namespace PG3302_Eksamen
{
    public static class GameMessages
    {
        public static void WelcomeMessage()
        {
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
             $"How many players? ({GameConfig.MinPlayers}-{GameConfig.MaxPlayers})"
            );
        }

        public static void PlayerLimit()
        {
            Console.WriteLine($"Nope, try again. How many players? ({GameConfig.MinPlayers}-{GameConfig.MaxPlayers})");
        }

        public static void SuccessfulInput()
        {
            Console.WriteLine("Let's play!\n");
        }

        public static void DrawCard(string name, ICard card)
        {
            Console.WriteLine(name + " drew card: " + card);
        }
        
        public static void ReceiveCard(string name, ICard card)
        {
            Console.WriteLine(name + " receives card: " + card);
        }

        public static void LeavingGame(string name)
        {
            Console.WriteLine(name + ": gg");
        }

        public static void PlayerInQuarantine(string name)
        {
            Console.WriteLine(name + " is quarantined, sitting out this round :(\n");
        }

        public static void PlayerGotQuarantined(string name)
        {
            Console.WriteLine(name + " is now quarantined!");
        }

        public static void ReturnCard(string name, ICard card)
        {
            Console.WriteLine(name + " returned card: " + card +"\n");
        }

        public static void Vulture(Player player)
        {
            Console.WriteLine(player.Name + " new max hand size: " + player.Hand.MaxHandSize);
        }

        public static void Bomb(string name)
        {
            Console.WriteLine(name + " has to throw away all his cards :(");
        }

        public static void WinningMessage(Player player)
        {
            Console.WriteLine(player.Name + " won the game with hand: " + player);
        }

        public static void DrawNewHand(string name)
        {
            Console.WriteLine(name + " draws a new hand...");
        }

        public static void GameEnded()
        {
            Console.Write("\n\nPress any key to continue...");
            Console.ReadKey(true);
        }

        public static void DebugLog(string log)
        {
            if (GameConfig.Debug)
                Console.WriteLine(log);
        }

        public static void Space()
        {
            Console.WriteLine("");
        }
    }
}