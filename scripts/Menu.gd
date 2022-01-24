extends Control

signal progress_intervall(intervall)

func _on_Hour_pressed():
	emit_signal("progress_intervall", { "Hour": 1 })
	pass # Replace with function body.


func _on_Day_pressed():
	emit_signal("progress_intervall", { "Day": 1 })
	pass # Replace with function body.


func _on_Month_pressed():
	emit_signal("progress_intervall", { "Month": 1})
	pass # Replace with function body.


func _on_Year_pressed():
	emit_signal("progress_intervall", { "Year": 1})
	pass # Replace with function body.


func _on_UniverseTime_UpdateUI(time):
	$TimePanel/Grid/Date/Text.text = time
	pass # Replace with function body.
