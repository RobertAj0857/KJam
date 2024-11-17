using Godot;
using System;
using System.Collections.ObjectModel;
using System.Drawing;

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
	
	private bool isSpawned = false;
	
	private double spawnTime = 0.5;
	private double timePassed = 0;
	private PirateCraftingController.Element element;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite2D.Scale = Vector2.Zero;
	}
    public override void _Process(double delta)
    {
        base._Process(delta);
		if(!isSpawned){
			Visible = true;
			isSpawned = true;
		}
		timePassed += delta;
		if(timePassed < spawnTime){
			sprite2D.Scale = Vector2.One * (float)(timePassed / spawnTime);
		}else if(sprite2D.Scale != Vector2.One){
			sprite2D.Scale = Vector2.One;
		}
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
					spawner.amountOfSpawns --;
					QueueFree();
					return;
				}else{
					cantPickUp = true;
				}
			}
		}
		if(GetOverlappingBodies().Count > 0 && !cantPickUp){
			spawner.amountOfSpawns --;
			QueueFree();
		}
	}

	public void MakePickUp(Spawner s, PirateCraftingController.Element element)
	{
		this.element = element;
		spawner = s;
		spawner.amountOfSpawns ++;
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
