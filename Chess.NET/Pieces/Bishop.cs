using System;
using System.Collections.Generic;
using System.Linq;
using SFML.System;

namespace Chess.Pieces {
	public class Bishop : Piece {
		public Bishop(Vector2f position, Team team) : base(position, team) { }

		public override int TextureIndex => 2;

		public override bool CanMove(Vector2f to) {
			return Bishop.CanMove(BoardPosition, to);
		}

		public static bool CanMove(Vector2f from, Vector2f to) {
			Vector2f diff = from - to;
			return Math.Abs(diff.X / diff.Y) == 1;
		}
	}
}
