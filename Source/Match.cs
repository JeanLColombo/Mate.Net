using Mate.Extensions;
using Mate.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Mate.Pieces;
using System.Text.RegularExpressions;
using System.Linq;

namespace Mate
{
    class Match
    {
        private readonly Chess chess = new Chess();

        public int CurrentMove { get; private set; } = 1;

        public bool PlayerTurn { get; private set; } = true;

        public IReadOnlyCollection<Piece> WhitePieces { get => chess.White.Pieces; }

        public IReadOnlyCollection<Piece> BlackPieces { get => chess.Black.Pieces; }

        public IReadOnlyCollection<Piece> WhiteCapturedPieces { get => chess.White.Captured; }

        public IReadOnlyCollection<Piece> BlackCapturedPieces { get => chess.Black.Captured; }

        public IReadOnlyCollection<Move> AvailableMoves { get => chess.LegalMoves(PlayerTurn); }

        public IReadOnlyCollection<MoveEntry> MoveEntries { get => chess.History.ToList<MoveEntry>(); }

        public bool CurrentPlayerIsChecked 
        {
            get
            {
                chess.ClearAttacks();
                chess.UpdateAttackers();

                return PlayerTurn ? chess.White.IsChecked() : chess.Black.IsChecked();
            }
        }

        public Outcome Outcome { get; private set; } = Outcome.Game;

        /// <summary>
        /// Initializes a <see cref="Match"/> of <see cref="Chess"/> with a Standard <see cref="Piece"/> setup.
        /// </summary>
        public Match()
        {
            chess.White.StandardSetup();
            chess.Black.StandardSetup();
        }

        /// <summary>
        /// Initializes a <see cref="Match"/> of <see cref="Chess"/> with a personalized <see cref="CustomPieceInput"/>.
        /// </summary>
        /// <param name="customPieces">Customizable <see cref="PieceInput"/>.</param>
        public Match(CustomPieceInput customPieces) =>
            CustomInitialization(customPieces);
        
        /// <summary>
        /// Process the <paramref name="moveIndex"/>-th <see cref="Move"/> available from a player, based on <see cref="PlayerTurn"/>. Also calls <see cref="UpdateOutcome()"/>.
        /// </summary>
        /// <param name="moveIndex"></param>
        /// <returns></returns>
        public Outcome ProcessMove(uint moveIndex)
        {
            if (Outcome != Outcome.Game)
                return Outcome;

            var availableMoves = chess.LegalMoves(PlayerTurn);

            if (moveIndex >= availableMoves.Count)
            {
                throw new ArgumentOutOfRangeException("Move Index is greater than Move.Count", nameof(moveIndex));
            }

            //TODO: This is the precise moment when bug happens.
            ProcessMove(availableMoves.ElementAt((int)moveIndex));

            return UpdateOutcome();
        }

        private void CustomInitialization(CustomPieceInput customPieces)
        {
            foreach (PieceInput piece in customPieces)
            {
                var player = piece.Item1 ? chess.White : chess.Black;
                var position = new Position(piece.Item3, piece.Item4);

                switch (piece.Item2)
                {
                    case ChessPieces.Pawns:
                        player.AddPiece<Pawn>(position);
                        break;
                    case ChessPieces.Rooks:
                        player.AddPiece<Rook>(position);
                        break;
                    case ChessPieces.Knights:
                        player.AddPiece<Knight>(position);
                        break;
                    case ChessPieces.Bishops:
                        player.AddPiece<Bishop>(position);
                        break;
                    case ChessPieces.Queen:
                        player.AddPiece<Queen>(position);
                        break;
                    case ChessPieces.King:
                        player.AddPiece<King>(position);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Toggles <see cref="Match.PlayerTurn"/>. If it is <see cref="WhitePieces"/> turn, also increases <see cref="Match.CurrentMove"/> by one.
        /// </summary>
        private void UpdatePlayerTurn()
        {
            PlayerTurn = PlayerTurn ? false : true;
            if (PlayerTurn)
                CurrentMove++;
        }

        /// <summary>
        /// Checks for <see cref="Outcome.Stalemate"/>, <see cref="Outcome.CheckmateWhite"/> or <see cref="Outcome.CheckmateBlack"/>, while also calling <see cref="UpdatePlayerTurn"/>.
        /// </summary>
        /// <returns></returns>
        private Outcome UpdateOutcome()
        {
            bool NextPlayer = PlayerTurn ? false : true;

            if (chess.LegalMoves(NextPlayer).Count == 0)
            {
                if (NextPlayer ? chess.White.IsChecked() : chess.Black.IsChecked())
                    Outcome = NextPlayer ? Outcome.CheckmateBlack : Outcome.CheckmateWhite;
                else
                    Outcome = Outcome.Stalemate;

                return Outcome;
            }

            UpdatePlayerTurn();

            return Outcome;
        }


        /// <summary>
        /// Process the move, based on <see cref="Move"/> tuple structure.
        /// </summary>
        /// <param name="move"></param>
        private void ProcessMove(Move move)
        {

            chess.ClearAttacks();

            Piece captured = null;

            var playerOne = PlayerTurn ? chess.White : chess.Black;
            var playerTwo = PlayerTurn ? chess.Black : chess.White;

            chess.History.Add(this.GetMoveEntry(move));

            switch (move.Item3) 
            {
                case MoveType.Normal:
                    captured = move.Item1.MoveTo(move.Item2);
                    break;
                case MoveType.KingSideCastle:
                    //TODO: Implement SpecialMoves
                    break;
                case MoveType.QueenSideCaste:
                    break;
                case MoveType.Passant:
                    break;
                case MoveType.PromoteToKnight:
                    move.Item1.ChangePosition();
                    playerOne.Pieces.Remove(move.Item1);
                    playerOne.AddPiece<Knight>(move.Item2);
                    break;
                case MoveType.PromoteToBishop:
                    move.Item1.ChangePosition();
                    playerOne.Pieces.Remove(move.Item1);
                    playerOne.AddPiece<Bishop>(move.Item2);
                    break;
                case MoveType.PromoteToRook:
                    move.Item1.ChangePosition();
                    playerOne.Pieces.Remove(move.Item1);
                    playerOne.AddPiece<Rook>(move.Item2);
                    break;
                case MoveType.PromoteToQueen:
                    move.Item1.ChangePosition();
                    playerOne.Pieces.Remove(move.Item1);
                    playerOne.AddPiece<Queen>(move.Item2);
                    break;
                default:
                    break;
            }

            move.Item1.HasMoved = true;

            if (captured != null)
            {
                playerOne.Captured.Add(captured);
                playerTwo.Pieces.Remove(captured);
            }

            chess.ClearAttacks();
        }

        private MoveEntry GetMoveEntry(Move move) => new MoveEntry(CurrentMove, move.Item3, move.Item1.Position, move.Item2);

    }
}
