using System;
using System.Collections.Generic;
using System.Threading;

namespace PG3302_Eksamen
{
    public class Game
    {
        private int _players;

        public Game(int players)
        {
            _players = players;
        }

        public void Run()
        {
            // create the dealer
            Dealer dealer = Dealer.GetDealer();
            
            // Create players
            List<Player> players = new List<Player>();
            for (int i = 0; i < _players; i++)
            {
                players.Add(new Player("Player" + (i + 1), i + 1));
            }
            
            // Deal initial hand to players
            for (int i = 0; i < 4; i++)
            {
                foreach (Player player in players)
                {
                    while (true)
                    {
                        Card card = dealer.GetCard();
                        if (card.CardType == CardType.Normal)
                        {
                            player.GiveCard(card);
                            Console.WriteLine(player.Name + " receiving card: " + card);
                            break;
                        }
                    }
                }
            }

            Console.WriteLine("");
            // print hands after cards are dealt for console
            foreach (Player player in players)
            {
                Console.WriteLine(player);
            }

            Console.WriteLine("");


            // Start threads
            for (int i = 0; i < _players; i++)
            {
                players[i].Start();
            }

            dealer.Started = true;
        }
    }
}