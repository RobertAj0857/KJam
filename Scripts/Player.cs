using Godot;
using System;
using System.Collections.Generic;

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
	private bool inWater = false;
	public bool InWater {
		get => inWater; set{
			if(inWater == value) return;
			inWater = value;
			if(inWater) {
				((ShaderMaterial)sprite.Material).SetShaderParameter("inWater", 1);
			} else {
				((ShaderMaterial)sprite.Material).SetShaderParameter("inWater", 0);
			}
		}
	}
	private float baseFriction;
	private float baseAcceleration;
	private Vector2 inputDirection = Vector2.Zero;
	public Vector2 precisePosition = Vector2.Zero;
	private double drunkTimer = -1;
	private bool drunkMovement = false;
	private double currentDrunkDuration = 0;
	private LinkedList<Vector2> lastPositions = new LinkedList<Vector2>();
	private double savePositionInterval = 0.1;
	private int positionsWarpedBack = 15;
	private double timePassed = 0;
	private double interpolation = 0;
	public bool DrunkMovement{ get => drunkMovement; set{
		drunkMovement = value;
		if(drunkMovement){
			drunkTimer = 0;
			Friction = baseFriction / 5f;
			AccelerationRate = baseAcceleration / 2.2f;
			drunkParticleEffect.Emitting = true;
		}else{
			Friction = baseFriction;
			AccelerationRate = baseAcceleration;
			drunkParticleEffect.Emitting = false;
		}
	}}
	[Export]
	public AnimatedSprite2D sprite;
	[Export]
	public CombinationShower combinationShower;

	[Export]
	public PirateCraftingController pirateCraftingController;
	[Export]
	public CpuParticles2D timeWarpEffect;
	[Export]
	public CpuParticles2D drunkParticleEffect;

	private Vector2 lastMovingDirection; // For animations
	public Vector2 AimDirection = new Vector2(0, 1); // For projectiles
	private bool timeWarping = false;
	private int timeWarpTargetIndex = 0;
	private double timeWarpDurationPerStep = 0;
	private double dashDuration = 0;
	private Vector2 originDashPosition;
	private Vector2 targetDashPosition;
	private bool dashing = false;
	public bool TimeWarping {get => timeWarping; set{
		timeWarping = value;
		timeWarpEffect.Emitting = timeWarping;
	}}
	private bool isDead = false;
	public bool IsDead {get => isDead; set{
		isDead = value;
		sprite.Play("Dead");
		InWater = false;
		if(IsPlayer1){
			UI.Instance.GetNode<Control>("FinishScreen").GetNode<RichTextLabel>("Winner").Text = "[center]Blackbeard Won!";	
		}else{
			UI.Instance.GetNode<Control>("FinishScreen").GetNode<RichTextLabel>("Winner").Text = "[center]Redbeard Won!";
		}
		UI.Instance.GetNode<Control>("FinishScreen").Visible = true;	
	}}
    public override void _Ready()
    {
        base._Ready();
		precisePosition = GlobalPosition;
		baseAcceleration = AccelerationRate;
		baseFriction = Friction;
    }
    public void ApplyDrunkEffect(double duration){
		currentDrunkDuration = duration;
		DrunkMovement = true;
	}

	public void TimeWarp(double duration){
		TimeWarping = true;
		//sprite.Visible = false;
		timePassed = 0;
		timeWarpTargetIndex = 1;
		lastPositions.AddFirst(Position);
		if(lastPositions.Count > 0){
			lastPositions.RemoveLast();
		}
		timeWarpDurationPerStep = duration/((double)positionsWarpedBack);
	}

	public void Dash(double duration){
		dashing = true;
		timePassed = 0;
		originDashPosition = Position;
		targetDashPosition = originDashPosition + lastMovingDirection * 70;
		var spaceState = GetWorld2D().DirectSpaceState;
		var query = PhysicsRayQueryParameters2D.Create(Position + lastMovingDirection * 2,targetDashPosition,CollisionMask);
		var result = spaceState.IntersectRay(query);
		if (result.Count > 0){
			targetDashPosition = (Vector2)result["position"] - lastMovingDirection * 4;
		}
		dashDuration = duration;
	}

	public void GetInput()
	{
		if (IsPlayer1)
		{
			inputDirection = Input.GetVector("Player1Left", "Player1Right", "Player1Up", "Player1Down").Normalized();
		}
		else
		{
			inputDirection = Input.GetVector("Player2Left", "Player2Right", "Player2Up", "Player2Down").Normalized();
		}
		
		if(inputDirection != Vector2.Zero && !TimeWarping){
			AimDirection = lastMovingDirection;
			lastMovingDirection = inputDirection; // For animations
		}
	}

	public void LerpVelocityToMax(Vector2 target, float interpolationValue, float max)
	{
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
		if(IsDead){
			return;
		}
		timePassed += delta;
		if(TimeWarping){
			if(!dashing){
				Vector2 posFrom = Position;
				if (lastPositions.First != null){
					posFrom = lastPositions.First.Value;
				}
				Vector2 posTo = posFrom;
				if (lastPositions.First != null && lastPositions.First.Next != null){
					posTo = lastPositions.First.Next.Value;
				}
				interpolation += delta / timeWarpDurationPerStep;
				GlobalPosition = precisePosition;
				Position = posFrom.Lerp(posTo, (float)interpolation);
				precisePosition = GlobalPosition;
				if(interpolation >= 1){
					interpolation = 0;
					timePassed = 0;
					if(lastPositions.Count > 0){
						lastPositions.RemoveFirst();
					}
					timeWarpTargetIndex ++;
					if(timeWarpTargetIndex >= positionsWarpedBack - 1){
						//sprite.Visible = true;
						TimeWarping = false;
					}
				}
			}else{
				interpolation += delta / dashDuration;
				GlobalPosition = precisePosition;
				Position = originDashPosition.Lerp(targetDashPosition, (float)interpolation);
				precisePosition = GlobalPosition;
				if(interpolation >= 1){
					interpolation = 0;
					timePassed = 0;
					dashing = false;
					TimeWarping = false;
				}
			}
		}else{
			if(timePassed >= savePositionInterval){
				lastPositions.AddFirst(Position);
				timePassed = 0;
				if(lastPositions.Count > positionsWarpedBack){
					lastPositions.RemoveLast();
				}
			}
		}
		if(drunkTimer != -1){
			drunkTimer += delta;
			if(drunkTimer >= currentDrunkDuration){
				drunkTimer = -1;
				DrunkMovement = false;
			}
		}
		float WaterFriction = InWater? 1.575f: 1;
		LerpVelocityToMax(new(), (float)Math.Pow(Friction*WaterFriction, delta * 60), 0);
		GetInput();
		bool isMoving = inputDirection != Vector2.Zero;
		if (isMoving && !TimeWarping)
		{
			Velocity += inputDirection * Speed * AccelerationRate * (float)delta;
			Velocity.LimitLength(MaxSpeed);
		}
		GlobalPosition = precisePosition;
		if (Velocity != Vector2.Zero && !TimeWarping)
		{
			MoveAndSlide();
		}
		precisePosition = GlobalPosition;
		GlobalPosition = precisePosition.Round();

		
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
