using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace Chess.Pieces {
	public class Knight : Piece {
		public Knight(Vector2f position, Team team) : base(position, team) { }

		public override int TextureIndex => 3;

		public override bool CanMove(List<Piece> board, Vector2f to) {
			Vector2f diff = (BoardPosition - to).Abs();
			return (diff.X == 2 && diff.Y == 1) || (diff.X == 1 && diff.Y == 2);
		}
	}
}
