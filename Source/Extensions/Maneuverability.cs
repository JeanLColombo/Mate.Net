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
                moves.UnionWith(piece.SpecialMoves);

                foreach (Position position in piece.AttackedSquares())
                {
                    chess.ClearAttacks();

                    var captured = piece.MoveTo(position);

                    chess.UpdateAttackers();

                    if (!player.IsChecked())
                    {
                        moves.Add(new Move(piece, position));
                    }

                    piece.MoveTo(piece.LastPosition);

                    if (captured != null)
                    {
                        captured.MoveTo(position);
                    }
                }
            }

            return moves;
        }

    }
}
