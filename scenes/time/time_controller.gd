extends Control



func _on_time_state_changed(state):
	$time_panel/grid/date_container/text.text = state["time"]


func _on_timer_state_changed(state):
	$time_panel/grid/timewarp_container/timewarp.text = state["timewarp"]
