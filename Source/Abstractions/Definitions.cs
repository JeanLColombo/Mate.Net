using System;
using System.Collections.Generic;
using System.Text;


namespace Mate.Abstractions
{
    public enum Files
    {
        a = 0,
        b = 1,
        c = 2,
        d = 3,
        e = 4,
        f = 5,
        g = 6,
        h = 7
    }

    public enum Ranks
    {
        one = 0,
        two = 1,
        three = 2,
        four = 3,
        five = 4,
        six = 5,
        seven = 6,
        eigth = 7
    }

    public enum Outcome
    {
        Game = 0,
        AgreedDraw = 1,
        Threefold = 2,
        Fiftyfold = 3,
        Stalemate = 4,
        ResignBlack = 5,
        ResignWhite = 6,
        MateWhite = 7,
        MateBlack = 8
    }

    public enum Direction
    {
        Files = 0,
        Ranks = 1,
        MainDiagonal = 2,
        OppositeDiagonal = 3
    }

    public enum ChessPieces
    {
        Pawns,
        Rooks,
        Knights,
        Bishops,
        Queen,
        King
    }

    public enum SpecialMoves
    {
        None,
        KingSideCastle,
        QueenSideCaste,
        Passant,
        PromoteToBishop,
        PromoteToKnight,
        PromoteToRook,
        PromoteToQueen
    }

    public class Position : Tuple<Files, Ranks>
    {
        public Position(Files file, Ranks rank) : base(file, rank) { }

        public bool SamePosition(Position position)
          => (this.Item1 == position.Item1 && this.Item2 == position.Item2);

    }

    public class Move : Tuple<Piece, Position, SpecialMoves>
    {
        public Move(Piece piece, Position position, SpecialMoves specialPiece = SpecialMoves.None) 
            : base(piece, position, specialPiece) { }
    }
    
    /// <summary>
    /// Piece data for <see cref="CustomPieceInput"/>.
    /// </summary>
    public class PieceInput : Tuple<bool,ChessPieces,Files,Ranks>
    {
        /// <summary>
        /// <see cref="Tuple"/> with <paramref name="pieceColor"/> and <paramref name="pieceType"/>, to be positioned at specified <paramref name="file"/> and <paramref name="rank"/>.
        /// </summary>
        /// <param name="pieceColor"><see cref="true"/>=<see cref="Chess.White"/>, <see cref="false"/>=<see cref="Chess.Black"/>.</param>
        /// <param name="pieceType"><see cref="Enum"/> for <see cref="Piece"/> <see cref="Type"/>.</param>
        /// <param name="file"><see cref="Files"/>.</param>
        /// <param name="rank"><see cref="Ranks"/>.</param>
        public PieceInput(bool pieceColor, ChessPieces pieceType, Files file, Ranks rank)
            : base(pieceColor, pieceType, file, rank) { }
    }

    /// <summary>
    /// <see cref="HashSet{T}"/> of <see cref="PieceInput"/>.
    /// </summary>
    public class CustomPieceInput : HashSet<PieceInput>
    {
    }
}
