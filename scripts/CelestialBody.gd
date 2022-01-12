extends Spatial
class_name CelestialBody

const G = 6.67430

export var has_colony: bool = false
export var parent_node_path: NodePath
export var density = 5210 #kg/m^3


var _parent: Spatial
var _distance_from_parent: float
var _theta: float
var _orbital_period: float
var _is_orbiting: bool = false


func _ready():
	if parent_node_path.is_empty():
		return
	_parent = self.get_node(parent_node_path) as Spatial
	if _parent == null:
		return
	_distance_from_parent = _parent.translation.distance_to(self.translation)
	_orbital_period = sqrt(3*PI/ (G * pow(10, -11) * 5210))
	_is_orbiting = true

		
func _process(delta):
	if _is_orbiting:
		self.translation = _calculate_next_position(delta)
	

func _calculate_next_position(delta: float) -> Vector3:
	_theta -= delta * _orbital_period / pow(10, 5)
	if (abs(_theta) > 2 * PI):
		_theta += 2 * PI
	return Vector3(cos(_theta), 0, sin(_theta)) * _distance_from_parent

func open_planet_window(location: Vector2):
	$PlanetInterface/PopupMenu.set_position(location)
	$PlanetInterface/PopupMenu.popup()


func _on_Fleets_pressed():
	print('fleets pressed')
	pass # Replace with function body.
