using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Monopoly
{
    public class ProxyPlayer
    {
        private Player _player;

        public Player player { get => _player; set => _player = value; }
        public ProxyPlayer(Player player) { _player = player; }

        public int[] RollDices()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nPlayer " + _player.Name + ":");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nPress any key to roll the dices !\n");
            Console.ReadKey(true);
            int[] dices = _player.RollDices();
            Console.ForegroundColor = ConsoleColor.Green;
            _player.MoveForward(dices[0] + dices[1]);
            Console.WriteLine("\nCurrent position: " + _player.position + "\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey(true);

            return dices;
        }

        public int[] MoveForward()
        {
            Console.WriteLine("\nWow, you got a double, you can roll the dices again !");
            Console.WriteLine("\nPress any key to roll the dices !\n");
            Console.ReadKey(true);
            int[] dices = _player.RollDices();
            _player.MoveForward(dices[0] + dices[1]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nCurrent position :" + _player.position + "\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey(true);

            return dices;
        }
    }
}
