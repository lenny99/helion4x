extends Node

signal projects_updated(projects)
signal project_is_finished(project)

var gdp: float = 0
var growth: float = 0
var tax: float = 0
var budget: float = 0


func _ready():
	emit_signal("projects_updated", $projects.get_children()) 


func process_day():
	budget = calculate_budget(365)
	progress_projects()


func calculate_budget(days: int) -> float:
	return gdp * tax / days


func progress_projects():
	for project in $projects.get_children():
		if budget <= 0:
			return
		progress_project(project)	
	

func progress_project(project: Project):
	var progress = project.cost - budget
	if progress > 0:
		project.progress = progress
	else:
		project.progress = project.cost
		budget -= progress
	if project.is_finished():
		$infrastructure.add_child(project.finish())
		emit_signal("project_is_finished", project)
	

func process_year():
	gdp *= growth + 1

