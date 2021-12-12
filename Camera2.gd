extends Camera


export var look_at_path: NodePath

onready var look_at_node: Spatial = self.get_node(look_at_path)

func _process(delta):
	self.look_at(look_at_node.translation, Vector3.UP)
	pass
