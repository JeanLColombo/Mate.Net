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
        internal static HashSet<Move> GetSuperMoves(this Chess chess, Player player)
        {
            var moves = new HashSet<Move>();

            foreach (Piece piece in player.Pieces)
            {
                if (piece is Pawn)
                {
                    moves.UnionWith(chess.GetPassant((Pawn)piece));
                }
            }
            
            return moves;

        }

        public static HashSet<Move> GetPassant(this Chess chess, Pawn pawn)
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

            if (!(pawn.GetSquare().GetAdjacentPositions().Contains(lastPiece.Position)))
                return moves; 

            var passantOriginalPosition = new Position(lastPiece.Position.Item1, pawn.Color ? Ranks.seven : Ranks.two);

            if (lastMove.Item3 != passantOriginalPosition)
                return moves;

            moves.Add(
                new Move(
                    pawn, 
                    new Position(passantOriginalPosition.Item1, pawn.Color ? Ranks.six : Ranks.three), 
                    MoveType.Passant));

            //TODO: Passant Finished. In dare need of testing!

            return moves;
        }

    }
}