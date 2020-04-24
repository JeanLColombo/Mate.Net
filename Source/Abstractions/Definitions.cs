using System;
using System.Collections.Generic;
using System.Text;


namespace Mate.Abstractions
{
    public enum Files
    {
        a = 0,
        b = 1,
        c = 2,
        d = 3,
        e = 4,
        f = 5,
        g = 6,
        h = 7
    }

    public enum Ranks
    {
        one = 0,
        two = 1,
        three = 2,
        four = 3,
        five = 4,
        six = 5,
        seven = 6,
        eigth = 7
    }

    public enum Outcome
    {
        Game = 0,
        AgreedDraw = 1,
        Threefold = 2,
        Fiftyfold = 3,
        Stalemate = 4,
        ResignBlack = 5,
        ResignWhite = 6,
        MateWhite = 7,
        MateBlack = 8
    }

    public enum Direction
    {
        File = 0,
        Rank = 1,
        MainDiagonal = 2,
        OppositeDiagonal = 3
    }

    public class Position : Tuple<Files, Ranks>
    {
        public Position(Files file, Ranks rank) : base(file, rank) { }

        public bool SamePosition(Position position)
          => (this.Item1 == position.Item1 && this.Item2 == position.Item2);


    }
}
