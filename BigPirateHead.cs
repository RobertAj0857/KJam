using Godot;
using System;

public partial class BigPirateHead : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		pos = GlobalPosition;
	}

	private Vector2 pos;

	float realValue = 100;
	float shownValue = 100;

	public void UpdateHealth(int h) {
		float t = realValue;
		realValue = h;
		if(t > h) {
			velocity = Vector2.FromAngle(random.NextSingle()*Mathf.Pi*2) * (5 + random.NextSingle() * 8);
		}
	}

	Vector2 velocity = Vector2.Zero;
	Vector2 offset = Vector2.Zero;

	private Random random = new();

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(offset.Length() > 1 || velocity.Length() > 1) {
			velocity = (velocity - offset) / 1.5f;
			offset += velocity;
			GlobalPosition = ((pos + offset)/2).Round()*2;
		} else if(offset != Vector2.Zero) {
			offset = Vector2.Zero;
			velocity =  Vector2.Zero;
			GlobalPosition = pos;
		}
		float i = 0.1f;
		if(Mathf.Abs(shownValue-realValue) > 1) {

			shownValue = Mathf.Lerp(shownValue, realValue, i);//((float)Math.Pow(i,delta*60)-shownValue)/(realValue-shownValue);
		} else if(shownValue != realValue) {
			shownValue = realValue;
		} else {
			return;
		}
		((ShaderMaterial)Material).SetShaderParameter("iValue", 1-(float)shownValue/100);
	}
}
