using Godot;
using System;

public partial class MarkerLocation : Marker2D
{
	Marker2D marker;

	[Export]
	public SubViewport subViewport { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		marker.Position = subViewport.Size;
	}
}
