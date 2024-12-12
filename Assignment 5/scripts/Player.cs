using Godot;
using System;

public partial class Player : Node2D
{
	// Variables
	public float speed = 200f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        // Creates a direction vector and initializes it to (0,0)
        Vector2 direction = Vector2.Zero;

        // Check for keyboard input 
        if (Input.IsActionPressed("ui_up"))  // W or up arrow key
        {
            direction.y -= 1;
        }
        if (Input.IsActionPressed("ui_down"))  // S or down arrow key
        {
            direction.y += 1;
        }
        if (Input.IsActionPressed("ui_left"))  // A or left arrow key
        {
            direction.x -= 1;
        }
        if (Input.IsActionPressed("ui_right"))  // D or right arrow key
        {
            direction.x += 1;
        }

        // Normalize the direction to prevent faster diagonal movement
        direction = direction.Normalized();

        // Move the player by adjusting its position
        Position += direction * speed * (float)delta;
    }
}
