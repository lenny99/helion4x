class_name Project extends Node

var project_name: String
var cost: float
var progress: float

func is_finished():
    return cost == progress

func finish() -> Node:
    return null