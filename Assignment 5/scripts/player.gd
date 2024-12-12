extends CharacterBody2D 
 
signal laser_shot(laser)

@export var acceleration := 10.0
@export var max_speed := 350.0
@export var rotation_speed := 250.0

# References the muzzle node
@onready var muzzle = $Muzzle

var laser_scene = preload("res://scenes/laser.tscn")

# Sets up the laser fire rate 
var shoot_cd = false
func _process(delta):
	if Input.is_action_pressed("shoot"):
		if !shoot_cd:
			shoot_cd = true
			shoot_laser()
			await get_tree().create_timer(0.1).timeout
			shoot_cd = false
 
func _physics_process(delta):
	# Basic keyboard movements
	var input_vector := Vector2(0, Input.get_axis("move_forward", "move_backward"))
	velocity += input_vector.rotated(rotation) * acceleration
	
	# Speed Limiter
	velocity = velocity.limit_length(max_speed)
	
	# Rotation
	if Input.is_action_pressed("rotate_right"):
		rotate(deg_to_rad(rotation_speed*delta))
	if Input.is_action_pressed("rotate_left"):
		rotate(deg_to_rad(-rotation_speed*delta))
	
	# Stop the player movement
	if input_vector.y == 0:
		velocity = velocity.move_toward(Vector2.ZERO, 3)
	
	move_and_slide()
	
	# If the player goes off the top of the screen respawn at bottom
	var screen_size = get_viewport_rect().size
	if global_position.y < 0:
		global_position.y = screen_size.y
	elif global_position.y > screen_size.y:
		global_position.y = 0
	if global_position.x < 0:
		global_position.x = screen_size.x
	elif global_position.x > screen_size.x:
		global_position.x = 0

# Create instance of laser and shoot it in the right direction and rotation
func shoot_laser():
	var l = laser_scene.instantiate()
	l.global_position = muzzle.global_position
	l.rotation = rotation
	emit_signal("laser_shot", l)
