#pragma once
#include "../Piece.hpp"

class Bishop : public Piece {
public:
	using Piece::Piece;
	bool canMove(sf::Vector2f position);
	static bool canMove(sf::Vector2f from, sf::Vector2f to);
protected:
	int getTextureIndex();
};