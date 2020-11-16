namespace PG3302_Eksamen
{
    public static class PlayerFactory
    {
        public static Player.Player CreatePlayer(string name, int id)
        {
            return new Player.Player(name, id);
        }
    }
}