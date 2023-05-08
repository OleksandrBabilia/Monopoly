using Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Player
    {
        public string name; // the name of the player
        public int position = 0; // the player's position on the board
        public long money = 1500; // how much money the player owns
        public bool jail = false; // if true then the player is in jail
        public List<Card> properties = new List<Card>(); // the list of the properties owned by the player
        public bool get_out_of_jail_card = false; // if true then the player has a "Get out of jail" card in reserve
        public bool loser = false;

        public string Name { get => name; set => name = value; }
        public int Position { get => position; set => position = value; }
        public long Money { get => money; set => money = value; }
        public bool Jail { get => jail; set => jail = value; }
        public List<Card> Properties { get => properties; set => properties = value; }
        public bool GetOut { get => get_out_of_jail_card; set => get_out_of_jail_card = value; }
        public bool Loser { get => loser; set => loser = value; }

        public Player(string Name, int Position, long Money, bool Jail, List<Card> Properties, bool GetOut, bool Loser)
        {
            this.name = Name;
            this.position = Position;
            this.money = Money;
            this.jail = Jail;
            this.properties = Properties;
            this.get_out_of_jail_card = GetOut;
            this.loser = Loser;
        }

        public Player(string Name) { this.name = Name; }

        public Player(Player other) 
        {
            this.name = other.name;
            this.position = other.position;
            this.money = other.money;
            this.jail = other.jail;
            this.properties = other.properties;
            this.get_out_of_jail_card = other.get_out_of_jail_card;
            this.loser = other.loser;
        }

        public Player() { }

        public string toString()
        {
            int nb_prop = 0;
            if (properties != null)
            {
                nb_prop = properties.Count();
            }
            Console.WriteLine("Proprerties of : " + name);
            for (int i = 0; i < properties.Count; i++)
            {
                Console.WriteLine(properties[i].toString());
            }
            return "Player: " + name + "\nPosition: " + position + "\nMoney: $" + money + "\nProperties: " + nb_prop;
        }

        public int[] RollDices()
        {
            Random rnd = new Random();
            int value1 = rnd.Next(1, 7);
            int value2 = rnd.Next(1, 7);
            int total = value1 + value2;
            Console.WriteLine("Dice 1 :" + value1);
            Console.WriteLine("Dice 2 :" + value2);
            Console.WriteLine("Total =" + total);
            int[] tab = new int[2];
            tab[0] = value1;
            tab[1] = value2;
            return tab;
        }

        public bool DoubleBool(int[] tab)
        {
            if (tab[0] == tab[1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MoveForward(int number)
        {
            if (position + number < 16)
            {
                position += number;
            }
            else
            {
                position = position + number - 16;
                money += 200;
            }
        }

        public void MoveBackward(int number)
        {
            if (position - number >= 0)
            {
                position -= number;
            }
            else
            {
                position = 16 + (position - number);
            }
        }
    }
}