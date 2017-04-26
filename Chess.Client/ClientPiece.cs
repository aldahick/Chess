using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Shared;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Chess.Client {
	public class ClientPiece {
		public Piece Child { get; }
		public Sprite Sprite { get; set; }

		public ClientPiece(Piece child) {
			Child = child;
			Sprite = SetupSprite();
			UseBoardPosition();
		}

		public void UseBoardPosition() {
			Sprite.Position = Child.BoardPosition * Piece.Size;
		}

		public Vector2f GetWorkingBoardPosition() {
			return (Sprite.Position / Piece.Size).Round();
		}

		private Sprite SetupSprite() {
			Vector2i texturePosition = new Vector2i(Child.TextureIndex * Piece.Size, Piece.Size * (int)Child.Team);
			Texture texture = new Texture(Game.PieceSpritesheet, new IntRect(texturePosition, new Vector2i(Piece.Size, Piece.Size)));
			return new Sprite(texture);
		}
	}
}
