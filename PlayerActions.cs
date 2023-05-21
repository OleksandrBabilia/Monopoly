using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public interface IPlayersActionHandler
    {
        void buy(Player player, int compt);
    }

    public abstract class PlayerActionHandlerTemplate : IPlayersActionHandler
    {

        public void buy(Player player, int compt)
        {
            if (player.properties.Count() == 0)
            {
                Console.WriteLine("You do not have any properties.");
                Console.WriteLine("\nPress any key to go back to the menu.");
            }
            else
            {
                PerformBuyAction(player, compt);
            }
        }
        protected abstract void PerformBuyAction(Player player, int compt);
    }

    public class PlayerBuyHotelActionHandler : PlayerActionHandlerTemplate
    { 
        protected override void PerformBuyAction(Player player, int compt)
        {
            int i = 0;
            Game game = Game.GetInstance();

            Console.WriteLine("For which property do you want to buy a hotel?\n");
            foreach (Property p in player.properties)
            {
                Console.Write(i + 1);
                Console.WriteLine(p.toString() + "\n");
                i++;
            }
            i = int.Parse(Console.ReadLine()) - 1;
            BoughtProperty bp = new BoughtProperty(player.properties[i], player);
            HouseProperty hsp = new HouseProperty(bp, player);
            while (player.properties[i].GetType() != hsp.GetType())
            {
                Console.WriteLine("You cannot buy a hotel for this property because it already has one or it does not have a house yet.");
                Console.WriteLine("1 : Chose another property\n2 : Go back to the menu");
                int r = int.Parse(Console.ReadLine());
                if (r == 2) { game.DisplayMenu(player, compt, false); }
                else if (r == 1)
                {
                    Console.WriteLine("For which property do you want to buy a hotel?\n");
                    foreach (Property p in player.properties)
                    {
                        Console.Write(i + 1);
                        Console.WriteLine(p.toString() + "\n");
                        i++;
                    }
                    i = int.Parse(Console.ReadLine());
                }
            }
            hsp = (HouseProperty)player.properties[i];
            long hotel_price = hsp.buying_cost * 3;
            Console.WriteLine("Buying a hotel for this property costs $" + hotel_price);
            if (player.money < hotel_price)
            {
                Console.WriteLine("\nYou do not have enough money to go through with this purchase.");
                Console.WriteLine("Press any key to go back to the menu.");
                Console.ReadKey(true);
                game.DisplayMenu(player, compt, false);
            }
            else
            {
                Console.WriteLine("\nYou currently have: $" + player.money);
                int res = 0;
                while (res != 1 && res != 2)
                {
                    Console.WriteLine("Are you sure you want to go throught with this purchase?\n1 : YES\n2 : NO");
                    res = int.Parse(Console.ReadLine());
                }
                if (res == 1)
                {
                    Console.Clear();
                    hsp = new HotelProperty(hsp, player);
                    HotelProperty htp = (HotelProperty)hsp;
                    game.board_game.board[player.position] = htp;
                    int j = 0;
                    foreach (Property prop in player.properties)
                    {
                        if (prop.name != htp.name)
                        {
                            j++;
                        }
                    }
                    player.properties[j] = htp;
                    Console.WriteLine("Congratulations on your new hotel!\n");
                    Console.WriteLine(htp.toStringOwner());
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey(true);
                    game.DisplayMenu(player, compt, false);
                }
                else if (res == 2)
                {
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey(true);
                    game.DisplayMenu(player, compt, false);
                }
            }
        }
    }

    public class PlayerBuyHouseActionHandler : PlayerActionHandlerTemplate
    { 
        protected override void PerformBuyAction(Player player, int compt)
        {
            Game game = Game.GetInstance();
            int i = 0;
            Console.WriteLine("For which property do you want to buy a house?\n");
            foreach (Property p in player.properties)
            {
                Console.Write(i + 1);
                Console.WriteLine(p.toString() + "\n");
                i++;
            }
            i = int.Parse(Console.ReadLine()) - 1;
            BoughtProperty bp = new BoughtProperty(player.properties[i], player);
            while (player.properties[i].GetType() != bp.GetType())
            {
                Console.WriteLine("You cannot buy a house for this property because it already has one.");
                Console.WriteLine("1 : Chose another property\n2 : Go back to the menu");
                int r = int.Parse(Console.ReadLine());
                if (r == 2) { game.DisplayMenu(player, compt, false); }
                else if (r == 1)
                {
                    Console.WriteLine("For which property do you want to buy a house?\n");
                    foreach (Property p in player.properties)
                    {
                        Console.Write(i + 1);
                        Console.WriteLine(p.toString() + "\n");
                        i++;
                    }
                    i = int.Parse(Console.ReadLine());
                }
            }
            bp = (BoughtProperty)player.properties[i];
            long house_price = bp.buying_cost * 2;
            Console.WriteLine("Buying a house for this property costs $" + house_price);
            if (player.money < house_price)
            {
                Console.WriteLine("\nYou do not have enough money to go through with this purchase.");
                Console.WriteLine("Press any key to go back to the menu.");
                Console.ReadKey(true);
                game.DisplayMenu(player, compt, false);
            }
            else
            {
                Console.WriteLine("\nYou currently have: $" + player.money);
                int res = 0;
                while (res != 1 && res != 2)
                {
                    Console.WriteLine("Are you sure you want to go throught with this purchase?\n1 : YES\n2 : NO");
                    res = int.Parse(Console.ReadLine());
                }
                if (res == 1)
                {
                    Console.Clear();
                    bp = new HouseProperty(bp, player);
                    HouseProperty hsp = (HouseProperty)bp;
                    game.board_game.board[player.position] = hsp;
                    int j = 0;
                    foreach (Property prop in player.properties)
                    {
                        if (prop.name != hsp.name)
                        {
                            j++;
                        }
                    }
                    player.properties[j] = hsp;
                    Console.WriteLine("Congratulations on your new house!\n");
                    Console.WriteLine(hsp.toStringOwner());
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey(true);
                    game.DisplayMenu(player, compt, false);
                }
                else if (res == 2)
                {
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey(true);
                    game.DisplayMenu(player, compt, false);
                }
            }
        }
    }

    public class PlayerBuyPropertyActionHandler : IPlayersActionHandler
    {

        private Game game = Game.GetInstance();
        public void buy(Player player, int compt)
        {
            Property p = new Property("", TypeProperty.Street, 0, 0, PropertySituation.Free, null, 0);
            BoughtProperty bp = new BoughtProperty(p, null);
            HouseProperty hsp = new HouseProperty(bp, null);
            HotelProperty htp = new HotelProperty(hsp, null);
            if (game.board_game.board[player.position].GetType() == bp.GetType() || game.board_game.board[player.position].GetType() == hsp.GetType() || game.board_game.board[player.position].GetType() == htp.GetType())
            {
                Console.WriteLine("This property is not available.");
                Console.WriteLine("Press any key to go back to the menu.");
                Console.ReadKey(true);
                game.DisplayMenu(player, compt, false);
            }
            else if (game.board_game.board[player.position].GetType() == p.GetType())
            {
                Console.Clear();
                Console.WriteLine("The property you want to buy is the following:\n");
                p = (Property)game.board_game.board[player.position];
                Console.WriteLine(p.toString());
                if (p.buying_cost > player.money)
                {
                    Console.WriteLine("\nYou do not have enough money to go through with this purchase.");
                    Console.WriteLine("Press any key to go back to the menu.");
                    Console.ReadKey(true);
                    game.DisplayMenu(player, compt, true);
                }
                else
                {
                    Console.WriteLine("\nYou currently have: $" + player.money);
                    int res = 0;
                    while (res != 1 && res != 2)
                    {
                        Console.WriteLine("Are you sure you want to go throught with this purchase?\n1 : YES\n2 : NO");
                        res = int.Parse(Console.ReadLine());
                    }
                    if (res == 1)
                    {
                        Console.Clear();
                        p = new BoughtProperty(p, player);
                        BoughtProperty b = (BoughtProperty)p;
                        game.board_game.board[player.position] = b;
                        player.properties.Add(b);
                        player.money -= p.buying_cost;
                        Console.WriteLine("Congratulations on your new property!\n");
                        Console.WriteLine(b.toStringOwner());
                        Console.WriteLine("\nPress any key to go back to the menu.");
                        Console.ReadKey(true);
                        game.DisplayMenu(player, compt, true);
                    }
                    else if (res == 2)
                    {
                        Console.WriteLine("\nPress any key to go back to the menu.");
                        Console.ReadKey(true);
                        game.DisplayMenu(player, compt, true);
                    }
                }
            }
            else
            {
                Console.WriteLine("This square is not a property, you cannot purchase it.");
                Console.WriteLine("\nPress any key to choose another action.\n");
                Console.ReadKey(true);
                game.DisplayMenu(player, compt, true);
            }
        }
    }
}
        
