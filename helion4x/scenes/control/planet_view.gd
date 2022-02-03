extends Control

func _ready():
	hide()


func _on_settlement_input_event(camera: Camera, event, position, normal, shape_idx):
	if Input.is_mouse_button_pressed(1):
		var location = camera.unproject_position(position)
		show()


func _on_window_close_button_pressed():
	hide()


func _on_settlement_state_changed(state):
	$window/tab_container/economy/gdp.set_text(state["gdp"])
	$window/tab_container/economy/growth.set_text(state["growth"])
	$window/tab_container/economy/tax.set_text(state["tax"])
	$window/tab_container/economy/budget.set_text(state["budget"])


func _on_settlement_projects_updated(projects):
	for project in projects:
		var row: Node = load("res://scenes/control/infrastructure_row.tscn").instance()
		row.set_project(project)
		row.connect("build_button_pressed", self, "_on_build_button_pressed")
		$window/tab_container/project/table.add_child(row)

func _on_build_button_pressed(project: Project):
	pass
