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

	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);


		// checks for players
		Godot.Collections.Array<Area2D> hits = GetOverlappingAreas();
		bool cantPickUp = false;
		foreach (Area2D hit in hits)
		{
			if (hit is Hitbox box)
			{
				Player player = (Player)box.GetParent<Player>();
				if(player.pirateCraftingController.elementAmounts[element] < player.pirateCraftingController.maxAmountOfElements){
					Collect(player);
					QueueFree();
					return;
				}else{
					cantPickUp = true;
				}
			}
		}
		if(hits.Count > 0 && !cantPickUp){
			QueueFree();
		}
	}

	public void MakePickUp(Spawner s, PirateCraftingController.Element element)
	{
		this.element = element;
		spawner = s;
		switch (element)
		{
			case PirateCraftingController.Element.Cannon:
				sprite2D.Texture = Cannon;
				break;

			case PirateCraftingController.Element.PocketWatch:
				sprite2D.Texture = StopWatch;
				break;

			case PirateCraftingController.Element.Rum:
				sprite2D.Texture = Rum;
				break;

			default:
				GD.Print("No item was picked");
				break;
		}
	}

	public void Collect(Player player)
	{
		player.pirateCraftingController.elementAmounts[element] += 1;
		player.pirateCraftingController.updateElementAmounts();
	}
}
