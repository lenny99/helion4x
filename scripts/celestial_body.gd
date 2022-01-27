extends Spatial
class_name CelestialBody

const G = 6.67430

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


func _calculate_next_position(delta: float) -> Vector3:
	_theta -= delta * _orbital_period / pow(10, 5)
	if (abs(_theta) > 2 * PI):
		_theta += 2 * PI
	return Vector3(cos(_theta), 0, sin(_theta)) * _distance_from_parent


func _time_process(intervall):
	match intervall:
		'HOUR': process_hour()
		'DAY':  process_day()
		'MONTH': process_month()
		'YEAR': process_year()


func process_hour():
	print("hours passed")
	pass


func process_day():
	print("days passed")
	if _is_orbiting:
		self.translation = _calculate_next_position(1)


func process_month():
	print("month passed")
	pass


func process_year():
	print("year passed")
	pass


func open_planet_window(menu_location: Vector2):
	$planet_interface.open_popup(menu_location)

	
