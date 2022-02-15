using Godot;

namespace Helion4x.Runtime
{
    public class Selectable : StaticBody
    {
        private bool _isSelected;
        public Environment Environment { get; private set; }
        public Settlement Settlement { get; private set; }

        public bool HasSettlement => Settlement != null;
        public bool HasEnvironment => Environment != null;

        public override void _Ready()
        {
            Connect("input_event", this, nameof(OnInputEvent));
        }

        public void OnInputEvent(Object camera, InputEvent @event, Vector3 position, Vector3 normal,
            int shapeIdx)
        {
            if (!@event.is) return;
            foreach (var child in GetParent().GetChildren())
            {
                if (child is Settlement settlement)
                    Settlement = settlement;
                if (child is Environment planet)
                    Environment = planet;
            }

            _isSelected = true;
            EventBus.Select(this);
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (_isSelected)
                EventBus.Unselect(this);
        }
    }

    public class Environment
    {
        public Environment(float temperature)
        {
            Temperature = temperature;
        }

        public float Temperature { get; }
    }
}