using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public interface ICardFactory
    {
        ICard CreateCard(int postionm, bool isProperty);
    }

    public class CardFactory: ICardFactory
    {
        public TypeCard type;

        public CardFactory(TypeCard type)
        {
            this.type = type;
        }

        public ICard CreateCard(int postion, bool isProperty)
        {
            return new Card(type, postion, isProperty);
        }
    }
}

