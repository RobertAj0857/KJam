using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public partial class PirateCraftingController : Node2D
{
	[Export]
	public double InputTime = 0.35;
	[Export]
	private double EffectCooldown = 0.2;
	public enum Element
	{
		Cannon,
		Rum,
		PocketWatch
	}
	private static string[] keyInputs = new string[6]{
		"Player1Element1",
		"Player2Element3",
		"Player1Element2",
		"Player2Element2",
		"Player1Element3",
		"Player2Element1",
	};
	public Dictionary<Element, int> elementAmounts;
	private Dictionary<Element, bool> currentCraftCombination;
	private delegate void Combination(PirateCraftingController p);
	private Dictionary<int, Combination> elementEffects = new Dictionary<int, Combination>(){
		{100,p => p.shootCannonBall()},
		{010,p => p.drinkRum()},
		{001,p => p.goBackInTime()},
		{110,p => p.shootFireCannonBall()},
		{011,p => p.timeBomb()},
		{101,p => p.teleportShot()},
		{111,p => p.explosionPlayer()},
	};
	private Dictionary<String, Element> keyInputToElement = new Dictionary<string, Element>(){
		{keyInputs[0],Element.Cannon},
		{keyInputs[1],Element.Cannon},
		{keyInputs[2],Element.Rum},
		{keyInputs[3],Element.Rum},
		{keyInputs[4],Element.PocketWatch},
		{keyInputs[5],Element.PocketWatch},
	};
	private Player player;
	private int amountOfInputs = 0;
	private int maxAmountOfInputs = 3;
	public int maxAmountOfElements = 5;
	private double timeSinceInput = 0;
	private double timeSinceEffect = 0;
	private bool isPerformingAction = false;
	private bool activeCraftCombination = false;

	private void shootCannonBall()
	{
		GD.Print("Cannon ball");
		Projectile cannonBall = (Projectile) GD.Load<PackedScene>("res://Scenes/cannonBall.tscn").Instantiate();
		cannonBall.Position = player.Position;
		cannonBall.Direction = player.AimDirection;
		cannonBall.Team1 = player.IsPlayer1;
		player.GetParent().AddChild(cannonBall);
	}
	private void drinkRum()
	{
		GD.Print("DRUNK MOVEMENT");
		player.ApplyDrunkEffect(2.5);
	}
	private void goBackInTime()
	{
		GD.Print("GO BACK IN TIME");
		player.TimeWarp(0.4);
	}
	private void shootFireCannonBall()
	{
		GD.Print("CANNON BALL ON FIRE");
		Projectile cannonBall = (Projectile) GD.Load<PackedScene>("res://Scenes/fire_ball.tscn").Instantiate();
		cannonBall.Position = player.Position;
		cannonBall.Direction = player.AimDirection;
		cannonBall.Team1 = player.IsPlayer1;
		player.GetParent().AddChild(cannonBall);
	}
	private void timeBomb()
	{
		GD.Print("TIME BOMB");
		ExplodingBarrel explodingBarrel = (ExplodingBarrel) GD.Load<PackedScene>("res://Scenes/explodingBarrel.tscn").Instantiate();
		explodingBarrel.Position = player.Position;
		player.GetParent().AddChild(explodingBarrel);
	}
	private void teleportShot()
	{
		GD.Print("TELEPORT WITH CANNON BALL");
		MagicCannonBall magicCannonBall = (MagicCannonBall) GD.Load<PackedScene>("res://Scenes/magicCannonBall.tscn").Instantiate();
		magicCannonBall.Position = player.Position;
		magicCannonBall.Direction = player.AimDirection;
		magicCannonBall.Team1 = player.IsPlayer1;
		magicCannonBall.Player = player;
		player.GetParent().AddChild(magicCannonBall);
	}
	private void explosionPlayer()
	{
		GD.Print("EXPLOSION TELEPORT");
	}

	public void updateElementAmounts(){
		Sprite2D playerUI;
		if(player.IsPlayer1){
			playerUI = UI.Instance.GetNode<Sprite2D>("Player1");
		}else{
			playerUI = UI.Instance.GetNode<Sprite2D>("Player2");
		}
		playerUI.GetNode<Sprite2D>("Elements").GetNode<Sprite2D>("Cannon").GetNode<RichTextLabel>("Amount").Text = elementAmounts[Element.Cannon]+"";
		playerUI.GetNode<Sprite2D>("Elements").GetNode<Sprite2D>("Rum").GetNode<RichTextLabel>("Amount").Text = elementAmounts[Element.Rum]+"";
		playerUI.GetNode<Sprite2D>("Elements").GetNode<Sprite2D>("PocketWatch").GetNode<RichTextLabel>("Amount").Text = elementAmounts[Element.PocketWatch]+"";
	}
	public void makeElementEffect()
	{
		isPerformingAction = true;
		activeCraftCombination = false;
		timeSinceEffect = 0;
		elementEffects[convertCraftCombinationToInt()](this);
		foreach (Element element in Enum.GetValues(typeof(Element)).Cast<Element>())
		{
			if (currentCraftCombination[element])
			{
				elementAmounts[element] -= 1;
				updateElementAmounts();
			}
			currentCraftCombination[element] = false;
			player.combinationShower.updateCombination(currentCraftCombination, amountOfInputs);
		}
		amountOfInputs = 0;
	}
	private int convertCraftCombinationToInt()
	{
		String craftCombination = "";
		foreach (KeyValuePair<Element, bool> entry in currentCraftCombination)
		{
			if (entry.Value)
			{
				craftCombination = craftCombination + "1";
			}
			else
			{
				craftCombination = craftCombination + "0";
			}
		}
		return craftCombination.ToInt();
	}
	private void addInput(Element element)
	{
		timeSinceInput = 0;
		activeCraftCombination = true;
		amountOfInputs += 1;
		currentCraftCombination[element] = true;
		player.combinationShower.updateCombination(currentCraftCombination, amountOfInputs);
	}
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event is InputEventKey eventKey && eventKey.Pressed && !player.IsDead && !player.TimeWarping)
		{
			foreach (String keyInput in keyInputs)
			{
				if ((player.IsPlayer1 && keyInput.Contains("Player1") && eventKey.IsAction(keyInput) || !player.IsPlayer1 && keyInput.Contains("Player2") && eventKey.IsAction(keyInput)) && !isPerformingAction)
				{
					Element element = keyInputToElement[keyInput];
					if (elementAmounts[element] > 0)
					{
						if (currentCraftCombination[element] || amountOfInputs >= maxAmountOfInputs)
						{
							makeElementEffect();
							addInput(element);
							GD.Print("Make effect2");
							break;
						}
						else
						{
							addInput(element);
							GD.Print("Make effect3");
						}
					}
				}
			}
		}
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetParent<Player>();
		elementAmounts = new Dictionary<Element, int>();
		currentCraftCombination = new Dictionary<Element, bool>();
		foreach (Element element in Enum.GetValues(typeof(Element)).Cast<Element>())
		{
			elementAmounts[element] = 0;
			currentCraftCombination[element] = false;
		}
		updateElementAmounts();
		player.combinationShower.updateCombination(currentCraftCombination, amountOfInputs);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!isPerformingAction)
		{
			timeSinceInput += delta;
			if (activeCraftCombination && timeSinceInput >= InputTime)
			{
				makeElementEffect();
				GD.Print("Make effect1");
			}
		}else{
			timeSinceEffect += delta;
			if(timeSinceEffect >= EffectCooldown){
				isPerformingAction = false;
			}
		}
	}
}
