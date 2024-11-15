using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public partial class PirateCraftingController : Node2D
{
	[Export]
	public double InputTime = 0.5;
	public enum Element
	{
		Cannon,
		Rum,
		PocketWatch
	}
	private static string[] keyInputs = new string[6]{
		"Player1Element1",
		"Player2Element1",
		"Player1Element2",
		"Player2Element2",
		"Player1Element3",
		"Player2Element3",
	};
	private Dictionary<Element, int> elementAmounts;
	private Dictionary<Element, bool> currentCraftCombination;
	private Dictionary<int, LambdaExpression> elementEffects = new Dictionary<int, LambdaExpression>(){
		{100,() => GD.Print("CANNON BALL")},
		{010,() => GD.Print("DRUNK MOVEMENT")},
		{001,() => GD.Print("GO BACK IN TIME")},
		{110,() => GD.Print("FIRE CANNON BALL")},
		{011,() => GD.Print("TIME BOMB")},
		{101,() => GD.Print("TELEPORT WITH CANNON BALL")},
		{111,() => GD.Print("EXPLOSION TELEPORT")},
	};
	private Dictionary<String, Element> keyInputToElement = new Dictionary<string, Element>(){
		{keyInputs[0],Element.Cannon},
		{keyInputs[1],Element.Cannon},
		{keyInputs[2],Element.Rum},
		{keyInputs[3],Element.Rum},
		{keyInputs[4],Element.PocketWatch},
		{keyInputs[5],Element.PocketWatch},
	};
	private int amountOfInputs = 0;
	private int maxAmountOfInputs = 3;
	private double timeSinceInput = 0;
	private bool isPerformingAction = false;
	private bool activeCraftCombination = false;
	private bool isPlayer1 = true;
	public void makeElementEffect(){
		isPerformingAction = true;
		activeCraftCombination = false;
		elementEffects[convertCraftCombinationToInt()].Compile().DynamicInvoke();
		foreach (Element element in Enum.GetValues(typeof(Element)).Cast<Element>()){
			currentCraftCombination[element] = false;
		}
		amountOfInputs = 0;
		isPerformingAction = false;
	}
	private int convertCraftCombinationToInt(){
		String craftCombination = "";
		foreach(KeyValuePair<Element, bool> entry in currentCraftCombination)
		{
			if(entry.Value){
				craftCombination = craftCombination+"1";
			}else{
				craftCombination = craftCombination+"0";
			}
		}
		return craftCombination.ToInt();
	}
	private void addInput(Element element){
		timeSinceInput = 0;
		activeCraftCombination = true;
		amountOfInputs += 1;
		currentCraftCombination[element] = true;
	}
	public override void _Input(InputEvent @event)
    {
        base._Input(@event);
		if(@event is InputEventKey eventKey && eventKey.Pressed){
			foreach(String keyInput in keyInputs){
				if((isPlayer1 && keyInput.Contains("Player1") && eventKey.IsAction(keyInput) || !isPlayer1 && keyInput.Contains("Player2") && eventKey.IsAction(keyInput)) && !isPerformingAction){
					Element element = keyInputToElement[keyInput];
					if(elementAmounts[element] > 0){
						if(currentCraftCombination[element] || amountOfInputs >= maxAmountOfInputs){
							makeElementEffect();
							addInput(element);
							GD.Print("Make effect2");
							break;
						}else{
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
		Player player = GetParent<Player>();
		isPlayer1 = player.IsPlayer1;
		elementAmounts = new Dictionary<Element, int>();
		currentCraftCombination = new Dictionary<Element, bool>();
		foreach (Element element in Enum.GetValues(typeof(Element)).Cast<Element>()){
			elementAmounts[element] = 1;
			GD.Print("Temp");
			currentCraftCombination[element] = false;
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(!isPerformingAction){
			timeSinceInput += delta;
			if(activeCraftCombination && timeSinceInput >= InputTime){
				makeElementEffect();
				GD.Print("Make effect1");
			}
		}
	}
}
