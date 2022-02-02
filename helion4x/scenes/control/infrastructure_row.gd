class_name ProjectRow extends PanelContainer

var project_name: String
var cost: String

func _init(project: Project):
	self.project_name = project.project_name
	self.cost = str(project.cost)
