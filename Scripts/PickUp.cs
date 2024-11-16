using Godot;
using System;

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
	private PirateCraftingController.Element element;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		switch (element)
		{
			case PirateCraftingController.Element.Cannon:
				sprite2D.Texture = Cannon;
				break;

			case PirateCraftingController.Element.PocketWatch:
				sprite2D.Texture = Rum;
				break;

			case PirateCraftingController.Element.Rum:
				sprite2D.Texture = StopWatch;
				break;

			default:
				GD.Print("No item was picked");
				break;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void MakePickUp(PirateCraftingController.Element element)
	{
		this.element = element;
	}
}
