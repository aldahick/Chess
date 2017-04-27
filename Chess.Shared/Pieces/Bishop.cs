using System;
using System.Collections.Generic;
using System.Linq;
using SFML.System;

namespace Chess.Shared.Pieces {
	public class Bishop : Piece {
		public Bishop(Vector2f position, Team team) : base(position, team) { }

		public override int TextureIndex => 2;

		public override bool CanMove(List<Piece> board, Vector2f to) {
			return Bishop.CanMove(this, board, to);
		}

		public static bool CanMove(Piece me, List<Piece> board, Vector2f to) {
			Vector2f from = me.BoardPosition;
			Vector2f diff = from - to;
			if (Math.Abs(diff.X / diff.Y) != 1 || diff.Equals(0)) {
				return false;
			}
			Vector2f mod = new Vector2f(from.X < to.X ? 1 : -1, from.Y < to.Y ? 1 : -1);
			Vector2f pos = from.Clone();
			Func<bool> sentry = () => {
				bool xGood = diff.X < 0 ? pos.X <= to.X : pos.X >= to.X;
				bool yGood = diff.Y < 0 ? pos.Y <= to.Y : pos.Y >= to.Y;
				return xGood && yGood;
			};
			for (; sentry(); pos += mod) {
				Piece other = board.Where(p => p.BoardPosition == pos).SingleOrDefault();
				if (other == null) {
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
