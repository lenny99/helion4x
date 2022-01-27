extends Timer

signal timewarp_timeout()

enum Timewarp {PAUSED, SLOWEST, SLOW, NORMAL, FASTER, FASTEST}

export (Timewarp) var timewarp = Timewarp.NORMAL;

var pre_pause_timewarp = Timewarp.NORMAL

func _on_Timer_timeout():
	emit_signal("timewarp_timeout")


func _input(event):
	if event.is_action_pressed("pause"):
		pause()


func pause():
	if paused:
		timewarp = pre_pause_timewarp
		set_timewarp(timewarp)
	else:
		pre_pause_timewarp = timewarp
		timewarp = Timewarp.PAUSED
		set_timewarp(timewarp)


func _on_slower_button_pressed():
	match self.timewarp:
		Timewarp.SLOWEST:
			timewarp = Timewarp.PAUSED
		Timewarp.SLOW:
			timewarp = Timewarp.SLOWEST
		Timewarp.NORMAL:
			timewarp = Timewarp.SLOW
		Timewarp.FASTER:
			timewarp = Timewarp.NORMAL
		Timewarp.FASTEST:
			timewarp = Timewarp.FASTER
	set_timewarp(timewarp)


func _on_faster_button_pressed():
	match self.timewarp:
		Timewarp.PAUSED:
			timewarp = Timewarp.SLOWEST
		Timewarp.SLOWEST:
			timewarp = Timewarp.SLOW
		Timewarp.SLOW:
			timewarp = Timewarp.NORMAL
		Timewarp.NORMAL:
			timewarp = Timewarp.FASTER
		Timewarp.FASTER:
			timewarp = Timewarp.FASTEST
	set_timewarp(timewarp)


func set_timewarp(timewarp):
	match timewarp:
		Timewarp.PAUSED:
			paused = true
		Timewarp.SLOWEST:
			set_wait_time(4)
		Timewarp.SLOW:
			set_wait_time(2)
		Timewarp.NORMAL:
			set_wait_time(1)
		Timewarp.FASTER:
			set_wait_time(0.4)
		Timewarp.FASTEST:
			set_wait_time(0.1)
	self.timewarp = timewarp
	$time_panel/grid/timewarp_container/timewarp.text = Timewarp.keys()[timewarp]	


func set_wait_time(time: float):
	paused = false
	wait_time = time
