using Godot;
using System;
using System.Collections;
using System.Linq.Expressions;

public partial class Sfx : AudioStreamPlayer
{
	[Export]
	private MP3 mp3;

	public enum MP3
	{
		Cannonball,
		DRUNKMOVEMENT,
		GOBACKINTIME,
		CANNONBALLONFIRE,
		TIMEBOMB,
		TELEPORTWITHCANNONBALL,
		EXPLOSIONTELEPORT,
		SEAGULL,
		WAVES,
	}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void PlaySound(String soundEffect)
	{
		switch (soundEffect)
		{
			case "Cannon ball":
				if (MP3.Cannonball == mp3) Playing = true;
				break;
			case "DRUNK MOVEMENT":
				if (MP3.DRUNKMOVEMENT == mp3) { Playing = true; }
				break;
			case "GO BACK IN TIME":
				if (MP3.GOBACKINTIME == mp3) { Playing = true; }
				break;
			case "CANNON BALL ON FIRE":
				if (MP3.CANNONBALLONFIRE == mp3) { Playing = true; }
				break;
			case "TIME BOMB":
				if (MP3.TIMEBOMB == mp3) { Playing = true; }
				break;
			case "TELEPORT WITH CANNON BALL":
				if (MP3.TELEPORTWITHCANNONBALL == mp3) { Playing = true; }
				break;
			case "EXPLOSION TELEPORT":
				if (MP3.EXPLOSIONTELEPORT == mp3) { Playing = true; }
				break;
			case "SEAGULL":
				if (MP3.SEAGULL == mp3) { Playing = true; }
				break;
			case "WAVES":
				if (MP3.WAVES == mp3) { Playing = true; }
				break;
			default:
				GD.Print("Illegal sound was made");
				break;
		}

	}
}
