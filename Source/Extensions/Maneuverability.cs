using Mate.Pieces;
using Mate.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mate.Extensions
{
    public static class Maneuverability
    {
        /// <summary>
        /// Returns whether <see cref="Player.King"/> is being attacked or not. If <see cref="Player.King"/> is <see cref="null"/>, return <see cref="false"/>.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static bool IsChecked(this Player player)
        {
            if (player.King == null)
                return false;

            return player.King.GetAttackers().Count > 0;
        }

        public static IReadOnlyCollection<Piece> GetAttackers(this Piece piece) => piece.AttackedBy;

        public static IReadOnlyCollection<Piece> GetAttackedPieces(this Piece piece) => piece.AttackedPieces;

        public static IReadOnlyCollection<Piece> GetDefenders(this Piece piece) => piece.ProtectedBy;

        public static IReadOnlyCollection<Piece> GetDefendedPieces(this Piece piece) => piece.ProtectedPieces;

        /// <summary>
        /// Returns a <see cref="HashSet{T}"/> of <see cref="Move"/>'s available for a <see cref="Player"/>, based on <paramref name="color"/>.
        /// </summary>
        /// <param name="chess">A game of <see cref="Chess"/>.</param>
        /// <param name="color"><see cref="Player.Color"/>.</param>
        /// <returns></returns>
        public static HashSet<Move> LegalMoves(this Chess chess, bool color)
        {
            var moves = new HashSet<Move>();

            Player player = color ? chess.White : chess.Black;

            foreach (Piece piece in player.Pieces)
            {
                var pieceMoves = piece.SpecialMoves;

                foreach (Position position in piece.AttackedSquares())
                {
                    pieceMoves.Add(new Move(piece, position));
                }

                moves.UnionWith(chess.CheckLegalityOf(pieceMoves));

            }

            return moves;
        }

        /// <summary>
        /// Checks all the <see cref="Move"/>'s in a <see cref="HashSet{T}"/>, and return only those that are legal. 
        /// </summary>
        /// <param name="chess"></param>
        /// <param name="moves"></param>
        /// <returns></returns>
        private static HashSet<Move> CheckLegalityOf(this Chess chess, HashSet<Move> moves)
        {
            var legalMoves = new HashSet<Move>();

            foreach (var move in moves)
            {
                chess.ClearAttacks();

                var player = move.Item1.Color ? chess.White : chess.Black;

                // TODO: Implement legality of Moves

                switch (move.Item3)
                {
                    case MoveType.KingSideCastle:
                        break;
                    case MoveType.QueenSideCaste:
                        break;
                    case MoveType.Passant:
                        break;
                    default:
                        var captured = move.Item1.MoveTo(move.Item2);
                        chess.UpdateAttackers();
                        if (!player.IsChecked())
                        {
                            legalMoves.Add(new Move(move.Item1, move.Item2, move.Item3));
                        }
                        move.Item1.MoveTo(move.Item1.LastPosition);
                        if (captured != null)
                        {
                            captured.MoveTo(move.Item2);
                        }
                        break;
                }
            }


            return legalMoves;
        }

    }
}
