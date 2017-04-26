using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace Chess.Pieces {
	public class Pawn : Piece {
		public Pawn(Vector2f position, Team team) : base(position, team) { }

		public override int TextureIndex => 5;

		public override bool CanMove(List<Piece> board, Vector2f to) {
			Vector2f diff = BoardPosition - to;
			int mod = Team == Team.White ? -1 : 1;
			int homeRow = Team == Team.White ? 1 : 6;
			int count = board.Count(p => {
				return p.BoardPosition == to && p.Team != this.Team && p != this;
			});
			return ((
				count == 0 && diff.X == 0 && diff.Y == mod
			) || (
				count == 1 && Math.Abs(diff.X) == 1 && diff.Y == mod
			) || (
				count == 0 && BoardPosition.Y == homeRow && diff.X == 0 && diff.Y == (mod * 2)
			));
		}
	}
}
