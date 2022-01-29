extends Control


func _on_Planet_pressed():
	pass # Replace with function body.


func _on_Settlement_pressed():
	$settlement.visible = true
	pass # Replace with function body.


func _on_TextureButton_pressed():
	$settlement.visible = false


func _on_settlement_input_event(camera: Camera, event, position, normal, shape_idx):
	var location = camera.unproject_position(position)
	$popup.set_position(location)
	$popup.popup()
