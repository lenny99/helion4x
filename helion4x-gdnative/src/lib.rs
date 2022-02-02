use gdnative::prelude::*;
use std::f32::consts::PI;

#[derive(NativeClass)]
#[inherit(Node)]
pub struct OrbitCalculator;

#[methods]
impl OrbitCalculator {
    fn new(_owner: &Node) -> Self {
        OrbitCalculator {}
    }
    #[export]
    fn calculate_orbital_period(
        &self,
        _owner: &Node,
        semi_major_axis: f32,
        parent_mass: f32,
    ) -> f32 {
        return calculate_ellipse_orbital_period(semi_major_axis, parent_mass);
    }
}

const G: f32 = physical_constants::NEWTONIAN_CONSTANT_OF_GRAVITATION as f32;

#[inline]
fn calculate_ellipse_orbital_period(semi_major_axis: f32, parent_mass: f32) -> f32 {
    let dividend = 4. * f32::powi(PI, 2) * f32::powi(semi_major_axis, 3);
    let divisor = G * parent_mass;
    return f32::sqrt(dividend / divisor);
}

fn init(handle: InitHandle) {
    handle.add_class::<OrbitCalculator>();
}

godot_init!(init);

#[cfg(test)]
mod tests {
    use crate::calculate_ellipse_orbital_period;

    #[test]
    fn calcualte_orbital_period() {
        let orbital_period = calculate_ellipse_orbital_period(26552e3, 5.97e24);
        assert_eq!(orbital_period, 43066.117);
    }
}
