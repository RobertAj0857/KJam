using Godot;
using System;

public partial class Health : Node2D
{
    [Export] int maxHealth = 100;
    [Export] bool immortality = false;
    [Export]
	public AnimationPlayer animationPlayer;
    public int MaxHealth { get => maxHealth; }
    public bool Immortality { get => immortality; set { immortality = value; } }

    [Export]
    public Player character { get; set; } = null;

    private int pirateHealth;
    public int PirateHealth { get => pirateHealth; set { pirateHealth = value; } }

    public override void _Ready()
    {
        pirateHealth = maxHealth;
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {

    }

    public void loseHealth(int damage)
    {
        if(!Immortality){
            pirateHealth -= damage;
            checkIfDead();
            animationPlayer.Play("HitEffect");
        }
    }

    public void checkIfDead()
    {
        if (pirateHealth <= 0)
        {
            pirateHealth = 0;
            character.IsDead = true;
            GD.Print("You fucking died loser!");
            //character.die();
        }
    }
}
