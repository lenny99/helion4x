# GdUnit generated TestSuite
#warning-ignore-all:unused_argument
#warning-ignore-all:return_value_discarded
class_name CelestialBodyextendsSpatialTest
extends GdUnitTestSuite

# TestSuite generated from
const __source = 'res://scenes/celestial_body/celestial_body.gd'


func test_calculate_orbital_period_in_hours() -> void:
	var instance: CelestialBody = CelestialBody.new()
	var orbital_peroid = instance.calculate_orbital_period(26552e3, 5.97e24)
	assert_float(orbital_peroid).is_equal(43079.99507)
	instance.free()
