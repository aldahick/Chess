using System;
using System.Collections.Generic;
using System.Linq;
using SFML.System;

namespace Chess.Pieces {
	public class King : Piece {
		public King(Vector2f position, Team team) : base(position, team) { }

		public override int TextureIndex => 0;

		public override bool CanMove(Vector2f to) {
			Vector2f diff = (BoardPosition - to).Abs();
			return !(diff.X > 1 || diff.Y > 1);
		}
	}
}
