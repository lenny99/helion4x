extends Control

signal progress_intervall()
signal speed_up()
signal slow_down()


func _on_UniverseTime_UpdateUI(time):
	$TimePanel/Grid/Date/Text.text = time
	pass # Replace with function body.


func _on_Slower_pressed():
	emit_signal("slow_down")
	pass # Replace with function body.


func _on_Faster_pressed():
	emit_signal("speed_up")
	pass # Replace with function body.


func _on_Timer_timewarp_changed(timewarp):
	$TimePanel/Grid/TimeWarp/Timewarp.text = str(timewarp)
