using PG3302_Eksamen.Hand;

namespace PG3302_Eksamen
{
    public static class HandFactory
    {
        // Create a new hand
        public static IHand CreateHand()
        {
            return new Hand.Hand();
        }
    }
}