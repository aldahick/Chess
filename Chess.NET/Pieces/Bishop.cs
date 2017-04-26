using System;
using System.Collections.Generic;
using System.Linq;
using SFML.System;

namespace Chess.Pieces {
	public class Bishop : Piece {
		public Bishop(Vector2f position, Team team) : base(position, team) { }

		public override int TextureIndex => 2;

		public override bool CanMove(List<Piece> board, Vector2f to) {
			return Bishop.CanMove(this, board, to);
		}

		public static bool CanMove(Piece me, List<Piece> board, Vector2f to) {
			Vector2f from = me.BoardPosition;
			Vector2f diff = from - to;
			if (Math.Abs(diff.X / diff.Y) != 1) {
				return false;
			}
			Vector2f pos = new Vector2f(from.X, from.Y);
			Func<bool> sentry;
			Action iteration;
			if (from.X > to.X && from.Y > to.Y) {
				sentry = () => pos.X >= to.X && pos.Y >= to.Y;
				iteration = () => { pos.X--; pos.Y--; };
			} else if (from.X > to.X && from.Y < to.Y) {
				sentry = () => pos.X >= to.X && pos.Y <= to.Y;
				iteration = () => { pos.X--; pos.Y++; };
			} else if (from.X < to.X && from.Y > to.Y) {
				sentry = () => pos.X <= to.X && pos.Y >= to.Y;
				iteration = () => { pos.X++; pos.Y--; };
			} else {
				sentry = () => pos.X <= to.X && pos.Y <= to.Y;
				iteration = () => { pos.X++; pos.Y++; };
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
