using Godot;
using System;

public partial class Damage : Node2D
{
	[Export]
	public int amount { get; set; } = 10;

	[Export]
	public Player character { get; set; } = null;
	[Export]
	public Health Health { get; set; } = null;

	[Export]
	public Key Attack { get; set; } = Key.F;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Attack))
		{
			attackHit();
		}
	}

	public void attackHit()
	{
		Health.loseHealth();
		/*
		if (character.hit true)
		{
			Health.loseHealth();
		} 
		*/
	}
}
