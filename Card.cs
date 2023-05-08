using Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public interface ICard
    {
        int RandomInt();

        int RandomCash();

        string CardInstruction(int what, int rand_cash, int rand_int);
    }
    
    public enum TypeCard { Chance, CommunityChest, TrainStation, Building, Street }
    public enum PropertySituation { Free, Bought, House, Hotel }

    public class Card: ICard
    {
        public TypeCard type; // type of card
        public int what; // what the card says
        public int position;
        public string name;
        public PropertySituation situation;
        public long buyingCost;
        public long taxes;
        public Player owner;
        public bool isProperty;

        public int Position { get => position; set => position = value; }
        public int What { get => what; set => what = value; }
        public string Name { get => name; set => name = value; }
        public TypeCard Type { get => type; set => type = value; }
        public long BuyingCost { get => buyingCost; set => buyingCost = value; }
        public long Taxes { get => taxes; set => taxes = value; }
        public PropertySituation Situation { get => situation; set => situation = value; }
        public Player Owner { get => owner; set => owner = value; }

        public Card() {  }
        public Card(TypeCard type, int positionm, bool isProperty)
        {
            this.type = type;
            this.what = RandomInt();
            this.position = position;
            this.isProperty = false;
        }
        public Card(string Name, TypeCard Type, long Buying_cost, long Taxes, PropertySituation Situation, Player Owner, int position, bool isProperty)
        {
            this.name = Name;
            this.type = Type;
            this.buyingCost = Buying_cost;
            this.taxes = Taxes;
            this.situation = Situation;
            this.owner = Owner;
            this.position = position;
            this.isProperty = isProperty;
        }

        public int RandomInt()
        {
            Random rnd = new Random();
            int result = rnd.Next(1, 8);
            return result;
        }

        public int RandomCash()
        {
            Random rnd = new Random();
            int result = rnd.Next(1, 1000);
            return result;
        }

        public string CardInstruction(int what, int rand_cash, int rand_int)
        {
            if (what == 1) { return "Get out of jail"; }
            else if (what == 2) { return "Pay $" + rand_cash + " to the player who played before you"; }
            else if (what == 3) { return "Pay $" + rand_cash + " for taxes"; }
            else if (what == 4) { return "Receive $" + rand_cash + " from the bank"; }
            else if (what == 5) { return "Move " + rand_int + " squares forward"; }
            else if (what == 6) { return "Move " + rand_int + " squares backward"; }
            else if (what == 7) { return "Go to jail"; }
            else { return "There was an error please try again"; }
        }
        public string toString()
        {
            return "\tName: " + name + "\n\tType: " + type.ToString() + "\n\tBuying cost: $" + buyingCost + "\n\tTaxes: $" + taxes +
                "\n\tSituation: " + situation.ToString();
        }
    }   
}
