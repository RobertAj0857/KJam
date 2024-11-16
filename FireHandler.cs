using Godot;
using System;

public partial class FireHandler : Node2D
{
	private static FireHandler instance = null;
	public static FireHandler Instance {
		get => instance;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	
}
