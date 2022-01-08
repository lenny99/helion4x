extends RigidBody
class_name CelestialBody

const G = 6.67430

export var has_colony: bool = false
export var parent_body: NodePath
export var iterations = 50

func _ready():
	$PlanetInterface/PopupMenu/Fleets.connect("pressed", self, "open_fleet_view")

func _process(delta):
	if parent_body != null:
		var parent = self.get_node(parent_body)
		if parent != null and typeof(parent) == typeof(self):
			var acceleration = _calculate_acceleration(delta, parent)
			self.linear_velocity += acceleration * delta
			var next_position: Vector3 = self.translation

func _calculate_acceleration(delta: float, parent: RigidBody):
	var square_distance: float = parent.translation.distance_squared_to(self.translation)
	var force_direction: Vector3 = (parent.translation - self.translation).normalized()
	var force: Vector3 = force_direction * G * mass * parent.mass / square_distance
	var acceleration = force / mass
	return acceleration

func open_planet_window(location: Vector2):
	$PlanetInterface/PopupMenu.set_position(location)
	$PlanetInterface/PopupMenu.popup()

func open_fleet_view():
	print("opening")
	pass
