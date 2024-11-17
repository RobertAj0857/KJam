using Godot;
using System;

public partial class Hitbox : Area2D
{
	public bool Team1 {get => ((Player) GetParent()).IsPlayer1;}
	[Export]
	public Damage damageComponent;
	private Player player;
	//[Export]
	//Health
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetParent<Player>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
		player.InWater = (
			GetOverlappingBodies().Count > 0 
			&& !GetParent().GetParent().GetNode<Level>("Level").IsOnBridge(GlobalPosition)
		);
		if(player.InWater) {
			//GD.Print("InWater");
		}
    }
}
