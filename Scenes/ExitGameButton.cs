using Godot;
using System;

public partial class ExitGameButton : TextureButton
{
	[Export]
	public Texture2D newNormalTexture;
	[Export]
	public Texture2D focusTexture;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void _on_button_up() {
		GetTree().Quit();
	}

	public void focusButton(){
		if(!HasFocus()){
			GrabFocus();
		}TextureNormal = focusTexture;
	}

	public void exitFocus(){
		TextureNormal = newNormalTexture;
	}
}
