using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public interface ISquare
    {
        string GetInfo();
    }


    public class Square: ISquare
    {
        public int position;

        public int Position { get => position; set => position = value; }

        public Square(int Position)
        {
            this.position = Position;
        }

        public Square() { }

        public string GetInfo()
        {
            return $"Position: {position}";
        }
    }
}
