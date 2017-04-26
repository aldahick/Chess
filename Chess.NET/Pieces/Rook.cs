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
			Vector2f pos = new Vector2f(from.X, from.Y);
			Func<bool> sentry;
			Action iteration;
			if (from.X > to.X) {
				sentry = () => pos.X >= to.X;
				iteration = () => { pos.X--; };
			} else if (from.X < to.X) {
				sentry = () => pos.X <= to.X;
				iteration = () => { pos.X++; };
			} else if (from.Y < to.Y) {
				sentry = () => pos.Y <= to.Y;
				iteration = () => { pos.Y++; };
			} else {
				sentry = () => pos.Y >= to.Y;
				iteration = () => { pos.Y--; };
			}
			for (; sentry(); iteration()) {
				Piece other = board.Where(p => p.BoardPosition == pos).SingleOrDefault();
				if (other != default(Piece)) {
					if (other.BoardPosition == to && other.Team != me.Team) {
						continue; // target position has an opposing piece
					}
					return false;
				}
			}
			return true;
		}
	}
}
