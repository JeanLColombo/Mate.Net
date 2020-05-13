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
    class Game
    {
        private readonly Chess chess = new Chess();

        public int CurrentMove { get; private set; } = 1;

        public bool PlayerTurn { get; private set; } = true;

        public IReadOnlyCollection<Piece> WhitePieces { get => chess.White.Pieces; }

        public IReadOnlyCollection<Piece> BlackPieces { get => chess.Black.Pieces; }

        public Outcome Outcome { get; private set; } = Outcome.Game;

        /// <summary>
        /// Initializes a <see cref="Game"/> of <see cref="Chess"/> with a Standard <see cref="Piece"/> setup.
        /// </summary>
        public Game()
        {
            chess.White.StandardSetup();
            chess.Black.StandardSetup();
        }

        /// <summary>
        /// Initializes a <see cref="Game"/> of <see cref="Chess"/> with a personalized <see cref="CustomPieceInput"/>.
        /// </summary>
        /// <param name="customPieces">Customizable <see cref="PieceInput"/>.</param>
        public Game(CustomPieceInput customPieces) =>
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
        /// Toggles <see cref="Game.PlayerTurn"/>. If it is <see cref="WhitePieces"/> turn, also increases <see cref="Game.CurrentMove"/> by one.
        /// </summary>
        private void UpdatePlayerTurn()
        {
            PlayerTurn = PlayerTurn ? false : true;
            if (PlayerTurn)
                CurrentMove++;
        }

        /// <summary>
        /// Checks for <see cref="Outcome.Stalemate"/>, <see cref="Outcome.MateWhite"/> or <see cref="Outcome.MateBlack"/>, while also calling <see cref="UpdatePlayerTurn"/>.
        /// </summary>
        /// <returns></returns>
        private Outcome UpdateOutcome()
        {
            UpdatePlayerTurn();

            if (chess.LegalMoves(PlayerTurn).Count == 0)
            {
                if (PlayerTurn ? chess.White.IsChecked() : chess.Black.IsChecked())
                    Outcome = PlayerTurn ? Outcome.MateWhite : Outcome.MateBlack;
                else
                    Outcome = Outcome.Stalemate;
            }

            return Outcome;
        }

        private void ProcessMove(Move move)
        {

            Piece captured = null;

            switch (move.Item3) 
            {
                case SpecialMoves.None:
                    captured = move.Item1.MoveTo(move.Item2);
                    break;
                case SpecialMoves.KingSideCastle:
                    //TODO: Implement SpecialMoves
                    break;
                case SpecialMoves.QueenSideCaste:
                    break;
                case SpecialMoves.Passant:
                    break;
                case SpecialMoves.PromoteToBishop:
                    break;
                case SpecialMoves.PromoteToKnight:
                    break;
                case SpecialMoves.PromoteToRook:
                    break;
                case SpecialMoves.PromoteToQueen:
                    break;
                default:
                    break;
            }

            if (captured != null)
            {
                var playerOne = PlayerTurn ? chess.White : chess.Black;
                var plaerTwo = PlayerTurn ? chess.Black : chess.White;

                playerOne.Captured.Add(captured);
                plaerTwo.Pieces.Remove(captured);
            }
        }
    }
}
