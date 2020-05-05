using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using Mate.Abstractions;
using Mate.Pieces;

namespace Mate.Extensions
{
    internal static class PieceSetup
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
        internal static bool AddPiece<TPiece>(this Player player, Position position) where TPiece : Piece
        {
            if (!player.Board.PositionIsEmpty(position))
                return false;

            // Cannot Add two kings.
            if (typeof(TPiece) == typeof(King) && player.King != null)
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

        /// <summary>
        /// Default <see cref="King"/> initialization. Can add at a specified <see cref="Position"/>.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="position">Specified <see cref="Position"/>. If <see cref="null"/>, instatiate at <see cref="StandardKingPosition(Player)"/>.</param>
        /// <returns></returns>
        internal static bool AddKing(this Player player, Position position = null)
        {            
            var kingAdded = player.AddPiece<King>(
                (position == null) ? 
                player.StandardKingPosition() : 
                position);

            if (kingAdded)
                player.King = (King)player.Pieces.Last();

            return kingAdded;
        }

        /// <summary>
        /// Returns the standard <see cref="King"/> <see cref="Position"/> for a <paramref name="player"/>, based on <see cref="Player.Color"/>.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private static Position StandardKingPosition(this Player player)
        {
            return player.Color ?
                new Position(Files.e, Ranks.one) :
                new Position(Files.e, Ranks.eigth);
        }
    }
}
