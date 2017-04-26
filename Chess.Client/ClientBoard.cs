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
			Vector2f workingPosition = Pieces[SelectedPiece].GetWorkingBoardPosition();
			Piece selected = Pieces[SelectedPiece].Child;
			ClientPiece other = Pieces.Where(p => {
				return workingPosition == p.Child.BoardPosition && p != Pieces[SelectedPiece];
			}).SingleOrDefault();
			if (other == null) {
				return true;
			}
			if (other.Child.Team == selected.Team) {
				return false;
			} else { // TODO: Handle check(mate)
				Pieces.Remove(other);
				return true;
			}
		}

		public List<Piece> AllExcept(ClientPiece piece) {
			return Pieces.Except(new[] { piece }).Select(cp => cp.Child).ToList();
		}
	}
}
