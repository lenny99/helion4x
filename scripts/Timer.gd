extends Timer

signal timewarp_timeout()
signal timewarp_changed(timewarp)

enum Timewarp {SLOW, NORMAL, FASTER, FASTEST}

export (Timewarp) var timewarp = Timewarp.NORMAL;

func _on_Timer_timeout():
	emit_signal("timewarp_timeout", str(timewarp))
	pass # Replace with function body.


func _on_Menu_slow_down():
	match self.timewarp:
		Timewarp.NORMAL:
			self.timewarp = Timewarp.SLOW
		Timewarp.FASTER:
			self.timewarp = Timewarp.NORMAL
		Timewarp.FASTEST:
			self.timewarp = Timewarp.FASTER
	emit_signal("timewarp_changed", Timewarp.keys()[timewarp])


func _on_Menu_speed_up():
	match self.timewarp:
		Timewarp.SLOW:
			self.timewarp = Timewarp.NORMAL
		Timewarp.NORMAL:
			self.timewarp = Timewarp.FASTER
		Timewarp.FASTER:
			self.timewarp = Timewarp.FASTEST
	emit_signal("timewarp_changed", Timewarp.keys()[timewarp])
	



