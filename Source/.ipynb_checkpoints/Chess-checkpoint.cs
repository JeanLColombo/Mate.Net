using System;
using System.Collections.Generic;
using System.Text;

namespace Mate
{
    public class Chess
    {
        //TODO: Check if readonly is ok. Can I access and alter its properties?

        public readonly Player White;

        public readonly Player Black;

        public readonly Board Board = new Board();

        public Chess() 
        {
            White = new Player(true, Board);
            Black = new Player(false, Board);
        }
    }
}
