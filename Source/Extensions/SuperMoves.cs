using System;
using System.Collections.Generic;
using System.Text;
using Mate.Abstractions;
using Mate.Extensions;
using System.Linq;
using Mate.Pieces;

// TODO: Check internals visibility
// TODO: Implement passant!!!

namespace Mate.Extensions
{
    internal static class SuperMoves
    {
        // TODO: How to implement supermoves, if it is a match or chess responsability.
        internal static HashSet<Move> GetSuperMoves(this Chess chess, bool color)
        {
            var moves = new HashSet<Move>();

            Player player = color ? chess.White : chess.Black;

            foreach (Piece piece in player.Pieces)
            {
                if (piece is Pawn)
                {

                }

            }

            return moves;

        }

    }
}