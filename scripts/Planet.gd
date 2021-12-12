extends Spatial
class_name CelestialObject

# AU in megameters
const G = 6.67408e-5 # Mm^3 / Mg^1 / s^2
const AU = 69816.9 # Mm
const SOLAR_MASS = 1.99e27 #Mg

export var mass = SOLAR_MASS
export var distance_from_sun: float = 1 # in AU
export var theta = 0
export var is_orbiting = true

var gravitation = mass * G

func _process(delta):
	if is_orbiting:
		orbit(delta, get_parent())
	pass

func orbit(delta: float, parent: CelestialObject):
	var radius = AU * distance_from_sun
	theta -= delta * sqrt(parent.gravitation / pow(radius, 3))
	if (abs(theta) > 2 * PI):
		theta += 2 * PI
	self.translation = Vector3(cos(theta), sin(theta), 0) * radius

func open_planet_window(location: Vector2):
	$Control/PopupMenu.popup(Rect2(location, Vector2(100, 100)))
