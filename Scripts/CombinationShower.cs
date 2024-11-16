using Godot;
using System;
using System.Collections.Generic;

public partial class CombinationShower : Node2D
{
	private Sprite2D cannonSprite;
	private Sprite2D rumSprite;
	private Sprite2D watchSprite;
	private List<List<Vector2>> elementPositions = new List<List<Vector2>>(){
		new List<Vector2>(){
			new Vector2(0, -13)
		},
		new List<Vector2>(){
			new Vector2(-7, -13),
			new Vector2(7, -13)
		},
		new List<Vector2>(){
			new Vector2(-14, -13),
			new Vector2(0, -13),
			new Vector2(14, -13)
		}
	};
	// Called when the node enters the scene tree for the first time.
	public void updateCombination(Dictionary<PirateCraftingController.Element, bool> currentCraftCombination, int amountInCombination){
		int index = 0;
		foreach (KeyValuePair<PirateCraftingController.Element, bool> element in currentCraftCombination)
		{
			Sprite2D sprite = null;
			switch(element.Key){
				case PirateCraftingController.Element.Cannon:
					sprite = cannonSprite;
					break;
				case PirateCraftingController.Element.Rum:
					sprite = rumSprite;
					break;
				case PirateCraftingController.Element.PocketWatch:
					sprite = watchSprite;
					break;
				default:
					break;
			}
			if(element.Value){
				sprite.Position = elementPositions[amountInCombination-1][index];
				index ++;
			}
			if(sprite != null){
				sprite.Visible = element.Value;
			}
		}
	}
	public override void _Ready()
	{
		cannonSprite = GetNode<Sprite2D>("Cannon");
		rumSprite = GetNode<Sprite2D>("Rum");
		watchSprite = GetNode<Sprite2D>("Watch");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
