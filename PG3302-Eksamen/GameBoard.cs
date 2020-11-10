using System;
using System.Collections.Generic;
using System.Text;

namespace PG3302_Eksamen
{
    public class GameBoard
    {
        private static List<Card> _cards;

        private static GameBoard _gameBoard = null;

        public static GameBoard createBoard()
        {
            if (_gameBoard == null)
            {
                _gameBoard = new GameBoard();
                _gameBoard.GenerateDeck();
            }

            return _gameBoard;
        }

        public List<Card> GenerateDeck()
        {
            int count = 0;
            _cards = new List<Card>();
            foreach (Value value in Enum.GetValues(typeof(Value)))
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    count++;
                    _cards.Add(new Card(suit, value));
                }
            }
            Console.WriteLine(count);

            return _cards;
        }


        public override string ToString()
        {
            StringBuilder hand = new StringBuilder();

            foreach (Card card in _cards)
            {
                hand.Append(card.Value + " of " + card.Suit);
            }
            
            return hand.ToString();
        }
    }
}