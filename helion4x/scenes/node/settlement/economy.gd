extends Node

signal projects_updated(projects)
signal project_is_finished(project)

var gdp: float
var last_gdp: float
var gdp_per_capita: float = 2500
var tax: float
var budget: float

func _ready():
	emit_signal("projects_updated", $projects/available.get_children()) 


func calculate_gdp(population: float) -> float:
	last_gdp = gdp
	gdp = (population * gdp_per_capita) * 1.02
	return gdp


func available_jobs(population: float) -> float:
	return calculate_gdp(population) / population


func get_growth() -> float:
	if gdp == 0 or last_gdp == 0:
		return 0.0
	return last_gdp / gdp


func progress_projects():
	budget = _calculate_budget()
	for project in $projects/in_progress.get_children():
		if budget <= 0:
			return
		_progress_project(project)


func _calculate_budget() -> float:
	return gdp * tax / 365
	

func _progress_project(project: Project):
	var progress = project.cost - budget
	if progress > 0:
		project.progress = progress
	else:
		project.progress = project.cost
		budget -= progress
	if project.is_finished():
		$infrastructure.add_child(project.finish())
		emit_signal("project_is_finished", project)
