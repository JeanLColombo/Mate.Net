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

        private static HashSet<Move> GetPassant(this Chess chess, Pawn pawn)
        {
            var moves = new HashSet<Move>();

            if (chess.History.Count == 0)
                return moves;    

            Ranks rank = pawn.Color ? Ranks.five : Ranks.four;

            if (pawn.Position.Item2 != rank)
                return moves;

            var lastMove = chess.History.Last();

            var lastPiece = chess.Board.GetSquare(lastMove.Item4).Piece;

            if (!(lastPiece is Pawn))
                return moves; 

            //TODO: We've checked if this is not the first move, if this pawn is in the right rank and if last piece is a pawn.
            //TODO: Now we need to check if last piece is in position, and if it was a double move.

            return moves;
        }

    }
}