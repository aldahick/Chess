#include <cstdlib>
#include "King.hpp"
#include "../VectorUtils.hpp"

bool King::canMove(sf::Vector2f position) {
	sf::Vector2f diff = abs(this->getBoardPosition() - position);
	return !(diff.x > 1 || diff.y > 1);
}

int King::getTextureIndex() {
	return 0;
}