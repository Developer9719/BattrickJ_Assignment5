extends Area2D

var movement_vector := Vector2(0, -1)
@export var speed := 500.0

func _physics_process(delta):
	# Sets movement of the laser
	global_position += movement_vector.rotated(rotation) * speed * delta


func _on_visible_on_screen_notifier_2d_screen_exited() -> void:
	# Deletes the laser as it leaves the screen
	queue_free()
