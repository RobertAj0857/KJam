using Godot;
using System;

public partial class Projectile : Area2D
{
	[Export]
	public float Speed {get; set;} = 1;
	[Export]
	public int Damage {get; set;} = 10;
	[Export]
	public int LifeTime {get; set;} = 2;
	[Export]
	public bool Team1 {get; set;} = true;
	private Vector2 velocity = Vector2.Zero;
	private Vector2 direction;
	[Export]
	public Vector2 Direction {get=>direction; set{
		direction = value;
		velocity = direction * Speed;
	}}
	private bool spawned = false;
	private double timePassed = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Scale = Vector2.Zero;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(!spawned) {
			spawned = true;
			Visible = true;
		}
	}

	public void Destroy() {
		QueueFree();
	}

    public override void _PhysicsProcess(double delta)
    {

		timePassed += delta;
		if(timePassed < 0.1) {
			Scale = Vector2.One * (float)(timePassed / 0.25);
		} else if(Scale != Vector2.One){
			Scale = Vector2.One;
		}
		if(timePassed >= LifeTime){
			Destroy();
			return;
		}
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
				box.damageComponent.attackHit(Damage);
				Destroy();
			}
		}
    }
}
