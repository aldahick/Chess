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

		public abstract bool CanMove(Vector2f to);
		public abstract int TextureIndex { get; }

		public Piece(Vector2f boardPosition, Team team) {
			Team = team;
			Sprite = SetupSprite();
			Sprite.Position = boardPosition * Size;
		}

		public Vector2f Position {
			get => Sprite.Position;
		}

		public Vector2f BoardPosition {
			get => Sprite.Position / Size;
			set {
				if (!CanMove(value)) {
					return;
				}
				Sprite.Position = value * Size;
			}
		}

		private Sprite SetupSprite() {
			Vector2i texturePosition = new Vector2i(TextureIndex * Size, Size *(int)Team);
			Texture texture = new Texture(Game.PieceSpritesheet, new IntRect(texturePosition, new Vector2i(Size, Size)));
			return new Sprite(texture);
		}

		public static Piece[] CreateStandardBoard() {
			Piece[] board = new Piece[32];
			Piece[] white = CreateStandardTeam(Team.White);
			Piece[] black = CreateStandardTeam(Team.Black);
			for (int i = 0; i < white.Length; i++) {
				board[i] = white[i];
			}
			for (int i = 0; i < black.Length; i++) {
				board[i + white.Length] = black[i];
			}
			foreach (Piece piece in board) {
				piece.SetupSprite();
			}
			return board;
		}

		private static Piece[] CreateStandardTeam(Team team) {
			int teamRow = team == Team.White ? 0 : 7;
			int pawnRow = team == Team.White ? 1 : 6;
			Piece[] board = new Piece[16];
			for (int i = 0; i < 8; i++) {
				board[i] = new Pawn(new Vector2f(i, pawnRow), team);
			}
			board[8] = new King(new Vector2f(team == Team.White ? 3 : 4, teamRow), team);
			board[9] = new Queen(new Vector2f(team == Team.White ? 4 : 3, teamRow), team);
			board[10] = new Bishop(new Vector2f(2, teamRow), team);
			board[11] = new Bishop(new Vector2f(5, teamRow), team);
			board[12] = new Knight(new Vector2f(1, teamRow), team);
			board[13] = new Knight(new Vector2f(6, teamRow), team);
			board[14] = new Rook(new Vector2f(0, teamRow), team);
			board[15] = new Rook(new Vector2f(7, teamRow), team);
			return board;
		}
	}
}
