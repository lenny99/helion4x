extends Node


func get_boni() -> Array:
	var boni = []
	for infrastructure in get_children():
		boni.append(infrastructure.apply()) 
	return boni
