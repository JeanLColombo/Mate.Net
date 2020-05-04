using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using Mate.Abstractions;
using Mate.Pieces;

namespace Mate.Extensions
{
    public static class PieceSetup
    {
        /// <summary>
        /// Add a new <typeparamref name="TPiece"/> : <see cref="Piece"/> to a <see cref="Player"/> set. 
        /// Snippet of code acquired from 
        /// <see href="https://stackoverflow.com/questions/731452/create-instance-of-generic-type-whose-constructor-requires-a-parameter">
        /// StackOverlfow</see>.
        /// </summary>
        /// <typeparam name="TPiece">Type of the <see cref="Piece"/> created.</typeparam>
        /// <param name="player"><see cref="Piece.Color"/> will be the same as <see cref="Player.Color"/>.</param>
        /// <param name="position"><see cref="Position"/> relative to the <see cref="Square"/> where the piece will be placed.</param>
        /// <returns>Returns <see cref="false"/> if <paramref name="position"/> is occupied. Otherwise <see cref="true"/>.</returns>
        public static bool AddPiece<TPiece>(this Player player, Position position) where TPiece : Piece
        {
            if (!player.Board.PositionIsEmpty(position))
                return false;

            var piece = (TPiece)Activator.CreateInstance(typeof(TPiece), new object[] { player, position });

            player.Board.Squares.TryGetValue(position, out Square square);

            square.Piece = piece;

            player.Pieces.Add(piece);

            return true;
        }

        internal static bool StandardSetup(this Player player)
        {
            if (player.Pieces.Count > 1)
            {
                throw new ApplicationException("Player pieces already initialized!");
            }

            player.StandardPawnPlacement();


            return true;
            //TODO: Implement this method;
        }

        /// <summary>
        /// Properly place pawns in their standard positions.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        internal static bool StandardPawnPlacement(this Player player)
        {
            Ranks rank = player.Color ? Ranks.two : Ranks.seven;

            foreach (Files file in Enum.GetValues(typeof(Files)))
            {
                if (!player.AddPiece<Pawn>(new Position(file, rank)))
                    return false;
            }

            return true;
        }
    }
}
