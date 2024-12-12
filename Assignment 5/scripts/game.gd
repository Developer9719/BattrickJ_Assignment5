extends Node

# Refrences 
@onready var lasers = $Lasers
@onready var player = $Player
@onready var asteroids = $Asteroids

func _ready():
	player.connect("laser_shot", _on_player_laser_shot)
	
	for asteroid in asteroids.get_children():
		asteroid.connect("exploded", _on_asteroid_exploded)

# Reload the scene on button press
func _process(delta): 
	if Input.is_action_just_pressed("reset"):
		get_tree().reload_current_scene()


func _on_player_laser_shot(laser):
	lasers.add_child(laser) 
	
func _on_asteroid_exploded(asteroid):
	# Remove asteroid from the scene and its parent container
	asteroid.queue_free()
	asteroids.remove_child(asteroid)
