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

	[Export]
	public AnimatedSprite2D sprite;

	private Vector2 lastMovingDirection; // For animations

	public void GetInput()
    {
        if (IsPlayer1) {
			inputDirection = Input.GetVector("Player1Left", "Player1Right", "Player1Up", "Player1Down");
		}else{
			inputDirection = Input.GetVector("Player2Left", "Player2Right", "Player2Up", "Player2Down");
		}

		lastMovingDirection = inputDirection; // For animations
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
		Boolean isMoving = inputDirection != Vector2.Zero;
		if (isMoving) {
			Velocity += inputDirection * Speed * AccelerationRate * (float) delta;
			Velocity.LimitLength(MaxSpeed);
			MoveAndSlide();
		}

		// For animations
		if (isMoving) {
			// Flip
			if (lastMovingDirection.X > 0) {
				sprite.FlipH = true;
			} else if (lastMovingDirection.X < 0) {
				sprite.FlipH = false;
			}

			if (lastMovingDirection.Equals(new Vector2(1, 0))) {
				sprite.Play("Side Walk");
			} else if (lastMovingDirection.Equals(new Vector2(-1, 0))) {
				sprite.Play("Side Walk");
			} else if (lastMovingDirection.Equals(new Vector2(0, 1))) {
				sprite.Play("Front Walk");
			} else if (lastMovingDirection.Equals(new Vector2(0, -1))) {
				sprite.Play("Back Walk");
			} else if (lastMovingDirection.Dot(new Vector2(1, 1)) > 0.9) {
				sprite.Play("Front Walk");
			} else if (lastMovingDirection.Dot(new Vector2(-1, 1)) > 0.9) {
				sprite.Play("Front Walk");
			} else if (lastMovingDirection.Dot(new Vector2(1, -1)) > 0.9) {
				sprite.Play("Back Walk");
			} else if (lastMovingDirection.Dot(new Vector2(-1, -1)) > 0.9) {
				sprite.Play("Back Walk");
			}
		}
		if (lastMovingDirection.Equals(new Vector2(1, 0))) {
			sprite.Play("Side Idle");
		} else if (lastMovingDirection.Equals(new Vector2(-1, 0))) {
			sprite.Play("Side Idle");
		} else if (lastMovingDirection.Equals(new Vector2(0, 1))) {
			sprite.Play("Front Idle");
		} else if (lastMovingDirection.Equals(new Vector2(0, -1))) {
			sprite.Play("Back Idle");
		} else if (lastMovingDirection.Dot(new Vector2(1, 1)) > 0.9) {
			sprite.Play("Front Idle");
		} else if (lastMovingDirection.Dot(new Vector2(-1, 1)) > 0.9) {
			sprite.Play("Front Idle");
		} else if (lastMovingDirection.Dot(new Vector2(1, -1)) > 0.9) {
			sprite.Play("Back Idle");
		} else if (lastMovingDirection.Dot(new Vector2(-1, -1)) > 0.9) {
			sprite.Play("Back Idle");
		}

	}
}
