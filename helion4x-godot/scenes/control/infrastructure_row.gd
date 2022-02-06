class_name ProjectRow extends PanelContainer

var project: Project

signal build_button_pressed(project)

func set_project(project: Project):
	self.project = project
	$hbox/name.text = project.project_name
	$hbox/cost.text = str(project.cost)


func _on_build_button_pressed():
	emit_signal("build_button_pressed", project)
