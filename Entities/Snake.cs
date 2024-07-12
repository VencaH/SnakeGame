using Godot;
using SnakeGame.Data;
using SnakeGame.Entities;
using System;
using System.Collections.Generic;

namespace SnakeGame.Entities {
	public partial class Snake : Node2D
	{
		private SnakeHeadFragment head;
		private Path2D snakePath; 
		private PackedScene snakeFragmentPack;
		private List<SnakeTrail> body;
		
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			this.head = GetNode<SnakeHeadFragment>("SnakeHeadFragment");
			GetNode<Timer>("SnakeTick").Timeout += this.Move;
			this.snakePath = GetNode<Path2D>("SnakePath");
			this.body = new List<SnakeTrail>{this.GetNode<SnakeTrail>("SnakePath/SnakeTail")};
		}

		private void Move()
		{
			this.head.Move();
			this.updateBody();
		}

		private void updateBody (){
			for (int i = 0; i < this.body.Count; i++)
			{
				if (i==this.body.Count-1) {
					this.body[i]
					.GetNode<SnakeBodyFragment>("Area2D/SnakeBodySprite")
					.Move((Directions)this.head.GetDirection());
				}
				else {
					this.body[i]
					.GetNode<SnakeBodyFragment>("Area2D/SnakeBodySprite")
					.Move(
						(Directions)this.body[i+1]
						.GetNode<SnakeBodyFragment>("Area2D/SnakeBodySprite")
						.GetDirection()
						);
				}
			}
		}

		public void Eat()
		{
			this.head.ExtendSnake();
			PackedScene snakeFragmentPack = GD.Load<PackedScene>("res://Scenes/SnakeBodyFragment.tscn");
			SnakeTrail newTrail = snakeFragmentPack.Instantiate<SnakeTrail>();
			newTrail.Progress = 20 * (this.body.Count);
			newTrail.GetNode<SnakeMiddleFragment>("Area2D/SnakeBodySprite").SetDirection((Directions)this.head.GetDirection());
			this.body.Add(newTrail);
			this.GetParent<Game>().CollisionSubscribe(newTrail.GetNode<Area2D>("Area2D"));
			this.snakePath.CallDeferred("add_child",newTrail);	
		}
	}
}
