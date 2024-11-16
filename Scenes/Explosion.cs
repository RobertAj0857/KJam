using Godot;
using System;

public partial class Explosion : Area2D
{
	[Export]
	Sprite2D effect;
	public int Damage {get; set;} = 5;//Set damage for explosion
	public int ExplosionRange = 10;
	public double ExplosionDuration = 1.5;
	private double timePassed = 0;
	private bool exploded = false;
	[Export]
	public CollisionShape2D collisionShape2D {get; set;} = null;
	// Called when the node enters the scene tree for the first time.
	public void Explode(){
		Godot.Collections.Array<Area2D> hits = GetOverlappingAreas();
		GD.Print("Ready explosion");
		GD.Print("Detect!"+hits);
		foreach(Area2D hit in hits) {
			if(hit is Hitbox box) {
				GD.Print("Detect!");
				box.damageComponent.attackHit(Damage);
			}
		}
	}
	public override void _Ready()
	{
		((CapsuleShape2D)collisionShape2D.Shape).Radius = ExplosionRange / 2;
		((CapsuleShape2D)collisionShape2D.Shape).Height = ExplosionRange;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		((ShaderMaterial)effect.Material).SetShaderParameter("iValue", Math.Min(1.0, timePassed/ExplosionDuration));
		if(timePassed >= 0.05 && !exploded){
			exploded = true;
			Explode();
		}
		if(timePassed >= ExplosionDuration){
			QueueFree();
		}
		timePassed += delta;
	}
}
