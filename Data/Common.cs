using Godot;
using System;

namespace SnakeGame.Data
{
	enum Collisions
	{
		SnakeSelf,
		Food,
		Border,
		None
	}
	enum GameState
	{
		Ready,
		Start,
		Gameover
	}
	public enum Directions
	{
		Up,
		Down,
		Left,
		Right
	}
}
