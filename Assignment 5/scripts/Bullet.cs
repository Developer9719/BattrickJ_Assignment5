using Godot;
using System;

public class partial Laser : Area2D
{
    // Direction vector and speed for the laser
    private Vector2 movementVector = new Vector2(0, -1);

    [Export] // Makes the speed variable editable in the Godot editor
    public double speed = 500;

    // Called every physics frame. 'delta' is the time elapsed since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        // Move the laser based on its rotation
        GlobalPosition += movementVector.Rotated(Rotation) * speed * delta;
    }

    // Called when the laser exits the screen (via the VisibleOnScreenNotifier2D)
    private void _on_VisibleOnScreenNotifier2D_screen_exited()
    {
        // Deletes the laser when it leaves the screen
        QueueFree();
    }
}
