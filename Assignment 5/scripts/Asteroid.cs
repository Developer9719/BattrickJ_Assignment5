using Godot;
using System;

public class Asteroid : Area2D
{
    // Signal equivalent in C#
    [Signal]
    public delegate void Exploded();

    // Movement vector and speed
    private Vector2 movementVector = new Vector2(0, -1);
    private float speed = 150f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Set a random rotation when the asteroid is ready
        Rotation = (float)GD.RandRange(0, Mathf.Tau);
    }

    // Called every frame, but useful for physics-related updates
    public override void _PhysicsProcess(double delta)
    {
        // Move the asteroid, rotating the movement vector based on its rotation
        GlobalPosition += movementVector.Rotated(Rotation) * speed * delta;

        // Get the screen size
        var screenSize = GetViewportRect().Size;

        // If the asteroid goes off the screen, respawn at the opposite edge
        if (GlobalPosition.y < 0)
        {
            GlobalPosition = new Vector2(GlobalPosition.x, screenSize.y);
        }
        else if (GlobalPosition.y > screenSize.y)
        {
            GlobalPosition = new Vector2(GlobalPosition.x, 0);
        }

        if (GlobalPosition.x < 0)
        {
            GlobalPosition = new Vector2(screenSize.x, GlobalPosition.y);
        }
        else if (GlobalPosition.x > screenSize.x)
        {
            GlobalPosition = new Vector2(0, GlobalPosition.y);
        }
    }

    // Method to trigger asteroid explosion
    public void Explode()
    {
        // Emit the signal
        EmitSignal(nameof(Exploded));

        // Free the asteroid from the scene
        QueueFree();
    }

    // Called when another area enters this one (such as a laser)
    private void _on_Area2D_area_entered(Area2D area)
    {
        if (area is Laser)
        {
            // Emit the signal and queue free when the laser hits the asteroid
            EmitSignal(nameof(Exploded));
            QueueFree();
        }
    }
}
    