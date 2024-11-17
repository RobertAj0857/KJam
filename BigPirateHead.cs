using Godot;
using System;

public partial class BigPirateHead : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	float realValue = 100;
	float shownValue = 100;

	public void UpdateHealth(int h) {
		realValue = h;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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
