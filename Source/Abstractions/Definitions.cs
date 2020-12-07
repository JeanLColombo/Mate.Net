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
        Stalemate = 1,
        CheckmateWhite = 2,
        CheckmateBlack = 3
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

    public enum MoveType
    {
        Normal,
        KingSideCastle,
        QueenSideCaste,
        Passant,
        PromoteToKnight,
        PromoteToBishop,
        PromoteToRook,
        PromoteToQueen
    }

    public class Position : Tuple<Files, Ranks>
    {
        public Position(Files file, Ranks rank) : base(file, rank) { }

        public bool SamePosition(Position position)
          => (this.Item1 == position.Item1 && this.Item2 == position.Item2);

    }

    /// <summary>
    /// Move data for a <see cref="Piece"/>. Contains the new <see cref="Position"/>, and <see cref="MoveType"/> information.
    /// </summary>
    public class Move : Tuple<Piece, Position, MoveType>
    {
        public Move(Piece piece, Position position, MoveType specialPiece = MoveType.Normal) 
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
    

    /// <summary>
    /// A tupple containing <see cref="Move"/> entries for a <see cref="Match"/>.
    /// </summary>
    public class MoveEntry : Tuple<int,MoveType, Position, Position>
    {
        /// <summary>
        /// Documents the <paramref name="origin"/>-th <see cref="Move"/>, of <see cref="Piece"/> <paramref name="piece"/>, from <paramref name="origin"/> to <paramref name="destination"/> <see cref="Position"/>.
        /// </summary>
        /// <param name="move"><see cref="Move"/> number. </param>
        /// <param name="moveType"><see cref="MoveType"/> moved.</param>
        /// <param name="origin"><paramref name="piece"/>'s original <see cref="Position"/>.</param>
        /// <param name="destination"><paramref name="piece"/>'s original <see cref="Position"/>.</param>
        public MoveEntry(int move, MoveType moveType, Position origin, Position destination)
            : base(move, moveType, origin, destination) { }
    }

    /// <summary>
    /// Stores the history of <see cref="MoveEntry"/>.
    /// </summary> 
    public class History : HashSet<MoveEntry>
    {
    }
}
