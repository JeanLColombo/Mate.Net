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

            Piece piece = (TPiece)Activator.CreateInstance(typeof(TPiece), new object[] { player, position });

            // King instantiation logic
            if (piece.GetType() == typeof(King))
            {
                if (player.King != null)
                    throw new ApplicationException("King already instantiated");
                else
                    player.King = (King)piece;
            }

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
        /// Places <see cref="Pawn"/>'s in their standard <see cref="Position"/>'s.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        internal static void StandardPawnPlacement(this Player player)
        {
            Ranks rank = player.Color ? Ranks.two : Ranks.seven;

            foreach (Files file in Enum.GetValues(typeof(Files)))
            {
                if (!player.AddPiece<Pawn>(new Position(file, rank)))
                    throw new ApplicationException("Pawn initialization failed. Square(s) Occupied!");
            }
        }

        /// <summary>
        /// Places <see cref="Rook"/>'s in their standard <see cref="Position"/>'s.
        /// </summary>
        /// <param name="player"></param>
        internal static void StandardRookPlacement(this Player player)
        {
            var rank = player.RankByColor();

            Position[] positions = { new Position(Files.a, rank), new Position(Files.h, rank) };

            foreach (Position position in positions)
            {
                if (!player.AddPiece<Rook>(position))
                {
                    throw new ApplicationException("Rook initialization failed. Square(s) Occupied!");
                }
            }
        }

        internal static void StandardKnightPlacement(this Player player)
        {
            //TODO: Create a Template Method, beacause why not?

            var rank = player.RankByColor();

            Position[] positions = { new Position(Files.b, rank), new Position(Files.g, rank) };

            foreach (Position position in positions)
            {
                if (!player.AddPiece<Knight>(position))
                {
                    throw new ApplicationException("Knight initialization failed. Square(s) Occupied!");
                }
            }
        }

        /// <summary>
        /// Places <see cref="King"/> in its standard <see cref="Position"/>.
        /// </summary>
        /// <param name="player"></param>
        internal static void StandardKingPlacement(this Player player)
        {
            if (!player.AddPiece<King>(new Position(Files.e, player.RankByColor())))
                throw new ApplicationException("King initialization failed. Square Occupied!");
        }

        /// <summary>
        /// Returns the Standard <see cref="Ranks"/> for <see cref="Piece"/> placement, based on <see cref="Player.Color"/>.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        internal static Ranks RankByColor(this Player player)
        {
            return player.Color ? Ranks.one : Ranks.eigth;
        }
    }
}
