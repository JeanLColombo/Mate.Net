using System;
using System.Collections.Generic;
using System.Text;
using Mate.Abstractions;

namespace Mate
{
    public class Chess
    {
        public readonly Player White;

        public readonly Player Black;

        public readonly Board Board = new Board();

        internal History History { get; set; } = new History();

        public Chess() 
        {
            White = new Player(true, Board);
            Black = new Player(false, Board);
        }

    }
}
