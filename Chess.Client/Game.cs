using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Chess.Shared;

namespace Chess.Client {
	public class Game {
		public static Font InfoFont { get; } = new Font("Resources/SourceSansPro-Regular.ttf");
		public static Image PieceSpritesheet { get; } = new Image("Resources/Pieces.png");
		public RenderWindow Window { get; }
		public Sprite Background { get; }
		public Team CurrentTurn { get; set; }
		public ClientBoard Board { get; }

		public Game() {
			Window = new RenderWindow(new VideoMode(360, 376), "Chess.NET");
			Background = CreateBackground();
			Board = new ClientBoard();
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
			for (int i = 0; i < Board.Pieces.Count; i++) {
				if (i != selectedPiece) {
					Window.Draw(Board.Pieces[i].Sprite);
				}
			}
			if (selectedPiece != -1) {
				Window.Draw(Board.Pieces[selectedPiece].Sprite);
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
			ClientPiece selected = Board.Pieces[selectedPiece];
			Vector2f workingPosition = selected.GetWorkingBoardPosition();
			Vector2f selectedPosition = workingPosition * Piece.Size;
			bool canMove = selected.Child.CanMove(Board.AllExcept(selected), workingPosition);
			Color color = canMove ? Color.Green : Color.Red;
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
			Board.Pieces[selectedPiece].Sprite.Position = new Vector2f(e.X - Piece.Size / 2, e.Y - Piece.Size / 2);
		}

		private void OnMouseButtonRelease(object sender, MouseButtonEventArgs e) {
			if (selectedPiece != -1) {
				Vector2f newPosition = (new Vector2f(e.X, e.Y) / Piece.Size).Floor();
				ClientPiece selected = Board.Pieces[selectedPiece];
				if (selected.Child.CanMove(Board.AllExcept(selected), newPosition) && Board.CanMoveSelected()) {
					selected.Child.BoardPosition = newPosition;
					CurrentTurn = CurrentTurn == Team.White ? Team.Black : Team.White;
				}
				selected.UseBoardPosition();
			}
			selectedPiece = -1;
		}

		private void OnMouseButtonPress(object sender, MouseButtonEventArgs e) {
			Vector2f boardPosition = (new Vector2f(e.X, e.Y) / Piece.Size).Floor();
			for (int i = 0; i < Board.Pieces.Count; i++) {
				if (Board.Pieces[i].Child.BoardPosition == boardPosition) {
					if (Board.Pieces[i].Child.Team == CurrentTurn) {
						selectedPiece = i;
						return;
					}
				}
			}
		}

		private void OnClose(object sender, EventArgs e) {
			Window.Close();
		}
	}
}
