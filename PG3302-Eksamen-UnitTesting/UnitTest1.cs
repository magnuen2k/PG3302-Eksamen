using System;
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
            Console.WriteLine(deck.Size());
            Assert.IsTrue(deck.Size() == 52);
        }

        [Test]
        public void CheckHandSize()
        {
            Player p = new Player("TestPlayer", 1);
            ExampleHand(p);
            Assert.IsTrue(p.Hand.Count() == 4);
        }
        
        /*[Test]
        public void CheckDeckSizeAfterDealingInitCards()
        {
            Deck deck = DeckFactory.CreateDeck();
            Player p = new Player("TestPlayer", 1);
            ExampleHand(p);
            Console.WriteLine(deck.Size());
            Assert.IsTrue(deck.Size() == 52);
        }*/
        
        [Test]
        public void CheckMaxSuitOnHand()
        {
            Player p = new Player("TestPlayer", 1);
            p.AddToHand(new Card(Suit.Clubs, Value.Eight));
            p.AddToHand(new Card(Suit.Clubs, Value.Ace));
            p.AddToHand(new Card(Suit.Hearts, Value.Nine));
            p.AddToHand(new Card(Suit.Diamonds, Value.Six));
            Assert.AreEqual(Suit.Clubs, p.Hand.BestSuit());
        }
        
        [Test]
        public void CheckMaxSuitCount()
        {
            Player p = new Player("TestPlayer", 1);
            p.AddToHand(new Card(Suit.Clubs, Value.Eight));
            p.AddToHand(new Card(Suit.Clubs, Value.Ace));
            p.AddToHand(new Card(Suit.Hearts, Value.Nine));
            p.AddToHand(new Card(Suit.Diamonds, Value.Six));
            Assert.AreEqual(2, p.Hand.BestSuitCount());
        }
        
        [Test]
        public void CheckCardWithWorstSuit()
        {
            Player p = new Player("TestPlayer", 1);
            p.AddToHand(new Card(Suit.Clubs, Value.Eight));
            p.AddToHand(new Card(Suit.Clubs, Value.Ace));
            p.AddToHand(new Card(Suit.Clubs, Value.Nine));
            p.AddToHand(new Card(Suit.Diamonds, Value.Six));
            Assert.AreEqual(new Card(Suit.Diamonds, Value.Six).GetSuit(), p.Hand.CardOfWorstSuit().GetSuit());
        }
        
        [Test]
        public void CheckHandCountAfterDrawNewHand()
        {
            Player p = new Player("TestPlayer", 1);
            Hand.DrawNewHand(p);
            Assert.IsTrue(p.Hand.MaxHandSize == p.Hand.Count());
        }

        [Test]
        public void CheckBombHandling()
        {
            Player p = new Player("TestPlayer", 1);
            ExampleHand(p);
            ICard card = new Card(Suit.Diamonds, Value.Jack);
            p.AddToHand(new BombCard(card));
            HandleCard.Handle(p, card);
            Assert.IsFalse(p.Hand.GetHand().Contains(card));
        }

        private void ExampleHand(Player p)
        {
            p.AddToHand(new Card(Suit.Clubs, Value.Eight));
            p.AddToHand(new Card(Suit.Clubs, Value.Ace));
            p.AddToHand(new Card(Suit.Clubs, Value.Nine));
            p.AddToHand(new Card(Suit.Diamonds, Value.Six));
        }
    }
}