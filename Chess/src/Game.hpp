#pragma once
#include <SFML/Graphics.hpp>
#include <SFML/Window.hpp>
#include "Piece.hpp"

class Game {
public:
	Game();
	~Game();
	void start();
	sf::RenderWindow* getWindow();
	sf::Image* getPiecesSprite();
private:
	void setup();
	void render();
	void update();
	void handleEvent(sf::Event);
	sf::RenderWindow window;
	sf::Image piecesSprite;
	sf::Image backgroundImage;
	Piece** pieces;
};

Game* game = NULL;