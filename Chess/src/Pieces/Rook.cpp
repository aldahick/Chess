#include <cstdlib>
#include "Rook.hpp"
#include "../VectorUtils.hpp"

bool Rook::canMove(sf::Vector2f position) {
	return Rook::canMove(this->getBoardPosition(), position);
}

bool Rook::canMove(sf::Vector2f from, sf::Vector2f to) {
	sf::Vector2f diff = abs(from - to);
	return (diff.x > 0 && diff.y == 0) || (diff.x == 0 && diff.y > 0);
}

int Rook::getTextureIndex() {
	return 4;
}