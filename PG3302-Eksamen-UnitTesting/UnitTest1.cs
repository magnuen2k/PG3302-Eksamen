using NUnit.Framework;
using PG3302_Eksamen;

namespace PG3302_Eksamen_UnitTesting
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckDeckSize()
        {
            Deck deck = DeckFactory.CreateDeck();
            Assert.IsTrue(deck.Size() == 52);
        }

        public void CheckDeckSizeAfterDealingInitCards()
        {
            Deck deck = DeckFactory.CreateDeck();
        }
    }
}