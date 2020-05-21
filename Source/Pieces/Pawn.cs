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
            var positions = new HashSet<Position>();

            if (!this.IsOnBoard())
                return positions;

            int rankByColor = Color ? 1 : -1;

            positions.AddPosition(PawnAttackUpdate(this.GetSquare().MovePlus(1, rankByColor)));
            positions.AddPosition(PawnAttackUpdate(this.GetSquare().MovePlus(-1, rankByColor)));

            //positions.AddPosition(CheckForPassant());

            return positions;

        }

        private Position PawnAttackUpdate(Position position)
        {
            if (position == null)
                return position;

            Player.Board.Squares.TryGetValue(position, out Square square);

            if (square.Occupied())
            {
                if (square.PieceColor() == Color)
                {
                    ProtectedPieces.Add(square.Piece);
                    square.Piece.ProtectedBy.Add(this);
                }
                else
                {
                    AttackedPieces.Add(square.Piece);
                    square.Piece.AttackedBy.Add(this);
                    return position;
                }
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
                pawn.AttackedBy.Add(this);

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

        public override HashSet<Move> SpecialMoves { get => SpecializedMoves(); }

        /// <summary>
        /// Pawn can only move foward when new <see cref="Position"/> is empty. If <see cref="Pawn"/> has not moved, it can move two <see cref="Position"/>'s.
        /// </summary>
        /// <returns></returns>
        private HashSet<Position> MoveFoward()
        {
            var positions = new HashSet<Position>();
            
            if (!this.IsOnBoard())
                return positions;

            var fowardPosition = this.GetSquare().MoveThrough<Ranks>(Color ? 1 : -1);

            if (this.Player.Board.PositionIsEmpty(fowardPosition))
            {
                positions.Add(fowardPosition);

                if (!HasMoved)
                {
                    var rushPosition = this.GetSquare().MoveThrough<Ranks>(Color ? 2 : -2);

                    if (this.Player.Board.PositionIsEmpty(fowardPosition))
                    {
                        positions.Add(rushPosition);
                    }
                }
            }
            
            return positions;
        }

        /// <summary>
        /// Organizes <see cref="Pawn.SpecialMoves"/>.
        /// </summary>
        /// <returns></returns>
        private HashSet<Move> SpecializedMoves()
        {
            var moves = new HashSet<Move>();

            foreach (var position in MoveFoward())
            {
                if (position.Item2 == (Color ? Ranks.eigth : Ranks.one))
                {
                    moves.Add(new Move(this, position, MoveType.PromoteToKnight));
                    moves.Add(new Move(this, position, MoveType.PromoteToBishop));
                    moves.Add(new Move(this, position, MoveType.PromoteToRook));
                    moves.Add(new Move(this, position, MoveType.PromoteToQueen));
                }
                else
                {
                    moves.Add(new Move(this, position));
                }
            }

            // Add passant?

            return moves;
        }
    }
}
