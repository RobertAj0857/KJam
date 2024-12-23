using Godot;
using System;

public partial class RestartButton : TextureButton
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public Texture2D newNormalTexture;

	[Export]
	public Texture2D focusTexture;
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_button_up() {
		GetTree().ReloadCurrentScene();
	}

	public void _on_finish_screen_visibility_changed(){
		if(GetParent<Control>().Visible){
			GrabFocus();
		}
	}

	public void focusButton(){
		if(!HasFocus()){
			GrabFocus();
		}
		TextureNormal = focusTexture;
	}

	public void exitFocus(){
		TextureNormal = newNormalTexture;
	}
}
