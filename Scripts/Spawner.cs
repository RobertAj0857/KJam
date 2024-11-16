using Godot;
using System;

public partial class Spawner : Node2D
{
	[Export]
	public Marker2D UpperLeft;
	[Export]
	public Marker2D BottomRight;

	[Export]
	public PirateCraftingController.Element element;

	[Export]
	public float from { get; set; } = 0.2f;
	[Export]
	public float to { get; set; } = 3f;
	private RandomNumberGenerator random;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		random = new RandomNumberGenerator();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void SpawnTimer()
	{
		Timer timer = new Timer();
		timer.OneShot = true;
		timer.WaitTime = random.RandfRange(from, to);
		AddChild(timer);
		//timer.Connect("timeout", this, nameof(onTimerTimeout));
		timer.Start();
	}

	private void SpawnItems()
	{
		GD.Print("fuck");
	}

}
