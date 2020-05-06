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
            if (piece is King)
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

        /// <summary>
        /// Creates a <typeparamref name="TPiece"/> : <see cref="Piece"/> at each individual <see cref="Position"/> in <paramref name="positions"/>.
        /// </summary>
        /// <typeparam name="TPiece">Type of the <see cref="Piece"/> created.</typeparam>
        /// <param name="player"><see cref="Piece.Color"/> will be the same as <see cref="Player.Color"/>.</param>
        /// <param name="positions"><see cref="HashSet{T}"/> of <see cref="Position"/>'s.</param>
        internal static void AddPieces<TPiece>(this Player player, HashSet<Position> positions) where TPiece : Piece
        {
            foreach (var position in from Position position in positions
                                     where !player.AddPiece<TPiece>(position)
                                     select position)
            {
                throw new ArgumentException("Cannot place piece at a occupied position", nameof(position));
            }
        }

        /// <summary>
        /// Standard Chess Setup based on <see cref="Player.Color"/>.
        /// </summary>
        /// <param name="player"></param>
        internal static void StandardSetup(this Player player)
        {
            if (player.Pieces.Count > 0)
            {
                throw new ApplicationException("Player pieces already initialized!");
            }

            foreach (ChessPieces pieces in Enum.GetValues(typeof(ChessPieces)))
            {
                player.StandardPlacement(pieces);
            }
        }


        /// <summary>
        /// Returns the Chess Standard <see cref="Position"/>'s based on <see cref="ChessPieces"/>.
        /// </summary>
        /// <param name="player">Standard <see cref="Position"/>'s are <see cref="Player.Color"/> dependant.</param>
        /// <param name="chessPieces"><see cref="Enum"/> for Standard Chess <see cref="Piece"/>'s.</param>
        /// <returns></returns>
        internal static HashSet<Position> StandardPositions(this Player player, ChessPieces chessPieces)
        {
            var positions = new HashSet<Position>();

            Ranks rank = player.RankByColor();

            switch (chessPieces)
            {
                case ChessPieces.Pawns:
                    rank = player.Color ? Ranks.two : Ranks.seven;
                    foreach (Files file in Enum.GetValues(typeof(Files)))
                    {
                        positions.AddPosition(new Position(file, rank));
                    }
                    break;

                case ChessPieces.Rooks:
                    positions.AddPosition(new Position(Files.a, rank)); ;
                    positions.AddPosition(new Position(Files.h, rank));
                    break;

                case ChessPieces.Knights:
                    positions.AddPosition(new Position(Files.b, rank));
                    positions.AddPosition(new Position(Files.g, rank));
                    break;

                case ChessPieces.Bishops:
                    positions.AddPosition(new Position(Files.c, rank));
                    positions.AddPosition(new Position(Files.f, rank));
                    break;

                case ChessPieces.Queen:
                    positions.AddPosition(new Position(Files.d, rank));
                    break;

                case ChessPieces.King:
                    positions.AddPosition(new Position(Files.e, rank));
                    break;

                default:
                    break;
            }

            return positions;
        }

        /// <summary>
        /// Creates <paramref name="chessPieces"/> based on their <see cref="StandardPositions(Player, ChessPieces)"/>.
        /// </summary>
        /// <param name="player">Creates <see cref="Piece"/>'s at <see cref="Player.Pieces"/>.</param>
        /// <param name="chessPieces"></param>
        public static void StandardPlacement(this Player player, ChessPieces chessPieces)
        {
            var positions = player.StandardPositions(chessPieces);

            switch (chessPieces)
            {
                case ChessPieces.Pawns:
                    player.AddPieces<Pawn>(positions);
                    break;
                case ChessPieces.Rooks:
                    player.AddPieces<Rook>(positions);
                    break;
                case ChessPieces.Knights:
                    player.AddPieces<Knight>(positions);
                    break;
                case ChessPieces.Bishops:
                    player.AddPieces<Bishop>(positions);
                    break;
                case ChessPieces.Queen:
                    player.AddPieces<Queen>(positions);
                    break;
                case ChessPieces.King:
                    player.AddPieces<King>(positions);
                    break;
                default:
                    break;
            }
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
