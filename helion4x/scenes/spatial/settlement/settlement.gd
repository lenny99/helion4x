class_name Settlement extends StaticBody

signal state_changed(state)
signal projects_updated(projects)

export var population: float = 0
export var gdp: float = 0
export var tax: float = 0.1
export var gdp_growth: float = 0.1
var population_incrase: float = 0.02


func _ready():
	self.add_to_group("Timeables")
	self.scale = get_parent().scale
	$economy.gdp = gdp
	$economy.growth = gdp_growth
	$economy.tax = tax
	state_change()


func state_change():
	emit_signal("state_changed", {
		"population": population, 
		"gdp": $economy.gdp, 
		"tax": $economy.tax, 
		"growth": $economy.growth, 
		"budget": $economy.budget
	})


func _time_process(intervall):
	if intervall == 'Day':
		$economy.process_day()
		$economy.process_year()
	if intervall == 'Year':
		population += population * population_incrase
	state_change()


func _on_economy_project_is_finished(project):
	pass # Replace with function body.


func _on_economy_projects_updated(projects):
	emit_signal("projects_updated", projects)
