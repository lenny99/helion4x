extends Control

signal close_button_pressed()

var drag_position

func _on_exit_button_pressed():
	emit_signal("close_button_pressed")


func _on_window_gui_input(event):
	if event is InputEventMouseButton:
		if event.pressed:
			drag_position = get_global_mouse_position() - rect_global_position
		else:
			drag_position = null
	if event is InputEventMouseMotion and drag_position:
		rect_global_position = get_global_mouse_position() - drag_position


func _on_title_bar_gui_input(event):
	self._on_window_gui_input(event)
