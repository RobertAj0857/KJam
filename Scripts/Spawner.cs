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
	private double timePassed = 0;
	private double waitTime = 0;
	private int itemint = 0;
	[Export]
	public int ItemFrom { get; set; } = 1;
	[Export]
	public int ItemTo { get; set; } = 3;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		random = new RandomNumberGenerator();
		waitTime = random.RandfRange(from, to);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		timePassed += delta;
		if (timePassed >= waitTime)
		{
			timePassed = 0;
			waitTime = random.RandfRange(from, to);
			SpawnItems();
		}
	}

	public void SpawnItems()
	{

		itemint = random.RandiRange(ItemFrom, ItemTo);
		PickUp pickUp = (PickUp)GD.Load<PackedScene>("res://Component/PickUp.tscn").Instantiate();
		pickUp.Position = GetRandomPositionBetweenMarkers();
		GetParent().AddChild(pickUp);
		switch (itemint)
		{
			case 1:
				element = PirateCraftingController.Element.Cannon;
				pickUp.MakePickUp(this, element);
				break;

			case 2:
				element = PirateCraftingController.Element.PocketWatch;
				pickUp.MakePickUp(this, element);
				break;

			case 3:
				element = PirateCraftingController.Element.Rum;
				pickUp.MakePickUp(this, element);
				break;

			default:
				GD.Print("No item was picked");
				break;
		}


	}

	public Vector2 GetRandomPositionBetweenMarkers()
	{
		Vector2 posA = UpperLeft.GlobalPosition;
		Vector2 posB = BottomRight.GlobalPosition;

		float randomX = GD.Randf() * (posB.X - posA.X) + posA.X;
		float randomY = GD.Randf() * (posB.Y - posA.Y) + posA.Y;



		return new Vector2(randomX, randomY);
	}

}
