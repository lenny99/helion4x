class_name Project extends Node

export var project_name: String
export var cost: float
var progress: float

func is_finished():
	return cost == progress

func finish() -> Node:
	return null
