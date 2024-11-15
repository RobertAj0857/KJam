using Godot;
using System;

public partial class Node2d : Node2D
{
	// Called when the node enters the scene tree for the first time.
	int val;
	public override void _Ready()
	{
		val = 10;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		for (int i = 0; i < 10; i++)
		{
			val += i;
			Console.WriteLine(val);
		}
	}
}
