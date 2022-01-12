extends Control


func _on_Game_StateChanged(state):
	$TimePanel/Grid/Date/Text.text = state.time
	$TimePanel/Grid/TimeWarp/Text.text = state.TimeToString()
	
