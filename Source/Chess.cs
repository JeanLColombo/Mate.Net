using System;
using System.Collections.Generic;
using System.Text;

namespace Mate
{
    public class Chess
    {
        //TODO: Check if readonly is ok. Can I access and alter its properties?

        public readonly Player WhitePieces;

        public readonly Player BlackPieces;

        public readonly Board Board = new Board();

        public Chess() 
        {
            WhitePieces = new Player(true, Board);
            WhitePieces = new Player(false, Board);
        }
    }
}
