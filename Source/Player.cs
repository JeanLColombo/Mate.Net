using Mate.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Mate
{
    public class Player
    {

        //TODO: Check if private set is ok.

        public List<Piece> Pieces { get; private set; }

        public  List<Piece> Captured { get; private set; }

        public readonly bool Color;

        public Player(bool color)
        {
            Color = color;
            Pieces = new List<Piece>();
            Captured = new List<Piece>();
        }

    }
}
