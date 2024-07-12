using Godot;
using System;
using System.Collections;
using SnakeGame.Data;
using System.Collections.Generic;
using System.Numerics;

namespace SnakeGame.Entities {
	public partial class SnakeMiddleFragment : SnakeBodyFragment
	{
		private float speed;
		private Directions? direction;
		private Queue<Directions> inputBuffer;
	
	// Called when the node enters the scene tree for the first time.
	
		private void updateSprite(Directions previous, Directions current) {
			switch (previous, current)
			{
				case (Directions.Up, Directions.Right):
					this.Frame = 5;
					break;
				case (Directions.Up, Directions.Left):
					this.Frame = 6;
					break;
				case (Directions.Up, Directions.Up):
					this.Frame = 12;
					break;
				case (Directions.Down, Directions.Right):
					this.Frame = 13;
					break;
				case (Directions.Down, Directions.Left):
					this.Frame = 14;
					break;
				case (Directions.Down, Directions.Down):
					this.Frame = 12;
					break;
				case (Directions.Right, Directions.Right):
					this.Frame = 4;
					break;
				case (Directions.Right, Directions.Up):
					this.Frame = 14;
					break;
				case (Directions.Right, Directions.Down):
					this.Frame = 6;
					break;	
				case (Directions.Left, Directions.Left):
					this.Frame = 4;
					break;
				case (Directions.Left, Directions.Up):
					this.Frame = 13;
					break;
				case (Directions.Left, Directions.Down):
					this.Frame = 5;
					break;
			}
		}

		public override Directions? GetDirection()
		{
			return this.direction;
		}
		public override void SetDirection(Directions directions)
		{
			if (!this.direction.HasValue)
			{
				this.updateSprite(directions,directions);
			}
			else
			{
				this.updateSprite((Directions)this.direction,directions);
			}
			this.direction = directions;
		}

		public override void Move(Directions direction)
		{
			this.GetParent().GetParent<SnakeTrail>().Progress = this.GetParent().GetParent<SnakeTrail>().Progress;
			this.SetDirection(direction);
		}
	}
}
