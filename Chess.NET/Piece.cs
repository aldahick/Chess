using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;
using Chess.Pieces;

namespace Chess {
	public abstract class Piece {
		public const int Size = 45;
		public Sprite Sprite { get; }
		public Team Team { get; }
		public Vector2f BoardPosition { get; set; }
		public bool IsCaptured { get; set; }

		/**
		 * <summary>Returns true if the piece can move to the specified position.</summary>
		 * <remarks>
		 * Should not modify the board at all.
		 * Returns true if there is an opposing piece in the target position.
		 * </remarks>
		 */
		public abstract bool CanMove(List<Piece> board, Vector2f to);
		public abstract int TextureIndex { get; }

		public Piece(Vector2f boardPosition, Team team) {
			Team = team;
			Sprite = SetupSprite();
			BoardPosition = boardPosition;
			UseBoardPosition();
		}

		public Vector2f Position {
			get => Sprite.Position;
			set => Sprite.Position = value;
		}

		public void UseBoardPosition() {
			this.Sprite.Position = BoardPosition * Size;
		}

		public Vector2f GetWorkingBoardPosition() {
			return (this.Sprite.Position / Size).Round();
		}

		private Sprite SetupSprite() {
			Vector2i texturePosition = new Vector2i(TextureIndex * Size, Size * (int)Team);
			Texture texture = new Texture(Game.PieceSpritesheet, new IntRect(texturePosition, new Vector2i(Size, Size)));
			return new Sprite(texture);
		}

		public static List<Piece> CreateStandardBoard() {
			List<Piece> board = new List<Piece>();
			board.AddRange(CreateStandardTeam(Team.White));
			board.AddRange(CreateStandardTeam(Team.Black));
			foreach (Piece piece in board) {
				piece.SetupSprite();
			}
			return board;
		}

		private static List<Piece> CreateStandardTeam(Team team) {
			int teamRow = team == Team.White ? 0 : 7;
			int pawnRow = team == Team.White ? 1 : 6;
			List<Piece> board = new List<Piece>();
			for (int i = 0; i < 8; i++) {
				board.Add(new Pawn(new Vector2f(i, pawnRow), team));
			}
			board.Add(new King(new Vector2f(team == Team.White ? 3 : 4, teamRow), team));
			board.Add(new Queen(new Vector2f(team == Team.White ? 4 : 3, teamRow), team));
			board.Add(new Bishop(new Vector2f(2, teamRow), team));
			board.Add(new Bishop(new Vector2f(5, teamRow), team));
			board.Add(new Knight(new Vector2f(1, teamRow), team));
			board.Add(new Knight(new Vector2f(6, teamRow), team));
			board.Add(new Rook(new Vector2f(0, teamRow), team));
			board.Add(new Rook(new Vector2f(7, teamRow), team));
			return board;
		}
	}
}
