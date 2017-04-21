#include <cstdlib>
#include "Bishop.hpp"

bool Bishop::canMove(sf::Vector2f position) {
	return Bishop::canMove(this->sprite.getPosition(), position);
}

bool Bishop::canMove(sf::Vector2f from, sf::Vector2f to) {
	sf::Vector2f diff = from - to;
	return abs(diff.x / diff.y) == 1;
}

int Bishop::getTextureIndex() {
	return 2;
}