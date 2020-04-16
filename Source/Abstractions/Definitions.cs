using System;
using System.Collections.Generic;
using System.Text;

namespace Mate.Abstractions
{
    public class Definitions
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
            first = 0,
            second = 1,
            third = 2,
            forth = 3,
            fifth = 4,
            sixth = 5,
            seventh = 6,
            eighth = 7
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
    }
}
