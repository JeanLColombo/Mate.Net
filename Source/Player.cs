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

        public readonly King King;

        public Player(bool color, Board board, Position kingPosition = null)
        {
            Color = color;
            Board = board;

            Pieces = new List<Piece>();
            Captured = new List<Piece>();

            King = AddKing(kingPosition);
        }

        private King AddKing(Position position)
        {
            if (position == null)
                this.AddPiece<King>(KingPositionByColor());
            else
                this.AddPiece<King>(position);

            return (King)Pieces.Last();
        }


        private Position KingPositionByColor()
        {
            return Color ? 
                new Position(Files.e, Ranks.one) : 
                new Position(Files.e, Ranks.eigth);
        }

        public void UpdateAttackers()
        {
            foreach (Piece piece in Pieces)
            {
                piece.AttackedSquares();
            }

        
        }


    }
}
