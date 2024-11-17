using Godot;
using System;

public partial class Music : AudioStreamPlayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void PlaySound(String soundEffect)
	{
		foreach (Node node in GetChildren())
		{
			if (node is Sfx sfx)
			{
				sfx.PlaySound(soundEffect);
			}
		}
	}
}
