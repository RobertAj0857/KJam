using Godot;
using System;

public partial class ExplodingBarrel : Area2D
{
	[Export]
	public int Damage {get; set;} = 25;
	[Export]
	public double Cooldown {get; set;} = 1;
	[Export]
	public double ExplosionDuration {get; set;} = 0.25;
	[Export]
	public int Range {get; set;} = 56;
	private double timePassed = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	public void Destroy() {
		QueueFree();
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timePassed += delta;
		if(timePassed >= Cooldown){
			Explosion explosion = (Explosion) GD.Load<PackedScene>("res://Scenes/explosion.tscn").Instantiate();
			explosion.Damage = Damage;
			explosion.ExplosionRange = Range;
			explosion.ExplosionDuration = ExplosionDuration;
			explosion.Position = Position;
			GetParent().AddChild(explosion);
			Destroy();
		}
	}
}
