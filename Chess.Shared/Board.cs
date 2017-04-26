using System;
using System.Collections.Generic;
using System.Linq;
using SFML.System;

namespace Chess.Shared {
	public class Board {
		public List<Piece> Pieces { get; }
		public Team CurrentTurn { get; set; } = Team.White;
		public int SelectedPiece { get; set; } = -1;

		public Board() {
			Pieces = Piece.CreateStandardBoard();
		}

		/**
		 * <summary>Removes opposing piece from target position and returns true, or returns false if the piece is friendly.</summary>
		 * <remarks>Also returns true if there is no piece at the target position.</remarks>
		 */
		public bool CanMoveSelected() {
			Vector2f workingPosition = Pieces[SelectedPiece].BoardPosition;
			Piece selected = Pieces[SelectedPiece];
			Piece other = Pieces.Where(p => {
				return workingPosition == p.BoardPosition && p != Pieces[SelectedPiece];
			}).SingleOrDefault();
			return CanMoveSelected(selected, other, workingPosition);
		}

		public bool CanMoveSelected(Piece selected, Piece other, Vector2f workingPosition) {
			if (other == null) {
				return true;
			}
			if (other.Team == selected.Team) {
				return false;
			} else { // TODO: Handle check(mate)
				Pieces.Remove(other);
				return true;
			}
		}
	}
}
