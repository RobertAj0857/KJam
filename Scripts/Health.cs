using Godot;
using System;

public partial class Health : Node2D
{
    [Export] int maxHealth = 100;
    [Export] bool immortality = false;
    public int MaxHealth { get => maxHealth; }
    public bool Immortality { get => immortality; set { immortality = value; } }

    [Export]
    public Damage damage { get; set; } = null;

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

    public void loseHealth()
    {
        pirateHealth -= damage.amount;
        checkIfDead();
    }

    public void checkIfDead()
    {
        if (pirateHealth <= 0)
        {
            pirateHealth = 0;
            GD.Print("You fucking died loser!");
            //character.die();
        }
    }
}
