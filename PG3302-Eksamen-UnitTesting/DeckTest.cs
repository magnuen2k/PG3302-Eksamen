using System;
using NUnit.Framework;
using PG3302_Eksamen;
using PG3302_Eksamen.Deck;
using PG3302_Eksamen.Player;

namespace PG3302_Eksamen_UnitTesting
{
    public class DeckTest
    {
        /*[Test]
        public void CheckDeckSize()
        {
            Deck deck2 = DeckFactory.CreateDeck();
            Assert.IsTrue(deck2.Size() == 52);
        }*/

        [Test]
        public void CheckDeckSizeAfterDealingInitCards()
        {
            IDeck deck = DeckFactory.CreateDeck();
            Player p = new Player("Test", 2);
            p.AddToHand(deck.GetNextCard());
            p.AddToHand(deck.GetNextCard());
            p.AddToHand(deck.GetNextCard());
            p.AddToHand(deck.GetNextCard());

            Console.WriteLine(deck.Size());
            Assert.IsTrue(deck.Size() == 48);
        }
    }
}