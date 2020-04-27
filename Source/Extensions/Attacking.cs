﻿using System;
using System.Collections.Generic;
using System.Text;
using Mate.Pieces;
using Mate.Abstractions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Mate.Net.UT")]

namespace Mate.Extensions
{
    internal static class Attacking
    {
        /// <summary>
        /// Updates <see cref="Piece.UnderAttack"/> and <see cref="Piece.AttackedPieces"/> from an attacked position.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="position">A new position based on <see cref="Piece"/> maneuverability.</param>
        /// <returns></returns>
        internal static Position UpdateAttackersFrom(this Piece piece, Position position)
        {
            if (position == null)
                return position;

            piece.Player.Board.Squares.TryGetValue(position, out Square square);

            if (square.Empty())
                return position;
            else
            {
                if (square.PieceColor() != piece.Color)
                {
                    piece.AttackedPieces.Add(square.Piece);
                    square.Piece.UnderAttack.Add(piece);
                    return position;
                }
            }

            return null;
        }

        /// <summary>
        /// Get a <see cref="HashSet{T}"/> of <see cref="Position"/> relative to a <see cref="Piece"/> attacking through a certain <see cref="Direction"/>.
        /// </summary>
        /// <param name="piece">Attacking <see cref="Piece"/></param>
        /// <param name="direction">Attack <see cref="Direction"/></param>
        /// <param name="orientation">Attacking orientation (if <see cref="true"/> attack through the positive orientation).</param>
        /// <returns></returns>
        internal static HashSet<Position> AttackThrough(this Piece piece, Direction direction, bool orientation)
        {
            if (orientation)
                return piece.AttackThroughInternalLogic(direction, 1);
            else
                return piece.AttackThroughInternalLogic(direction, -1);
        }

        /// <summary>
        /// Internal logic for <see cref="AttackThrough(Piece, Direction, bool)"/>
        /// </summary>
        /// <param name="piece">Attacking <see cref="Piece"/>.</param>
        /// <param name="direction">Attack <see cref="Direction"/>.</param>
        /// <param name="numberOfSquares">Number of <see cref="Square"/>'s.</param>
        /// <returns></returns>
        private static HashSet<Position> AttackThroughInternalLogic(this Piece piece, Direction direction, int numberOfSquares)
        {
            var positions = new HashSet<Position>();

            var attackerCount = piece.AttackedPieces.Count;

            bool notKing = (piece.GetType() != typeof(King));

            switch (direction)
            {
                case Direction.Files:
                    if (positions.AddPosition(piece.UpdateAttackersFrom(piece.GetSquare().MoveThrough<Files>(numberOfSquares))) &&
                        (attackerCount == piece.AttackedPieces.Count) &&
                        notKing)
                    {
                        numberOfSquares += Math.Sign(numberOfSquares);
                        positions.UnionWith(piece.AttackThroughInternalLogic(direction, numberOfSquares));
                    }
                    break;
                case Direction.Ranks:
                    if (positions.AddPosition(piece.UpdateAttackersFrom(piece.GetSquare().MoveThrough<Ranks>(numberOfSquares))) &&
                        (attackerCount == piece.AttackedPieces.Count) &&
                        notKing)
                    {
                        numberOfSquares += Math.Sign(numberOfSquares);
                        positions.UnionWith(piece.AttackThroughInternalLogic(direction, numberOfSquares));
                    }
                    break;
                case Direction.MainDiagonal:
                    if (positions.AddPosition(piece.UpdateAttackersFrom(piece.GetSquare().MovePlus(numberOfSquares, numberOfSquares))) &&
                        (attackerCount == piece.AttackedPieces.Count) &&
                        notKing)
                    {
                        numberOfSquares += Math.Sign(numberOfSquares);
                        positions.UnionWith(piece.AttackThroughInternalLogic(direction, numberOfSquares));
                    }
                    break;
                case Direction.OppositeDiagonal:
                    if (positions.AddPosition(piece.UpdateAttackersFrom(piece.GetSquare().MovePlus(-numberOfSquares, numberOfSquares))) &&
                        (attackerCount == piece.AttackedPieces.Count) &&
                        notKing)
                    {
                        numberOfSquares += Math.Sign(numberOfSquares);
                        positions.UnionWith(piece.AttackThroughInternalLogic(direction, numberOfSquares));
                    }
                    break;
                default:
                    break;
            }

            return positions;
        }
    }
}
