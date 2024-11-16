using Godot;
using System;

public partial class MarkerLocation : Marker2D
{
	Marker2D marker;

	[Export]
	public float xPos = 640;

	[Export]
	public float YPos = 360;

	private Vector2 vec;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		vec = new Vector2(xPos, YPos);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		marker.Position = vec;
	}
}
