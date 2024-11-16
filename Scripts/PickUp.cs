using Godot;
using System;
using System.Collections.ObjectModel;

public partial class PickUp : Area2D
{
	[Export]
	public Sprite2D sprite2D;
	[Export]
	public Texture2D Cannon;
	[Export]
	public Texture2D Rum;
	[Export]
	public Texture2D StopWatch;
	[Export]
	public CollisionShape2D collision;
	public Spawner spawner;
	private PirateCraftingController.Element element;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("_Ready_PickUp");
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);


		// checks for players
		Godot.Collections.Array<Area2D> hits = GetOverlappingAreas();
		foreach (Area2D hit in hits)
		{
			if (hit is Hitbox box)
			{
				Collect((Player)box.GetParent<Player>());
				QueueFree();
				break;
			}
		}
	}

	public void MakePickUp(Spawner s, PirateCraftingController.Element element)
	{
		GD.Print("MakePickUp");
		this.element = element;
		spawner = s;
		switch (element)
		{
			case PirateCraftingController.Element.Cannon:
				GD.Print("Cannon event");
				sprite2D.Texture = Cannon;
				break;

			case PirateCraftingController.Element.PocketWatch:
				GD.Print("PC event");
				sprite2D.Texture = Rum;
				break;

			case PirateCraftingController.Element.Rum:
				GD.Print("Rum event");
				sprite2D.Texture = StopWatch;
				break;

			default:
				GD.Print("No item was picked");
				break;
		}
	}

	public void Collect(Player player)
	{
		player.pirateCraftingController.elementAmounts[element] += 1;
	}
}
