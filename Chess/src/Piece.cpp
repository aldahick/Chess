#include "Piece.hpp"
#include "Game.hpp"

//Piece::Piece(sf::Vector2f position, Team team)
//	: team(team) {
//	this->setupSprite(position);
//}

bool Piece::setPosition(sf::Vector2f position) {
	if (!this->canMove(position)) {
		return false;
	}
	this->sprite.setPosition(position * PIECE_SIZE);
	return true;
}

void Piece::setupSprite(sf::Vector2f initialPosition) {
	sf::Texture texture = sf::Texture();
	sf::Vector2i texturePosition = sf::Vector2i(this->getTextureIndex(), this->team == White ? 0 : 1) * (int)PIECE_SIZE;
	texture.loadFromImage(*game->getPiecesSprite(), sf::IntRect(texturePosition.x, texturePosition.y, PIECE_SIZE, PIECE_SIZE));
	this->sprite = sf::Sprite(texture);
	this->sprite.setPosition(initialPosition);
}

sf::Vector2f Piece::getBoardPosition() {
	return this->sprite.getPosition() / PIECE_SIZE;
}

sf::Sprite* Piece::getSprite() {
	return &this->sprite;
}