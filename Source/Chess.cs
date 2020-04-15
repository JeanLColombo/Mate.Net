using System;
using System.Collections.Generic;
using System.Text;

namespace Mate
{
    public class Chess
    {
        //TODO: Check if readonly is ok. Can I access and alter its properties?

        public readonly Player WhitePieces = new Player(true);

        public readonly Player BlackPieces = new Player(false);

        public readonly Board Board = new Board();
    }
}
