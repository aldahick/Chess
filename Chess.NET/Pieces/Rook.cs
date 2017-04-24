using System;
using System.Collections.Generic;
using System.Linq;
using SFML.System;

namespace Chess.Pieces {
	public class Rook : Piece {
		public Rook(Vector2f position, Team team) : base(position, team) { }

		public override int TextureIndex => 4;

		public override bool CanMove(List<Piece> board, Vector2f to) {
			return CanMove(this, board, to);
		}

		public static bool CanMove(Piece me, List<Piece> board, Vector2f to) {
			Vector2f from = me.BoardPosition;
			Vector2f diff = (from - to).Abs();
			return (diff.X > 0 && diff.Y == 0) || (diff.X == 0 && diff.Y > 0);
		}
	}
}
