using Godot;
using System;
using System.Collections;
using SnakeGame.Data;
using System.Collections.Generic;

namespace SnakeGame.Entities
{
	public partial class SnakeHeadFragment : Godot.RigidBody2D
	{
		private int snakeSize;
		private float speed;
		private Directions? direction;
		private Queue<Directions> inputBuffer;
		private Path2D tail;
		
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			base._Ready();
			this.ContactMonitor = true;
			this.MaxContactsReported = 5;
			this.speed = 1f;
			inputBuffer = new Queue<Directions>();
			this.SetDirection(Directions.Right);
			this.tail = GetNode<Path2D>("../SnakePath");
		}
		
		public override void _Input(InputEvent @event)
		{
			base._Input(@event);
			if(@event is InputEventKey keyEvent && @event.IsActionPressed("SnakeTurn"))
			{
				while (inputBuffer.Count > 2) 
				{
					inputBuffer.Dequeue();
				}
				switch(keyEvent.Keycode)
				{
					case Key.Up:
						inputBuffer.Enqueue(Directions.Up);
						break;	 
					
					case Key.Down:
						inputBuffer.Enqueue(Directions.Down);
						break;
					case Key.Left:
						inputBuffer.Enqueue(Directions.Left);
						break;
					case Key.Right:
						inputBuffer.Enqueue(Directions.Right);
						break;
				}
			} 
		}

		private void SetDirection(Directions directions)
		{
			if (!this.direction.HasValue)
			{
				this.direction = directions;
				return;
			}
			switch (directions)
			{
				case Directions.Up:
					if (this.direction != Directions.Down)
					{ 
						this.direction = directions;
						this.GetNode<Sprite2D>("SnakeHeadSprite").Frame = 10;
					}
					break;
				case Directions.Down:
					if (this.direction != Directions.Up) 
					{
						this.direction = directions;
						this.GetNode<Sprite2D>("SnakeHeadSprite").Frame = 3;
					}
					break;
				case Directions.Right:
					if (this.direction != Directions.Left) 
					{
						this.direction = directions;
						this.GetNode<Sprite2D>("SnakeHeadSprite").Frame = 2;
					}
					break;
				case Directions.Left:
					if (this.direction != Directions.Right) 
					{
						this.direction = directions;
						this.GetNode<Sprite2D>("SnakeHeadSprite").Frame = 11;
					}
					break;
			}
		}

		public Directions? GetDirection()
		{
			return this.direction;
		}

		public void Move()
		{
			this.tail.Curve.RemovePoint(0);
			this.MoveAndCollide(this.GetMoveVector());
			this.tail.Curve.AddPoint(this.Position - GetNode<Path2D>("../SnakePath").Position);
		}

		public void ExtendSnake() {
			this.MoveAndCollide(this.GetMoveVector());
			this.tail.Curve.AddPoint(this.Position - GetNode<Path2D>("../SnakePath").Position);
		}

		private Godot.Vector2 GetMoveVector()
		{
			Sprite2D snakeSprite = GetNode<Sprite2D>("SnakeHeadSprite");
			float snakeStep = 20;
			if(inputBuffer.Count != 0) this.SetDirection(inputBuffer.Dequeue());
			switch(this.direction)
			{
				case Directions.Right:
					return new Godot.Vector2(snakeStep,0);
				case Directions.Left:
					return new Godot.Vector2(-1*snakeStep,0);
				case Directions.Down:
					return new Godot.Vector2(0,snakeStep);
				case Directions.Up:
					return new Godot.Vector2(0,-1*snakeStep);
				default:
					return new Godot.Vector2(0,0);
			}
		}
	}
}


