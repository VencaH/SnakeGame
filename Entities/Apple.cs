using Godot;
using System;

namespace SnakeGame.Entities {
	public partial class Apple : Area2D
	{
		public void Place(Vector2 position) {
			this.Position = position;
		}
	}
}
