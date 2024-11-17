using Godot;
using System;

public partial class Level : TileMapLayer
{
	[Export]
	TileMapLayer bridges;

	public bool IsOnBridge(Vector2 pos) {
		TileData tile = bridges.GetCellTileData(bridges.LocalToMap(bridges.ToLocal(pos)));
		if(tile == null) return false;
		return true;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
