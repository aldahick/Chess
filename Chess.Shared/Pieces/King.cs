using System;
using System.Collections.Generic;
using System.Linq;
using SFML.System;

namespace Chess.Shared.Pieces {
	public class King : Piece {
		public King(Vector2f position, Team team) : base(position, team) { }

		public override int TextureIndex => 0;

		public override bool CanMove(List<Piece> board, Vector2f to) {
			Vector2f diff = (BoardPosition - to).Abs();
			int count = board.Count(p => p.BoardPosition == to && p.Team == this.Team && p != this);
			return count == 0 && !(diff.X > 1 || diff.Y > 1);
		}
	}
}
