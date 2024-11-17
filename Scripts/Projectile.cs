using Godot;
using System;

public partial class Projectile : Area2D
{
	[Export]
	public bool isCannonBall = false;
	[Export]
	private Texture2D destroyedTex;
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
		if(isCannonBall && !destroyed) {
			destroyed = true;
			GetNode<Sprite2D>("Sprite2D").Texture = destroyedTex;
			return;
		}
		QueueFree();
	}

	private double stayDelta = 0.0;
	private double stayTime = 0.1;
	protected double timeToGrow = 0.1;
	private bool destroyed = false;

    public override void _PhysicsProcess(double delta)
    {
		if(destroyed) {
			stayDelta+=delta;
			if(stayDelta >= stayTime) {
				QueueFree();
			}
			return;
		}
		timePassed += delta;
		if(timePassed < timeToGrow) {
			Scale = Vector2.One * (float)(Math.Pow(timePassed / timeToGrow, 1));
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
			if(this is FireBall){
				Explosion explosion = (Explosion) GD.Load<PackedScene>("res://Scenes/explosion.tscn").Instantiate();
				explosion.Damage = Damage;
				explosion.ExplosionRange = 32;
				explosion.ExplosionDuration = 0.4;
				explosion.Position = Position;
				GetParent().AddChild(explosion);
			}
			Destroy();
		}
		// checks for players
		Godot.Collections.Array<Area2D> hits = GetOverlappingAreas();
		foreach(Area2D hit in hits) {
			if(hit is Hitbox box && box.Team1 != Team1) {
				((Player) box.GetParent()).Velocity += Direction * 400;
				if(this is FireBall){
					Explosion explosion = (Explosion) GD.Load<PackedScene>("res://Scenes/explosion.tscn").Instantiate();
					explosion.Damage = Damage;
					explosion.ExplosionRange = 32;
					explosion.ExplosionDuration = 0.4;
					explosion.Position = Position;
					GetParent().AddChild(explosion);
				}else{
					box.damageComponent.attackHit(Damage);
				}
				Destroy();
			}
		}
    }
}
