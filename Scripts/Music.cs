using Godot;
using System;

public partial class Music : AudioStreamPlayer
{
	private RandomNumberGenerator random;
	private double timePassed = 0;
	private double waitTime = 0;
	[Export]
	public float from { get; set; } = 30f;
	[Export]
	public float to { get; set; } = 60f;

	private double timePassed2 = 0;
	private double waitTime2 = 0;
	[Export]
	public float from2 { get; set; } = 10f;
	[Export]
	public float to2 { get; set; } = 20f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		random = new RandomNumberGenerator();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		timePassed += delta;
		if (timePassed >= waitTime)
		{
			timePassed = 0;
			waitTime = random.RandfRange(from, to);
			PlaySound("SEAGULL");
		}

		timePassed2 += delta;
		if (timePassed2 >= waitTime2)
		{
			timePassed2 = 0;
			waitTime2 = random.RandfRange(from2, to2);
			PlaySound("WAVES");
		}
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
