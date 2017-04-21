#include "Game.hpp"

Game::Game() { }

Game::~Game() {
	for (int i = 0; i < 32; i++) {
		delete(this->pieces[i]);
	}
	delete[](this->pieces);
}

void Game::start() {
	this->setup();
	while (this->window.isOpen()) {
		this->update();
		this->render();
	}
}

sf::Image createBackground(void);
void Game::setup() {
	this->window.create(sf::VideoMode(800, 600), "Chess");
	this->backgroundImage = createBackground();
	this->piecesSprite = sf::Image();
	this->piecesSprite.loadFromFile("resources/pieces.png");
	this->pieces = Piece::createStandardBoard();
}

void Game::render() {
	this->window.clear(sf::Color::Black);
	for (int i = 0; i < 32; i++) {
		this->window.draw(*this->pieces[i]->getSprite());
	}
	this->window.display();
}

void Game::update() {
	sf::Event event;
	while (this->window.pollEvent(event)) {
		this->handleEvent(event);
	}
}

void Game::handleEvent(sf::Event event) {
	if (event.type == sf::Event::Closed) {
		this->window.close();
	}
}

sf::Image* Game::getPiecesSprite() {
	return &this->piecesSprite;
}

sf::RenderWindow* Game::getWindow() {
	return &this->window;
}

sf::Image createBackground() {
	sf::Image image;
	image.create(PIECE_SIZE * 8, PIECE_SIZE * 8);
	sf::Color onColor = sf::Color(0xEE, 0xEE, 0xD2);
	sf::Color offColor = sf::Color(0xBA, 0xCA, 0x44);
	for (int x = 0; x < 8; x++) {
		for (int y = 0; y < 8; y++) {
			sf::Color color = (x % 2 == y % 2) ? onColor : offColor;
			for (int xi = x * PIECE_SIZE; xi < y * PIECE_SIZE + PIECE_SIZE; xi++) {
				for (int yi = y * PIECE_SIZE; yi < y * PIECE_SIZE + PIECE_SIZE; yi++) {
					image.setPixel(xi, yi, color);
				}
			}
		}
	}
	return image;
}