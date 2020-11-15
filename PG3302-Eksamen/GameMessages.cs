using System;

namespace PG3302_Eksamen
{
    public static class GameMessages
    {
        private static bool _showDebug = true;
        
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
            Console.WriteLine("Good job u know how to get that input slap\n");
        }

        public static void DrawCard(string name, ICard card)
        {
            Console.WriteLine(name + " drew card: " + card);
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

        public static void Vulture(int maxHandSize)
        {
            Console.WriteLine("New max hand size: " + maxHandSize);
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

        public static void DebugLog(string log)
        {
            if (_showDebug)
                Console.WriteLine(log);
        }
    }
}