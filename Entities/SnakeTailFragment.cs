using Godot;
using System;
using System.Collections;
using SnakeGame.Data;
using System.Collections.Generic;
using System.Numerics;

namespace SnakeGame.Entities {
	public partial class SnakeTailFragment : SnakeBodyFragment
	{
		private float speed;
		private Directions? direction;

		public override void _Ready()
		{
			base._Ready();
			this.SetDirection(Directions.Right);
		}
	}
}
