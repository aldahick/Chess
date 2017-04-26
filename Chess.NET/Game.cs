using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Chess {
	public class Game {
		public static Font InfoFont { get; } = new Font("Resources/SourceSansPro-Regular.ttf");
		public static Image PieceSpritesheet { get; } = new Image("Resources/Pieces.png");
		public RenderWindow Window { get; }
		public Sprite Background { get; }
		public List<Piece> Pieces { get; }
		public Team CurrentTurn { get; set; } = Team.White;

		public Game() {
			Window = new RenderWindow(new VideoMode(360, 376), "Chess.NET");
			Background = CreateBackground();
			Pieces = Piece.CreateStandardBoard();
		}

		public void Start() {
			RegisterEventHandlers();
			while (Window.IsOpen) {
				Window.DispatchEvents();
				Render();
			}
		}

		private void Render() {
			Window.Clear(Color.Black);
			Window.Draw(Background);
			for (int i = 0; i < Pieces.Count; i++) {
				if (i != selectedPiece) {
					Window.Draw(Pieces[i].Sprite);
				}
			}
			if (selectedPiece != -1) {
				Window.Draw(Pieces[selectedPiece].Sprite);
			}
			if (selectedPiece != -1) {
				Window.Draw(GetPieceHighlight(), PrimitiveType.Lines);
			}
			Window.Draw(new Text($"It is currently {CurrentTurn.ToString()}'s turn.", InfoFont, 16) {
				Position = new Vector2f(0, 360)
			});
			Window.Display();
		}

		private Sprite CreateBackground() {
			Image image = new Image(Piece.Size * 8, Piece.Size * 8);
			Color onColor = new Color(0xEE, 0xEE, 0xD2);
			Color offColor = new Color(0xBA, 0xCA, 0x44);
			for (int x = 0; x < 8; x++) {
				for (int y = 0; y < 8; y++) {
					Color color = (x % 2 == y % 2) ? onColor : offColor;
					for (int xi = x * Piece.Size; xi < x * Piece.Size + Piece.Size; xi++) {
						for (int yi = y * Piece.Size; yi < y * Piece.Size + Piece.Size; yi++) {
							image.SetPixel((uint)xi, (uint)yi, color);
						}
					}
				}
			}
			Sprite sprite = new Sprite(new Texture(image)) {
				Position = new Vector2f(0, 0)
			};
			return sprite;
		}

		private void RegisterEventHandlers() {
			Window.Closed += this.OnClose;
			Window.MouseButtonPressed += this.OnMouseButtonPress;
			Window.MouseButtonReleased += this.OnMouseButtonRelease;
			Window.MouseMoved += this.OnMouseMove;
		}
		
		private int selectedPiece = -1;

		private Vertex[] GetPieceHighlight() {
			Vertex[] arr = new Vertex[8];
			Piece selected = Pieces[selectedPiece];
			Vector2f workingPosition = selected.GetWorkingBoardPosition();
			Vector2f selectedPosition = workingPosition * Piece.Size;
			Color color = selected.CanMove(Pieces, workingPosition) ? Color.Green : Color.Red;
			arr[0] = new Vertex(selectedPosition, color);
			arr[1] = new Vertex(selectedPosition + new Vector2f(0, Piece.Size), color);
			arr[2] = new Vertex(selectedPosition + new Vector2f(Piece.Size, Piece.Size), color);
			arr[3] = new Vertex(selectedPosition + new Vector2f(Piece.Size, 0), color);
			arr[4] = arr[0];
			arr[5] = arr[3];
			arr[6] = arr[1];
			arr[7] = arr[2];
			return arr;
		}

		private void OnMouseMove(object sender, MouseMoveEventArgs e) {
			if (selectedPiece == -1) {
				return;
			}
			Pieces[selectedPiece].Sprite.Position = new Vector2f(e.X - Piece.Size / 2, e.Y - Piece.Size / 2);
		}

		private void OnMouseButtonRelease(object sender, MouseButtonEventArgs e) {
			if (selectedPiece != -1) {
				Vector2f newPosition = (new Vector2f(e.X, e.Y) / Piece.Size).Floor();
				Piece selected = Pieces[selectedPiece];
				if (selected.CanMove(Pieces.Except(new[] { selected }).ToList(), newPosition) && CanMoveSelected()) {
					selected.BoardPosition = newPosition;
					CurrentTurn = CurrentTurn == Team.White ? Team.Black : Team.White;
				}
				selected.UseBoardPosition();
			}
			selectedPiece = -1;
		}

		private void OnMouseButtonPress(object sender, MouseButtonEventArgs e) {
			Vector2f boardPosition = (new Vector2f(e.X, e.Y) / Piece.Size).Floor();
			for (int i = 0; i < Pieces.Count; i++) {
				if (Pieces[i].BoardPosition == boardPosition) {
					if (Pieces[i].Team == CurrentTurn) {
						selectedPiece = i;
						return;
					}
				}
			}
		}

		/**
		 * <summary>Removes opposing piece from target position and returns true, or returns false if the piece is friendly.</summary>
		 * <remarks>Also returns true if there is no piece at the target position.</remarks>
		 */
		public bool CanMoveSelected() {
			Piece selected = Pieces[selectedPiece];
			Vector2f workingPosition = selected.GetWorkingBoardPosition();
			Piece other = Pieces.Where(p => {
				return workingPosition == p.BoardPosition && p != Pieces[selectedPiece];
			}).SingleOrDefault();
			if (other == default(Piece)) {
				return true;
			}
			if (other.Team == selected.Team) {
				return false;
			} else { // TODO: Handle check(mate)
				Pieces.Remove(other);
				return true;
			}
		}

		private void OnClose(object sender, EventArgs e) {
			Window.Close();
		}
	}
}
