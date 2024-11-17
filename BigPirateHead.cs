using Godot;
using System;

public partial class BigPirateHead : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public void UpdateHealth(int h) {
		((ShaderMaterial)Material).SetShaderParameter("iValue", 1-(float)h/100);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
