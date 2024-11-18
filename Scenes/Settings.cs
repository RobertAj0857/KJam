using Godot;
using System;

public partial class Settings : Panel
{
	private Sprite2D sprite2D;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetTree().Paused = false;
		sprite2D = GetNode<Sprite2D>("Sprite2D");
		sprite2D.GetNode<TextureButton>("MainMenuButton").Visible = false;
		sprite2D.GetNode<TextureButton>("ExitGameButton").Visible = false;
		if(!(GetParent() is UI)){
			sprite2D.GetNode<TextureButton>("MainMenuButton").QueueFree();
			sprite2D.GetNode<TextureButton>("ExitGameButton").Position = new Vector2(29, 50);
		}
	}

	public override void _Input(InputEvent @event){
		if(@event.IsActionPressed("Settings")){
			GetTree().Paused = !Visible;
			sprite2D.GetNode<TextureButton>("ExitGameButton").Visible = !Visible;
			if(sprite2D.GetNodeOrNull<TextureButton>("MainMenuButton") != null){
				sprite2D.GetNode<TextureButton>("MainMenuButton").Visible = !Visible;
				sprite2D.GetNode<TextureButton>("MainMenuButton").GrabFocus();
			}else{
				sprite2D.GetNode<TextureButton>("ExitGameButton").GrabFocus();
			}
			Visible = !Visible;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
