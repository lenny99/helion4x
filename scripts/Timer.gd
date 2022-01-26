extends Timer

signal timewarp_timeout()

enum Timewarp {SLOW, NORMAL, FASTER, FASTEST}

export (Timewarp) var timewarp = Timewarp.NORMAL;

func _on_Timer_timeout():
	emit_signal("timewarp_timeout")


func _on_slower_button_pressed():
	match self.timewarp:
		Timewarp.NORMAL:
			timewarp = Timewarp.SLOW
		Timewarp.FASTER:
			timewarp = Timewarp.NORMAL
		Timewarp.FASTEST:
			timewarp = Timewarp.FASTER
	set_timewarp(timewarp)


func _on_faster_button_pressed():
	match self.timewarp:
		Timewarp.SLOW:
			timewarp = Timewarp.NORMAL
		Timewarp.NORMAL:
			timewarp = Timewarp.FASTER
		Timewarp.FASTER:
			timewarp = Timewarp.FASTEST
	set_timewarp(timewarp)


func set_timewarp(timewarp):
	match timewarp:
		Timewarp.SLOW:
			wait_time = 2
		Timewarp.NORMAL:
			wait_time = 1
		Timewarp.FASTER:
			wait_time = 0.4
		Timewarp.FASTEST:
			wait_time = 0.1
	self.timewarp = timewarp
	$time_menu/time_panel/grid/timewarp_container/timewarp.text = Timewarp.keys()[timewarp]	

