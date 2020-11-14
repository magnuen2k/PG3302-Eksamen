﻿namespace PG3302_Eksamen
{
    public static class GameConfig
    {
        // These values could be changed if you want to add fewer or more players
        // TODO document that setting minPlayers > maxPlayers will make it impossible to enter the game
        public const int MinPlayers = 2;
        public const int MaxPlayers = 4;
        // Default hand size and win condition. Setting them to the same makes the most sense, but is not required
        // TODO document that setting WinConditionCount 2 higher than DefaultMaxHandSize will make the game run forever unless vulture is put back in the deck
        public const int DefaultMaxHandSize = 4;
        public const int WinConditionCount = 4;
    }
}