using Mate.Abstractions;
using Mate.Pieces;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Schema;

namespace Mate.Extensions
{
    public static class Positioning
    {

        /// <summary>
        /// Returns the square position.
        /// </summary>
        /// <param name="square"></param>
        /// <returns></returns>
        public static Position GetPosition(this Square square) => new Position(square.File, square.Rank);
        

        /// <summary>
        /// Move along the <see cref="Files"/> or <see cref="Ranks"/> from a certain <paramref name="square"/> position.
        /// </summary>
        /// <typeparam name="T"><see cref="Files"/> or <see cref="Ranks"/>.</typeparam>
        /// <param name="square">Actual Square.</param>
        /// <param name="numberOfSquares">Number of squares moved.</param>
        /// <returns></returns>
        public static Position MoveThrough<T>(this Square square, int numberOfSquares)
        { 
            var newFile = (int)square.File;
            var newRank = (int)square.Rank;


            if (typeof(T) == typeof(Files))
                newFile += numberOfSquares;
            else if (typeof(T) == typeof(Ranks))
                newRank += numberOfSquares;
            else
                throw new ApplicationException("Must Move Through Definition.Ranks or Definition.Files!");


            if (numberOfSquares == 0)
                return square.GetPosition();


            if (!Enum.IsDefined(typeof(T), newFile) || !Enum.IsDefined(typeof(T),newRank))
                return null;


            return new Position(
                (Files)newFile,
                (Ranks)newRank);
        }
        
        /// <summary>
        /// From <paramref name="square"/>, move a certain <paramref name="numberOfFiles"/> and <paramref name="numberOrRanks"/>.
        /// </summary>
        /// <param name="square">Actual <see cref="Square"/></param>
        /// <param name="numberOfFiles">Number of <see cref="Files"/> moved.</param>
        /// <param name="numberOrRanks">Number of <see cref="Ranks"/> moved.</param>
        /// <returns>Returns <see cref="null"/> if invalid square.</returns>
        public static Position MovePlus(this Square square, int numberOfFiles, int numberOrRanks)
        {
            //TODO: Implement Move Method.

            var newSquareFile = square.MoveThrough<Files>(numberOfFiles);
            var newSquareRank = square.MoveThrough<Ranks>(numberOrRanks);

            if (newSquareFile == null || newSquareRank == null)
                return null;

            return new Position(
                newSquareFile.Item1, 
                newSquareRank.Item2);
        }

        public static bool Empty(this Square square) => (square.Piece == null);

        public static bool Occupied(this Square square) => !square.Empty();

        public static bool PositionIsEmpty(this Board board, Position position)
        {
            board.Squares.TryGetValue(position, out Square square);
            return square.Empty();

        }
            
        public static bool PieceColor(this Square square)
        {
            if (square.Empty())
                throw new ApplicationException("Square is Empty. There is no Piece.");

            return square.Piece.Color;
        }

        /// <summary>
        /// Get <see cref="Square"/> based on <see cref="Piece.Position"/>. 
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        public static Square GetSquare(this Piece piece)
        {
            if (!piece.IsOnBoard() || piece.Player == null)
                return null;

            piece.Player.Board.Squares.TryGetValue(piece.Position, out Square square);

            return square;
        }

    }
}
