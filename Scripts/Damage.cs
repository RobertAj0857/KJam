using Godot;
using System;

public partial class Damage : Node2D
{

	[Export]
	public Player character { get; set; } = null;
	[Export]
	public Health Health { get; set; } = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void attackHit(int damage)
	{
		if (!character.IsDead){

		}
		Health.loseHealth(damage);
	}
}
