namespace PG3302_Eksamen.Game
{
    public static class GameConfig
    {
        // These values could be changed if you want to add fewer or more players
        // MinPlayer has to be less than MaxPlayers
        public const int MinPlayers = 2;
        public const int MaxPlayers = 4;
        
        // Default hand size and win condition. Setting them to the same makes the most sense, but is not required
        // No values higher than 8 recommended, personally we recommend 6 for both values
        public const int DefaultMaxHandSize = 4;
        public const int WinConditionCount = 4;

        // Slow up/down the game. This is only for console output speed
        public const int GameSpeed = 500;
        
        // Run the game in debug mode
        public const bool Debug = false;
    }
}