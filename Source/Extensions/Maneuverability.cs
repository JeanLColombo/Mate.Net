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


    }
}
