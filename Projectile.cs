using Godot;
using System;

public partial class Projectile : Area2D
{
	[Export]
	public float Speed {get; set;} = 1;
	[Export]
	public float Damage {get; set;} = 10;
	[Export]
	public bool Team1 {get; set;} = true;

	private Vector2 velocity = Vector2.Zero;
	private Vector2 direction;
	[Export]
	public Vector2 Direction {get=>direction; set{
		direction = value;
		velocity = direction * Speed;
	}}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Destroy() {
		QueueFree();
	}

    public override void _PhysicsProcess(double delta)
    {
		// move projectile
        GlobalPosition += velocity * (float)delta * 100;
		// checks for walls
		if(GetOverlappingBodies().Count > 0) {
			// destroy if wall hit
			Destroy();
		}
		// checks for players
		Godot.Collections.Array<Area2D> hits = GetOverlappingAreas();
		foreach(Area2D hit in hits) {
			if(hit is Hitbox box && box.Team1 != Team1) {
				// do damage
				Destroy();
			}
		}
    }
}
