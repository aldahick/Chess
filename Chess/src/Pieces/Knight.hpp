#pragma once
#include "../Piece.hpp"

class Knight : public Piece {
public:
	using Piece::Piece;
	bool canMove(sf::Vector2f position);
protected:
	int getTextureIndex();
};