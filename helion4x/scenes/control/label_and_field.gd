extends HBoxContainer

export var label: String = "label: "

func _ready():
	$label.text = label

func set_text(text):
	$edit.text = str(text)
