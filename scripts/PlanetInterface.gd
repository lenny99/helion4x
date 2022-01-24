extends Control


func open_popup(location: Vector2):
	$PopupMenu.set_position(location)
	$PopupMenu.popup()


func _on_Fleets_pressed():
	pass # Replace with function body.


func _on_Planet_pressed():
	pass # Replace with function body.


func _on_Settlement_pressed():
	$SettlementView.visible = true
	pass # Replace with function body.
