namespace PG3302_Eksamen
{
    public static class HandFactory
    {
        public static IHand CreateHand()
        {
            return new Hand();
        }
    }
}