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

		public override bool CanMove(Vector2f to) {
			Vector2f diff = BoardPosition - to;
			int mod = Team == Team.White ? 1 : -1;
			return (diff.X <= 1 && diff.X >= -1) && diff.Y == mod;
		}
	}
}
