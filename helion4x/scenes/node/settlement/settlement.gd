class_name Settlement extends Node

signal state_changed(state)
signal projects_updated(projects)

export var population: float = 0
export var gdp_per_capita: float = 0
export var tax: float = 0.1


func _ready():
	self.add_to_group("Timeables")
	$economy.gdp_per_capita = gdp_per_capita
	state_change()


func state_change():
	emit_signal("state_changed", {
		"population": population, 
		"gdp": $economy.calculate_gdp($population.GetPopulation()), 
		"tax": $economy.tax, 
		"growth": $economy.get_growth(), 
		"budget": $economy.budget
	})


func _time_process(intervall):
	if intervall == 'Day':
		var population = $population.GetPopulation()
		$economy.calculate_gdp(population)
		$economy.progress_projects()
		$infrastructure.get_boni()
	if intervall == 'Year':
		$population.GrowPopulation()
	state_change()


func _on_economy_project_is_finished(project):
	pass # Replace with function body.


func _on_economy_projects_updated(projects):
	emit_signal("projects_updated", projects)
