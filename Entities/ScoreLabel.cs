using Godot;
using System;

namespace SnakeGame.Entities {
public partial class ScoreLabel : RichTextLabel
	{
		public void UpdateScore(int score)
		{
			this.Text = $"[right]Score: {score}[/right]";
		}
	}
}
