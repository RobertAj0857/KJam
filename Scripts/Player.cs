using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public bool IsPlayer1 = true;
	[Export]
	public float Speed { get; set; } = 1000.0f;
	[Export]
	public float AccelerationRate { get; set; } = 2f;
	[Export]
	public float MaxSpeed { get; set; } = 2000.0f;
	[Export]
	public float Friction { get; set; } = 0.3f;
	private float baseFriction;
	private float baseAcceleration;
	private float currentVelocity = 0f;
	private Vector2 inputDirection = Vector2.Zero;
	private double drunkTimer = -1;
	private bool drunkMovement = false;
	private double currentDrunkDuration = 0;
	public bool DrunkMovement{ get => drunkMovement; set{
		drunkMovement = value;
		if(drunkMovement){
			drunkTimer = 0;
			Friction = baseFriction / 5f;
			AccelerationRate = baseAcceleration / 2.25f;
		}else{
			Friction = baseFriction;
			AccelerationRate = baseAcceleration;
		}
	}}
	[Export]
	public AnimatedSprite2D sprite;

	private Vector2 lastMovingDirection; // For animations
	public Vector2 AimDirection = new Vector2(0,1); // For projectiles
	private bool isDead = false;
	public bool IsDead {get => isDead; set{
		isDead = value;
		sprite.Play("Dead");
		GetTree().ReloadCurrentScene();
	}}
    public override void _Ready()
    {
        base._Ready();
		baseAcceleration = AccelerationRate;
		baseFriction = Friction;
    }
    public void ApplyDrunkEffect(double duration){
		currentDrunkDuration = duration;
		DrunkMovement = true;
	}

	public void GetInput()
	{
		if (IsPlayer1)
		{
			inputDirection = Input.GetVector("Player1Left", "Player1Right", "Player1Up", "Player1Down");
		}
		else
		{
			inputDirection = Input.GetVector("Player2Left", "Player2Right", "Player2Up", "Player2Down");
		}
		
		if(inputDirection != Vector2.Zero){
			AimDirection = lastMovingDirection;
			lastMovingDirection = inputDirection; // For animations
		}
    }

	public void LerpVelocityToMax(Vector2 target, float interpolationValue, float max){
		max *= Speed;
		float speed = Velocity.Length();
		Vector2 sum = Velocity + (target - Velocity) * interpolationValue;
		float sumSpeed = sum.Length();
		if (sumSpeed == 0)
		{
			Velocity = new();
		}
		else if (speed >= max && sumSpeed >= speed)
		{
			Velocity = (speed == 0) ? new() : sum / sumSpeed * speed;
		}
		else if (speed <= max && sumSpeed > max)
		{
			Velocity = (max == 0) ? new() : sum / sumSpeed * max;
		}
		else
		{
			Velocity = sum;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if(drunkTimer != -1){
			drunkTimer += delta;
			if(drunkTimer >= currentDrunkDuration){
				drunkTimer = -1;
				DrunkMovement = false;
			}
		}
		LerpVelocityToMax(new(), (float)Math.Pow(Friction, delta * 60), 0);
		GetInput();
		bool isMoving = inputDirection != Vector2.Zero;
		if (isMoving)
		{
			Velocity += inputDirection * Speed * AccelerationRate * (float)delta;
			Velocity.LimitLength(MaxSpeed);
		}
		if(Velocity != Vector2.Zero){
			MoveAndSlide();
		}

		// For animations
		string animationType = "Idle";
		if (isMoving)
		{
			// Flip
			if (lastMovingDirection.X > 0)
			{
				sprite.FlipH = true;
			}
			else if (lastMovingDirection.X < 0)
			{
				sprite.FlipH = false;
			}
			animationType = "Walk";
		}

		if (lastMovingDirection.X < -0.9 || 0.9 < lastMovingDirection.X)
		{
			sprite.Play("Side " + animationType);
		}
		else if (lastMovingDirection.Y > 0)
		{
			sprite.Play("Front " + animationType);
		}
		else if (lastMovingDirection.Y < 0)
		{
			sprite.Play("Back " + animationType);
		}
	}
}
