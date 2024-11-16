using Godot;
using System;

public partial class MainViewport : Control
{
	private Vector2 dimensions = new(320, 180);
	private Vector2 lastViewportSize = Vector2.Zero;
	private SubViewportContainer viewportContainer = null;
	private int scalingFactor = 1;
	private int ScalingFactor {
		get => scalingFactor;
		set {
			scalingFactor = value;
			if(viewportContainer != null) {
				viewportContainer.Size = dimensions * scalingFactor;
				viewportContainer.Position = new(
					(lastViewportSize.X - viewportContainer.Size.X)/2, 
					0//lastViewportSize.Y - viewportContainer.Size.Y
				);
			}
		}
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		viewportContainer = GetNode<SubViewportContainer>("SubViewportContainer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 size = GetViewportRect().Size;
		if(size != lastViewportSize) {
			lastViewportSize = size;
			if(size.X / dimensions.X > size.Y / dimensions.Y) {
				ScalingFactor = (int)(size.Y / (dimensions.Y-1));
			} else {
				ScalingFactor = (int)(size.X / (dimensions.X-1));
			}
		}
	}
}
