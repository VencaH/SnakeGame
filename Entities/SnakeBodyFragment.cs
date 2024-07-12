using Godot;
using System;
using System.Collections;
using SnakeGame.Data;
using System.Collections.Generic;
using System.Numerics;

namespace SnakeGame.Entities {
public partial class SnakeBodyFragment : Sprite2D
{
	private float speed;
	private Directions? direction;

	public virtual void SetDirection(Directions directions)
	{
		this.direction = directions;
	}

	public virtual Directions? GetDirection()
	{
		return this.direction;
	}

	public virtual void Move(Directions _)
	{
	// Without explicit Progress change, Godot does not updates position of the object,
	// so snake body will be stuck otherwise.
		this.GetParent().GetParent<SnakeTrail>().Progress = this.GetParent().GetParent<SnakeTrail>().Progress;
	}
}		
}
