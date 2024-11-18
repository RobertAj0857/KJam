using Godot;
using System;

public partial class MagicCannonBall : Projectile
{

	public Player Player;
	private Vector2 velocity = Vector2.Zero;
	private Vector2 direction;
	private double timePassed = 0;
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

    public override void _PhysicsProcess(double delta)
    {
		timePassed += delta;
		if(timePassed >= LifeTime){
			Destroy();
			return;
		}
		// move projectile
        GlobalPosition += velocity * (float)delta * 100;
		// checks for walls
		if(GetOverlappingBodies().Count > 0) {
			// destroy if wall hit
			Player.GlobalPosition = Player.precisePosition;
			Player.Position = Position - direction * 10;
			Player.precisePosition = Player.GlobalPosition;
			Destroy();
		}
		// checks for players
		Godot.Collections.Array<Area2D> hits = GetOverlappingAreas();
		foreach(Area2D hit in hits) {
			if(hit is Hitbox box && box.Team1 != Team1) {
				box.damageComponent.attackHit(Damage);
				Player.GlobalPosition = Player.precisePosition;
				Player.Position = Position - direction * 10;
				Player.precisePosition = Player.GlobalPosition;
				Destroy();
			}
		}
    }
}
