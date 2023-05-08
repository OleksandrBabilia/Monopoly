using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public interface ISquareFactory
    {
        ISquare CreateSquare(int postion);
    }

    public class SquareFactory: ISquareFactory
    {
        public TypeCard type;

        public SquareFactory(TypeCard type)
        {
            this.type = type;
        }

        public ISquare CreateSquare(int postion)
        {
            return new Card(type, postion);
        }
    }
}

