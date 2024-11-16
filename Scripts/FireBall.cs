using Godot;
using System;

public partial class FireBall : Projectile
{
	[Export]
	private Sprite2D sprite;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Direction != Vector2.Zero) {
			float dir = Direction.Angle();
			GlobalRotation = dir;
			sprite.GlobalRotation = 0;
		}
	}
}
