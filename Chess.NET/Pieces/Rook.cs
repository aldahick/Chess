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
			Vector2f diff = from - to;
			if ((diff.X != 0 && diff.Y != 0) || (diff.X == 0 && diff.Y == 0)) {
				return false;
			}
			bool useX = diff.X != 0;
			float diffComponent = useX ? diff.X : diff.Y;
			float fromComponent = useX ? from.X : from.Y;
			float toComponent = useX ? to.X : to.Y;
			float mod = fromComponent < toComponent ? 1 : -1;
			for (float c = fromComponent; c <= toComponent; c += mod) {
				Vector2f pos = new Vector2f() {
					X = useX ? c : from.X,
					Y = useX ? from.Y : c
				};
				Piece other = board.Where(p => p.BoardPosition == pos).SingleOrDefault();
				if (other == default(Piece)) {
					continue;
				}
				if (other.BoardPosition == to && other.Team != me.Team) {
					continue;
				}
				return false;
			}
			return true;
		}
	}
}
