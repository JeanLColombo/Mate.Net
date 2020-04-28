using System;
using System.Collections.Generic;
using System.Text;
using Mate.Abstractions;
using Mate.Extensions;
using System.Linq;

namespace Mate.Pieces
{
    class Pawn : Piece
    {

        public bool MovedTwoSquares { get; private set; } = false;

        public Pawn(bool color, Position position = null) : base(color, position)
        {
        }

        public Pawn(Player player, Position position = null) : base(player, position)
        {
        }

        public override HashSet<Position> AttackedSquares()
        {
            //TODO: Unit test this strange AttackedSquares.

            var positions = new HashSet<Position>();

            if (!this.IsOnBoard())
                return positions;

            int rankByColor = Color ? 1 : -1;

            positions.AddPosition(PawnAttackUpdate(this.GetSquare().MovePlus(1, rankByColor)));
            positions.AddPosition(PawnAttackUpdate(this.GetSquare().MovePlus(-1, rankByColor)));

            positions.AddPosition(CheckForPassant());

            return positions;

        }

        public override bool MoveTo(Position position)
        {
            throw new NotImplementedException();
        }

        private Position PawnAttackUpdate(Position position)
        {
            if (position == null)
                return position;

            Player.Board.Squares.TryGetValue(position, out Square square);

            if (
                square.Occupied()                         && 
                (square.PieceColor() != this.Color))
            {
                this.AttackedPieces.Add(square.Piece);
                square.Piece.UnderAttack.Add(this);
                return position;
            }

            return null;
        }

        private Position CheckForPassant()
        {
            Ranks rank = Color ? Ranks.five : Ranks.four;

            if (Position.Item2 == rank)
            {
                var pawn = GetAdjacentPawn(false);

                if (pawn == null)
                {
                    pawn = GetAdjacentPawn(true);
                }

                if (pawn == null)
                {
                    return null;
                }

                AttackedPieces.Add(pawn);
                pawn.UnderAttack.Add(this);

                Ranks positionRank = Color ? Ranks.six : Ranks.three;

                return new Position(pawn.Position.Item1, positionRank);
            }

            return null;
        }

        private Pawn GetAdjacentPawn(bool right)
        {
            var numberOfFiles = right ? 1 : -1;

            var position = this.GetSquare().MoveThrough<Files>(numberOfFiles);

            if (position == null)
                return null;

            Player.Board.Squares.TryGetValue(position, out Square square);

            if (square.Empty())
                return null;

            var piece = square.Piece;

            if (piece.GetType() != typeof(Pawn))
                return null;

            Pawn pawn = (Pawn)piece;

            if (!pawn.MovedTwoSquares)
                return null;

            return pawn;

        }
    }
}
