#pragma once
#include <SFML/System.hpp>

sf::Vector2f abs(sf::Vector2f vector) {
	return sf::Vector2f(abs(vector.x), abs(vector.y));
}