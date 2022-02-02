class_name Settlement extends StaticBody

signal state_changed(state)

export var population: float = 0
export var gdp: float = 0
var population_incrase: float = 0.02


func _ready():
	set_size()
	state_change()


func state_change():
	emit_signal("state_changed", {"population": population})


func set_size():
	self.scale = get_parent().scale


func _time_process(intervall):
	if intervall == 'Day':
		$economy.process_day()
	if intervall == 'Year':
		population += population * population_incrase
		$economy.process_year()
	state_change()


func _on_economy_project_is_finished(project):
	pass # Replace with function body.
