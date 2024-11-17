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

	public void button1() {
		GetTree().ChangeSceneToFile(path1);
	}

	public void button2() {
		GetTree().ChangeSceneToFile(path2);
	}

	public void button3() {
		GetTree().ChangeSceneToFile(path3);
	}
}
