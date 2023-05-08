using System;

namespace Monopoly
{
    public class Game
    {
        public List<Player> players = new List<Player>(); // who is playing
        public GameBoard board_game = new GameBoard();
        public int rounds; // number of rounds played
        public Player winner;
        public Game()
        { }

        public void Create()
        {
            Console.WriteLine("Welcome!");
            int amountOfPlayers = 0, level = 0;
           
            Console.WriteLine("How many players (2-6)?");
            amountOfPlayers = int.Parse(Console.ReadLine());

            Console.WriteLine("What level (1-3)?");
            level = int.Parse(Console.ReadLine());
            Player prototype = new Player();
            switch (level)
            {
                case 1:
                    prototype.name = "prototype";
                    prototype.money = 4000;
                    prototype.get_out_of_jail_card = true;
                    break;
                case 2:
                    prototype.name = "prototype";
                    prototype.money = 3000;
                    prototype.get_out_of_jail_card = true;
                    break;
                case 3:
                    prototype.name = "prototype";
                    prototype.money = 2000;
                    prototype.get_out_of_jail_card = false;
                    break;
                default:
                    Console.WriteLine("Wrong input");
                    break;
            }

                
            for (int i = 0; i < amountOfPlayers; i++)
            {
                Console.WriteLine("Player " + (i + 1) + ":");
                Console.Write("Username: ");
                string name = Console.ReadLine();
                Player temp = new Player(prototype);
                temp.name = name;
                players.Add(temp);
                Console.WriteLine("\nThe player was successfully added!\n");
            }
            Console.WriteLine("\nPlayers:");
            for (int i = 0; i < amountOfPlayers; i++)
            {
                Console.WriteLine("\n" + players[i].toString());
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nPress any key to start the game !\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey(true);
        }
    }
}


