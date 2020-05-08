using Mate.Extensions;
using Mate.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Mate.Pieces;

namespace Mate
{
    class Game
    {
        private readonly Chess chess = new Chess();

        public int CurrentMove { get; private set; } = 1;

        public IReadOnlyCollection<Piece> WhitePieces { get => chess.White.Pieces; }

        public IReadOnlyCollection<Piece> BlackPieces { get => chess.Black.Pieces; }

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
            customInitialization(customPieces);
        

        private void customInitialization(CustomPieceInput customPieces)
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
    }
}
