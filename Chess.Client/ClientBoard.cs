using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;
using Chess.Shared;

namespace Chess.Client {
	public class ClientBoard : Board {
		public new List<ClientPiece> Pieces { get; set; }

		public ClientBoard() {
			Pieces = new List<ClientPiece>();
			base.Pieces.ForEach(p => {
				Pieces.Add(new ClientPiece(p));
			});
		}

		public new bool CanMoveSelected() {
			return CanMoveSelected(Pieces[selectedPiece].GetWorkingBoardPosition());
		}

		public List<Piece> AllExcept(ClientPiece piece) {
			return Pieces.Except(new[] { piece }).Select(cp => cp.Child).ToList();
		}
	}
}
