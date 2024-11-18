using Godot;
using System;

public partial class MainMenu : Control
{
	[Export]
	public String path1;

	[Export]
	public String path2;

	[Export]
	public String path3;
	[Export]
	public TextureButton buttonOne;
	[Export]
	public TextureButton buttonTwo;
	[Export]
	public TextureButton buttonThree;

	public override void _Ready(){
		if(buttonThree != null){
			buttonThree.GrabFocus();
		}else{
			buttonOne.GrabFocus();
		}
	}
	public void button1() {
		GetTree().ChangeSceneToFile(path1);
	}

	public void button2() {
		GetTree().ChangeSceneToFile(path2);
	}

	public void button3() {
		GetTree().ChangeSceneToFile(path3);
	}

	public override void _Input(InputEvent @event){
		if(@event.IsActionPressed("Settings") && button2 != null){
			GetTree().ChangeSceneToFile("res://Scenes/main_menu.tscn");
		}
	}
}
