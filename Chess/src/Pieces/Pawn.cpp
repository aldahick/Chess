#include <cstdlib>
#include "Pawn.hpp"
#include "../VectorUtils.hpp"

bool Pawn::canMove(sf::Vector2f position) {
	sf::Vector2f diff = this->getBoardPosition() - position;
	int mod = this->team == White ? 1 : -1;
	return (diff.x <= 1 && diff.x >= -1) && diff.y == mod;
}

int Pawn::getTextureIndex() {
	return 5;
}