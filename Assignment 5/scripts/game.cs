using Godot;
using System;

public class Game : Node
{
    // References
    private Node lasers;
    private Node player;
    private Node asteroids;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Initialize references
        lasers = GetNode("Lasers");
        player = GetNode("Player");
        asteroids = GetNode("Asteroids");

        // Connect player laser shot signal
        player.Connect("laser_shot", this, nameof(_on_player_laser_shot));

        // Connect asteroid explosion signal for each asteroid
        foreach (Node asteroid in asteroids.GetChildren())
        {
            asteroid.Connect("exploded", this, nameof(_on_asteroid_exploded));
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // Reload the scene when the reset action is pressed
        if (Input.IsActionJustPressed("reset"))
        {
            GetTree().ReloadCurrentScene();
        }
    }

    // Called when the player shoots a laser
    private void _on_player_laser_shot(Node laser)
    {
        lasers.AddChild(laser);
    }

    // Called when an asteroid explodes
    private void _on_asteroid_exploded(Node asteroid)
    {
        // Remove the asteroid from the scene and its parent container
        asteroid.QueueFree();
        asteroids.RemoveChild(asteroid);
    }
}
