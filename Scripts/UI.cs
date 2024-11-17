using Godot;
using System;

public partial class UI : Control
{
	[Export]
	public BigPirateHead head1;
	[Export]
	public BigPirateHead head2;
	private static UI instance = null;
	public static UI Instance {get{
		if(instance == null){
			instance = new UI();
		}
		return instance;
	}}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
