extends Control


func _on_Game_StateChanged(state):
	$TimePanel/Grid/Date/Text.text = state.TimeToString()
	$TimePanel/Grid/TimeWarp/Text.text = str(state.timeWarp)
	
