#include "Piece.hpp"
#include "Pieces/Bishop.hpp"
#include "Pieces/King.hpp"
#include "Pieces/Knight.hpp"
#include "Pieces/Pawn.hpp"
#include "Pieces/Queen.hpp"
#include "Pieces/Rook.hpp"

Piece** createStandardTeam(Team team) {
	int teamRow = team == White ? 0 : 7;
	int pawnRow = team == White ? 1 : 6;
	Piece** board = new Piece*[16];
	for (int i = 0; i < 8; i++) {
		board[i] = new Pawn(sf::Vector2f(i, pawnRow), team);
	}
	board[8] = new King(sf::Vector2f(team == White ? 3 : 4, teamRow), team);
	board[9] = new Queen(sf::Vector2f(team == White ? 4 : 3, teamRow), team);
	board[10] = new Bishop(sf::Vector2f(2, teamRow), team);
	board[11] = new Bishop(sf::Vector2f(5, teamRow), team);
	board[12] = new Knight(sf::Vector2f(1, teamRow), team);
	board[13] = new Knight(sf::Vector2f(6, teamRow), team);
	board[14] = new Rook(sf::Vector2f(0, teamRow), team);
	board[15] = new Rook(sf::Vector2f(7, teamRow), team);
	return board;
}

Piece** Piece::createStandardBoard() {
	Piece** board = new Piece*[32];
	Piece** white = createStandardTeam(White);
	Piece** black = createStandardTeam(Black);
	for (int i = 0; i < 16; i++) {
		board[i] = white[i];
	}
	for (int i = 0; i < 16; i++) {
		board[i + 16] = black[i];
	}
	return board;
}
