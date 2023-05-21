using Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public enum TypeProperty { TrainStation, Building, Street }
    public enum PropertySituation { Free, Bought, House, Hotel }

    public class Property : Square
    {
        public string name;
        public TypeProperty type;
        public long buying_cost; 
        public long taxes; 
        public PropertySituation situation; 
        public Player owner; 

        public string Name { get => name; set => name = value; }
        public TypeProperty Type { get => type; set => type = value; }
        public long Buying_cost { get => buying_cost; set => buying_cost = value; }
        public long Taxes { get => taxes; set => taxes = value; }
        public PropertySituation Situation { get => situation; set => situation = value; }
        public Player Owner { get => owner; set => owner = value; }

        public Property(string Name, TypeProperty Type, long Buying_cost, long Taxes, PropertySituation Situation, Player Owner, int position) : base(position)
        {
            this.name = Name;
            this.type = Type;
            this.buying_cost = Buying_cost;
            this.taxes = Taxes;
            this.situation = Situation;
            this.owner = Owner;
            this.position = position;
        }

        public Property() { }

        public string toString()
        {
            return "\tName: " + name + "\n\tType: " + type.ToString() + "\n\tBuying cost: $" + buying_cost + "\n\tTaxes: $" + taxes +
                "\n\tSituation: " + situation.ToString();
        }
    }
}