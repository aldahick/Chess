#pragma once
#include <SFML/Graphics.hpp>
#define PIECE_SIZE 45.0f

enum Team {
	White, Black
};

class Piece {
public:
	Piece(sf::Vector2f position, Team team)
		: team(team) {
		this->setupSprite(position);
	}

	bool setPosition(sf::Vector2f position);
	sf::Vector2f getBoardPosition();
	sf::Sprite* getSprite();

	virtual bool canMove(sf::Vector2f position) = 0;

	static Piece** createStandardBoard();
protected:
	virtual int getTextureIndex() = 0;

	void setupSprite(sf::Vector2f initialPosition);

	Team team;
	sf::Sprite sprite;
};
