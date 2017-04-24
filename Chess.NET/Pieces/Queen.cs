using System;
using System.Collections.Generic;
using System.Linq;
using SFML.System;

namespace Chess.Pieces {
	public class Queen : Piece {
		public Queen(Vector2f position, Team team) : base(position, team) { }

		public override int TextureIndex => 1;

		public override bool CanMove(List<Piece> board, Vector2f to) {
			return Bishop.CanMove(this, board, to) || Rook.CanMove(this, board, to);
		}
	}
}
