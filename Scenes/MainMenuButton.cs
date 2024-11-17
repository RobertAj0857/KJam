using Godot;
using System;

public partial class MainMenuButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void _on_button_up() {
		GetTree().ChangeSceneToFile("res://Scenes/main_menu.tscn");
		GetParent<Control>().Visible = false;
	}
}
