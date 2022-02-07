class_name SpaceOrbitCamera extends OrbitCamera

export var RAY_LENGTH: float = 200
var distance_from_anchor: float

func _ready():
	#far = 10e10
	._ready()

func _process(delta: float):
	._process(delta)
	distance_from_anchor = self.translation.distance_to(_anchor_node.translation)

func _input(event):
	._input(event)
	#if event is InputEventMouseButton and event.button_index == BUTTON_RIGHT and event.pressed:
	#	interact(event)
	if event is InputEventMouseButton:
		mouse_scroll(event)


func interact(event):
	var from = project_ray_origin(event.position)
	var to = from + project_ray_normal(event.position) * RAY_LENGTH
	var hit = get_world().direct_space_state.intersect_ray(from, to)

func mouse_scroll(e: InputEventMouseButton):
	if e.button_index == BUTTON_WHEEL_UP:
		_scroll_speed = -1 * SCROLL_SPEED * distance_from_anchor
	elif e.button_index == BUTTON_WHEEL_DOWN:
		_scroll_speed = 1 * SCROLL_SPEED * distance_from_anchor