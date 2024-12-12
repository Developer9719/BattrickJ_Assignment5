class_name Asteroid extends Area2D

signal exploded

# Sets up direction and speed
var movement_vector := Vector2(0, -1)
var speed := 150

# Sets up random rotation
func _ready():
	rotation = randf_range(0, 2*PI)

# Moves the asteroid
func _physics_process(delta):
	global_position += movement_vector.rotated(rotation) * speed * delta
	
	# If the asteroid goes off the top of the screen respawn at bottom
	var screen_size = get_viewport_rect().size
	if global_position.y < 0:
		global_position.y = screen_size.y
	elif global_position.y > screen_size.y:
		global_position.y = 0
	if global_position.x < 0:
		global_position.x = screen_size.x
	elif global_position.x > screen_size.x:
		global_position.x = 0

# Deletes the asteroid
func explode():
	emit_signal("exploded")
	queue_free()


# Deletes the asteroid when a laser collides (modified)
func _on_area_entered(area):
	if area is Laser:
		emit_signal("exploded")  # Emit the signal for game.gd to handle
		queue_free()  # Delete the asteroid
