using Mate.Abstractions;
using Mate.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mate.Pieces
{
    public class Rook : Piece
    {
        public Rook(bool color, Position position = null) : base(color, position) { }

        public Rook(Player player, Position position = null) : base(player, position) { }

        public override HashSet<Position> AttackedSquares()
        {
            var positions = new HashSet<Position>();

            if (this.IsOnBoard())
                return positions;


            positions.AddNullPosition(this.UpdateAttackersFrom(this.GetSquare().MoveThrough<Files>(1)));


            return positions;
        }

        public override bool MoveTo(Position position)
        {
            throw new NotImplementedException();
        }

        private HashSet<Position> MoveUp(int numberOfSquares = 1)
        {
            var positions = new HashSet<Position>();

            //TODO: Not so simple. If is occupied by opposing piece, it has to stop!
            if (
                positions.AddNullPosition(this.UpdateAttackersFrom(this.GetSquare().MoveThrough<Files>(numberOfSquares)))       || 
                positions.AddNullPosition(this.UpdateAttackersFrom(this.GetSquare().MoveThrough<Files>(-numberOfSquares)))      ||
                positions.AddNullPosition(this.UpdateAttackersFrom(this.GetSquare().MoveThrough<Ranks>(numberOfSquares)))       ||
                positions.AddNullPosition(this.UpdateAttackersFrom(this.GetSquare().MoveThrough<Ranks>(-numberOfSquares))) 
                )
            {
                positions.UnionWith(MoveUp(++numberOfSquares));
            }



            return positions;

        }
    }
}
