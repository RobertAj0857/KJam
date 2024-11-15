using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public bool IsPlayer1 = true;
	[Export]
	public float Speed { get; set; } = 1000.0f;
	[Export]
	public float AccelerationRate { get; set; } = 0.1f;
	[Export]
	public float MaxSpeed { get; set; } = 1000.0f;
	[Export]
	public float Friction { get; set; } = 0.25f;
	private float currentVelocity  = 0f;
	private Vector2 inputDirection = Vector2.Zero;

	public void GetInput()
    {
        if (IsPlayer1) {
			inputDirection = Input.GetVector("Player1Left", "Player1Right", "Player1Up", "Player1Down");
		}else{
			inputDirection = Input.GetVector("Player2Left", "Player2Right", "Player2Up", "Player2Down");
		}
    }

	public void LerpVelocityToMax(Vector2 target, float interpolationValue, float max) {
		max *= Speed;
		float speed = Velocity.Length();
		Vector2 sum = Velocity + (target - Velocity) * interpolationValue;
		float sumSpeed = sum.Length();
		if (sumSpeed == 0) {
			Velocity = new();
		} else if (speed >= max && sumSpeed >= speed) {
			Velocity = (speed == 0)? new(): sum / sumSpeed * speed;
		} else if(speed <= max && sumSpeed > max) {
			Velocity = (max == 0)? new(): sum / sumSpeed * max;
		} else {
			Velocity = sum;
		}
	}

    public override void _PhysicsProcess(double delta)
	{
		LerpVelocityToMax(new(), (float) Math.Pow(Friction, delta * 60), 0);
		GetInput();
		bool isMoving = inputDirection != Vector2.Zero;
		if (isMoving) {
			Velocity += inputDirection * Speed * AccelerationRate * (float) delta;
			Velocity.LimitLength(MaxSpeed);
			MoveAndSlide();
		}
	}
}
