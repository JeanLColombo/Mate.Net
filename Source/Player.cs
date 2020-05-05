using Mate.Abstractions;
using Mate.Extensions;
using Mate.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public readonly Board Board;

        public King King { get; internal set; } = null;

        public Player(bool color, Board board, Position kingPosition = null)
        {
            Color = color;
            Board = board;

            Pieces = new List<Piece>();
            Captured = new List<Piece>();
        }

        public void UpdateAttackers()
        {
            //TODO: Send to an extension.
            foreach (Piece piece in Pieces)
            {
                piece.AttackedSquares();
            }

        
        }


    }
}
