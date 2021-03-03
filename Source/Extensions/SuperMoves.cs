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

            var lastSquare = chess.Board.GetSquare(lastMove.Item4);

            if (!(lastSquare.Piece is Pawn))
                return moves; 

            if (!(pawn.GetSquare().GetAdjacentPositions().Contains(lastSquare.GetPosition())))
                return moves; 

            var passantOriginalFile = lastSquare.GetPosition().Item1;
            var passantOriginalRank = pawn.Color ? Ranks.seven : Ranks.two;

            var passantOriginalPosition = new Position(passantOriginalFile, passantOriginalRank);

            if (lastSquare.GetPosition() != passantOriginalPosition)
                return moves;

            moves.Add(
                new Move(
                    pawn, 
                    new Position(passantOriginalFile, pawn.Color ? Ranks.six : Ranks.three), 
                    MoveType.Passant));

            //TODO: Passant Finished. In dare need of testing!

            return moves;
        }

    }
}