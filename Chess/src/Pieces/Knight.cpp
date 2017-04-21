#include <cstdlib>
#include "Knight.hpp"
#include "../VectorUtils.hpp"

bool Knight::canMove(sf::Vector2f position) {
	sf::Vector2f diff = abs(this->getBoardPosition() - position);
	return (diff.x == 2 && diff.y == 1) || (diff.x == 1 && diff.y == 2);
}

int Knight::getTextureIndex() {
	return 3;
}