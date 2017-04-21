#include <cstdlib>
#include "Queen.hpp"
#include "Bishop.hpp"
#include "Rook.hpp"

bool Queen::canMove(sf::Vector2f position) {
	sf::Vector2f current = this->getBoardPosition();
	return Bishop::canMove(current, position) || Rook::canMove(current, position);
}

int Queen::getTextureIndex() {
	return 1;
}