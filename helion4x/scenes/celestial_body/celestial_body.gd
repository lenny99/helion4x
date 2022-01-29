class_name CelestialBody extends Spatial

export var parent_node_path: NodePath
export var mass: float

var parent: Spatial
var distance_from_parent: float
var theta: float
var orbital_period_in_hours: float
var is_orbiting: bool = false


func _ready():
	if parent_node_path.is_empty():
		return
	parent = self.get_node(parent_node_path) as Spatial
	if parent == null:
		return
	is_orbiting = true
	distance_from_parent = parent.translation.distance_to(self.translation)
	var orbital_period = $orbital_calculator.calculate_orbital_period(distance_from_parent, parent.mass)
	orbital_period_in_hours = orbital_period / 60 / 60


func _time_process(intervall):
	if intervall == 'Hour':
		process_day()
		

func process_day():
	if is_orbiting:
		self.translation = calculate_next_position(24)


func calculate_next_position(hours: float) -> Vector3:
	theta -= 2 * PI / orbital_period_in_hours * hours
	if (abs(theta) > 2 * PI):
		theta += 2 * PI
	return Vector3(cos(theta), 0, sin(theta)) * distance_from_parent


