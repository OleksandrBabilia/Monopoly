using System;

namespace Monopoly
{
    public class Game
    {
        private static Game _instance;
        public List<Player> players = new List<Player>(); // who is playing
        public GameBoard board_game ;
        public int rounds; // number of rounds played
        public Player winner;

        private readonly Dictionary<int, IPlayersActionHandler> _actionHandler;

        public static Game GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Game();
            }
            return _instance;
        }

        public bool IsWinner()
        {
            int compt2 = 0;
            Player pl = new Player();
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].money != 0) { pl = players[i]; compt2++; }
            }
            if (compt2 == 1) { winner = pl; return true; }
            int j = 0;
            foreach (Player p in players)
            {
                if (p.loser == false) { winner = p; j++; }
            }
            if (j == 1) { return true; }
            else { return false; }
        }

        public void Create()
        {
            board_game = GameBoard.GetInstance();
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
                Player temp = prototype.DeepCopy();
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

        public void Start()
        {
            int compt = 0;
            Console.Clear();
            Console.WriteLine("The game is starting!");
            while (!IsWinner())
            {
                Console.Clear();
                rounds++;
                while (players[compt].loser)
                {
                    if (compt == players.Count - 1)
                    {
                        compt = 0;
                    }
                    else
                    {
                        compt++;
                    }
                }
                Player current = players[compt];
                ProxyPlayer proxy = new ProxyPlayer(players[compt]);
                
                int[] dices = proxy.RollDices();
                int nbdouble = 0;

                DisplayMenu(current, compt, true);
                while (current.DoubleBool(dices))
                {
                    nbdouble++;
                    if (nbdouble == 3)
                    {
                        Console.WriteLine("You rolled a double for the third time in a row. You must go to jail.");
                        current.jail = true;
                        current.position = 10;
                        Console.WriteLine("You are now in jail.\n");
                        Console.ReadKey(true);
                        break;
                    }
                    proxy.MoveForward();
                    current = proxy.player;
                    DisplayMenu(current, compt, true);
                }
                if (compt == players.Count - 1)
                {
                    compt = 0;
                }
                else
                {
                    compt++;
                }
            }
            Console.WriteLine("Winner :" + winner.Name);
            Console.ReadKey(true);
        }

        public void DisplayMenu(Player player, int compt, bool pos)
        {
            Dictionary<int, IPlayersActionHandler> _actionHandler;
            _actionHandler = new Dictionary<int, IPlayersActionHandler>
            {
                { 3, new PlayerBuyPropertyActionHandler() },
                { 4, new PlayerBuyHotelActionHandler() },
                { 5, new PlayerBuyHouseActionHandler() }
            };
            Console.Clear();
            if (pos)
            {
                DisplayPosition(player, compt);
            }
            int resp = 0;
            Console.WriteLine("\nPlease Make a Selection :\n");
            Console.WriteLine("0 : Game Status");
            Console.WriteLine("1 : Finish Turn");
            Console.WriteLine("2 : Your DashBoard");
            Console.WriteLine("3 : Purchase the property");
            Console.WriteLine("4 : Buy House for property");
            Console.WriteLine("5 : Buy Hotel for property");
            Console.WriteLine("6 : Declare Bankrupt");
            Console.WriteLine("7 : Quit Game");
            Console.Write("(1-7)>");
            try
            {
                resp = int.Parse(Console.ReadLine());
            }
            catch (FormatException e)
            {
                this.DisplayMenu(player, compt, true);
            }

            if (resp == 0)
            {
                Console.WriteLine("Game Status :");
                for (int i = 0; i < players.Count; i++)
                {
                    Console.WriteLine("\n" + players[i].toString());
                }
                Console.ReadKey();
                Console.Clear();
                DisplayMenu(player, compt, pos);
            }
            else if (resp == 1)
            {
                //skip
            }
            else if (resp == 2)
            {
                Dashboard(player, compt);
            }
            else if (resp == 3 || resp == 4 || resp == 5)
            {
                IPlayersActionHandler action = _actionHandler[resp];
                action.buy(player, compt);
            }
            else if (resp == 6)
            {
                player.loser = true;
            }
            else if (resp == 7)
            {
                player.loser = true;
                player.money = 0;
            }
        }

        public void DisplayPosition(Player player, int compt)
        {
            Property p = new Property("", TypeProperty.Street, 0, 0, PropertySituation.Free, null, 0);
            BoughtProperty bp = new BoughtProperty(p, null);
            HouseProperty hsp = new HouseProperty(bp, null);
            HotelProperty htp = new HotelProperty(hsp, null);
            Card c = new Card(TypeCard.Chance, 0);
            Square s = new Square();
            Console.WriteLine("The square you are currently on is the following:");
            if (board_game.board[player.position].GetType() == p.GetType())
            {
                p = (Property)board_game.board[player.position];
                Console.WriteLine(p.toString());
            }
            else if (board_game.board[player.position].GetType() == bp.GetType())
            {
                bp = (BoughtProperty)board_game.board[player.position];
                Console.WriteLine(bp.toStringOwner());
                if (bp.owner != player)
                {
                    Console.WriteLine("\nYou must pay $" + bp.taxes + " to the owner of this property (" + bp.owner.name + ")");
                    if (player.money < bp.taxes)
                    {
                        Console.WriteLine("You do not have enough money. You lost.");
                        player.loser = true;
                        player.money = 0;
                        Console.WriteLine("\nPress any key to go back to the menu.");
                        Console.ReadKey(true);
                        DisplayMenu(player, compt, false);
                    }
                    else
                    {
                        player.money -= bp.taxes;
                        bp.owner.money += bp.taxes;
                        Console.WriteLine("You now have $" + player.money);
                        Console.WriteLine("The owner (" + bp.owner.name + ") now has $" + bp.owner.money);
                        Console.WriteLine("\nPress any key to go back to the menu.");
                        Console.ReadKey(true);
                        DisplayMenu(player, compt, false);
                    }
                }
            }
            else if (board_game.board[player.position].GetType() == hsp.GetType())
            {
                hsp = (HouseProperty)board_game.board[player.position];
                Console.WriteLine(hsp.toStringOwner());
                if (hsp.owner != player)
                {
                    Console.WriteLine("\nYou must pay $" + hsp.taxes + " to the owner of this property (" + hsp.owner.name + ")");
                    if (player.money < hsp.taxes)
                    {
                        Console.WriteLine("You do not have enough money. You lost.");
                        player.loser = true;
                        player.money = 0;
                        Console.WriteLine("\nPress any key to go back to the menu.");
                        Console.ReadKey(true);
                        DisplayMenu(player, compt, false);
                    }
                    else
                    {
                        player.money -= hsp.taxes;
                        hsp.owner.money += hsp.taxes;
                        Console.WriteLine("You now have $" + player.money);
                        Console.WriteLine("The owner (" + hsp.owner.name + ") now has $" + hsp.owner.money);
                        Console.WriteLine("\nPress any key to go back to the menu.");
                        Console.ReadKey(true);
                        DisplayMenu(player, compt, false);
                    }
                }
            }
            else if (board_game.board[player.position].GetType() == htp.GetType())
            {
                htp = (HotelProperty)board_game.board[player.position];
                Console.WriteLine(htp.toStringOwner());
                if (htp.owner != player)
                {
                    Console.WriteLine("\nYou must pay $" + htp.taxes + " to the owner of this property (" + htp.owner.name + ")");
                    if (player.money < htp.taxes)
                    {
                        Console.WriteLine("You do not have enough money. You lost.");
                        player.loser = true;
                        player.money = 0;
                        Console.WriteLine("\nPress any key to go back to the menu.");
                        Console.ReadKey(true);
                        DisplayMenu(player, compt, false);
                    }
                    else
                    {
                        player.money -= htp.taxes;
                        htp.owner.money += htp.taxes;
                        Console.WriteLine("You now have $" + player.money);
                        Console.WriteLine("The owner (" + htp.owner.name + ") now has $" + htp.owner.money);
                        Console.WriteLine("\nPress any key to go back to the menu.");
                        Console.ReadKey(true);
                        DisplayMenu(player, compt, false);
                    }
                }
            }
            else if (board_game.board[player.position].GetType() == c.GetType())
            {
                c = (Card)board_game.board[player.position];
                Console.WriteLine($"Info :{c.what}/{player.position}");
                Console.WriteLine(c.type.ToString() + " card!");
                CardSquare(c, player, compt);
            }
            else if (board_game.board[player.position].GetType() == s.GetType())
            {
                EmptySquare(player, compt);
            }
        }

        public void Dashboard(Player player, int compt)
        {
            Console.Clear();
            Console.WriteLine("Your position is: " + player.position);
            Console.WriteLine("You have: $" + player.money);
            Console.WriteLine("You own " + player.properties.Count() + " properties:\n");
            if (player.properties.Count() != 0)
            {
                foreach (Property p in player.properties)
                {
                    Console.WriteLine(p.toString());
                }
            }
            Console.WriteLine("\nPress any key to go back to the menu.");
            Console.ReadKey(true);
            DisplayMenu(player, compt, false);
        }

        public void CardSquare(Card c, Player player, int compt)
        {
            Console.Write("The card says:");
            int rand_cash = c.RandomCash();
            int rand_int = c.RandomInt();
            Console.WriteLine("'" + c.CardInstruction(c.what, rand_cash, rand_int) + "'");
            if (c.what == 1)
            {
                if (player.jail)
                {
                    Console.WriteLine("This card allows you to get out of jail. You are now free.");
                    player.jail = false;
                }
                else
                {
                    Console.WriteLine("You are not currently in jail. You can keep this card for later.");
                    player.get_out_of_jail_card = true;
                }
            }
            else if (c.what == 2)
            {
                if (rounds < 2)
                {
                    Console.WriteLine("It is your lucky day: nobody played before you! You do not have to pay anything.");
                }
                else
                {
                    if (player.money < rand_cash)
                    {
                        Console.WriteLine("You do not have enough money to pay. You lost.");
                        player.loser = true;

                    }
                    else
                    {
                        players[compt - 1].money += rand_cash;
                        player.money -= rand_cash;
                        Console.WriteLine("You have given $" + rand_cash + " to " + players[compt].name);
                        Console.WriteLine("You now have $" + player.money);
                    }
                }
            }
            else if (c.what == 3)
            {
                if (player.money < rand_cash)
                {
                    Console.WriteLine("You do not have enough money to pay. You lost.");
                    player.loser = true;

                }
                else
                {
                    player.money -= rand_cash;
                    Console.WriteLine("You have paid $" + rand_cash + " for taxes.");
                    Console.WriteLine("You now have $" + player.money);
                }
            }
            else if (c.what == 4)
            {
                player.money += rand_cash;
                Console.WriteLine("You have received $" + rand_cash + " from the bank.");
                Console.WriteLine("You now have $" + player.money);
            }
            else if (c.what == 5)
            {
                player.MoveForward(rand_int);
                Console.WriteLine("Your current position is now: " + player.position);
            }
            else if (c.what == 6)
            {
                player.MoveBackward(rand_int);
                Console.WriteLine("Your current position is now: " + player.position);
            }
            else if (c.what == 7)
            {
                player.position = 10;
                player.jail = true;
                Console.WriteLine("You are now in jail.");
            }
            Console.WriteLine("\nPress any key to go back to the menu.");
            Console.ReadKey(true);
        }

        public void EmptySquare(Player player, int compt)
        {
            if (player.position == 0)
            {
                Console.WriteLine("\nStart square!");
            }
            else if (player.position == 4)
            {
                Console.WriteLine("Income taxes!\nYou have to pay $200.");
                if (player.money < 200)
                {
                    Console.WriteLine("\nYou do not have enough money. You lost.");
                    player.loser = true;
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey(true);
                    DisplayMenu(player, compt, false);
                }
                else
                {
                    player.money -= 200;
                    Console.WriteLine("You now have $" + player.money);
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey(true);
                    DisplayMenu(player, compt, false);
                }
            }
            else if (player.position == 12)
            {
                Console.WriteLine("\nFree parking!");
            }
            else if (player.position == 8)
            {
                Console.WriteLine("\nGo to jail!");
                player.jail = true;
                player.position = 10;
                Console.WriteLine("You are now in jail.");
                Console.WriteLine("\nPress any key to go back to the menu.");
                Console.ReadKey(true);
                DisplayMenu(player, compt, false);
            }
        }
    }
}


