using Godot;
using System;

public partial class EndScreenLabel : RichTextLabel
{
	public void UpdateFinalScore(int score)
	{
		this.Text = $"[center]Game Over! Final Score: {score}[/center]";
	}
	
	public void Display() 
	{
		this.Visible = true;
	} 
}
