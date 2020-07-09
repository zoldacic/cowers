using Godot;
using System;

public class Player : Area2D
{
	[Export]
	public int Speed = 400;

	private Vector2 _screenSize; 

	public override void _Ready()
	{
		_screenSize = GetViewport().Size;
	}

	public override void _Process(float delta) {
		var velocity = new Vector2();

		CheckKey("ui_right", ref velocity.x, 1);
		CheckKey("ui_left", ref velocity.x, -1);
		CheckKey("ui_down", ref velocity.y, 1);
		CheckKey("ui_up", ref velocity.y, -1);

		var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite.Play();
		}
		else
		{
			animatedSprite.Stop();
		}

		Position += velocity * delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.x, 0, _screenSize.x),
			y: Mathf.Clamp(Position.y, 0, _screenSize.y)
		);
	}  
	
	private void CheckKey(string key, ref float coord, int value)
	{
		if (Input.IsActionPressed(key))
		{
			coord += value;
		}
	}
}
