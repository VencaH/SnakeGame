using Godot;
using System;
using SnakeGame.Entities;
using SnakeGame.Data;

namespace SnakeGame.Entities {
	public partial class Game : Node2D
	{
		private Snake snake;
		private Apple apple;
		private GameState state;
		private Area2D arena;
		private int score;
		const int width = 1152;
		const int height = 648;
		
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			GD.Randomize();
			base._Ready();
			this.apple  = GetNode<Apple>("Apple");
			this.apple.BodyEntered += this.CollisionApple;
			this.arena = this.GetNode<Area2D>("GameArena");
			this.arena.BodyExited += this.SnakeFleeing;
			this.snake = GetNode<Snake>("Snake");
			this.state = GameState.Start;
			this.score = 0;
		}
		
		public void CollisionSubscribe(Area2D target) 
		{
			target.BodyEntered += this.CollisionSnake;
		}	
		
		private void SnakeFleeing(Node body)
		{
			this.state = GameState.Gameover;
			this.GetNode<Timer>("Snake/SnakeTick").Stop();
			this.GetNode<EndScreenLabel>("EndScreenLabel").UpdateFinalScore(this.score);
			this.GetNode<EndScreenLabel>("EndScreenLabel").Display();
		}

		private void CollisionSnake(Node body)
		{
			this.state = GameState.Gameover;
			this.GetNode<Timer>("Snake/SnakeTick").Stop();
			this.GetNode<EndScreenLabel>("EndScreenLabel").UpdateFinalScore(this.score);
			this.GetNode<EndScreenLabel>("EndScreenLabel").Display();
		}
		
		private void CollisionApple(Node body)
		{
			this.snake.Eat();
			this.score++;
			GetNode<ScoreLabel>("ScoreLabel").UpdateScore(this.score);
			float appleX =(float)Math.Round((decimal)((GD.Randf() * (width - 40f) + 20f)/20))*20;
			float appleY =(float)Math.Round((decimal)((GD.Randf() * (height - 40f) + 20f)/20))*20;
			this.apple.Place(
				new Vector2(
					appleX,
					appleY
				)
			);
		}
	}
}
