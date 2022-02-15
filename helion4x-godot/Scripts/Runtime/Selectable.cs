using Godot;
using Helion4x.Util;

namespace Helion4x.Runtime
{
    public class Selectable : StaticBody, ISelectable
    {
        private Player _player;

        public Environment Environment { get; private set; }
        public Settlement Settlement { get; private set; }

        public bool HasSettlement => Settlement != null;
        public bool HasEnvironment => Environment != null;

        public void Select()
        {
            var selectable = new Selectable();
            foreach (var child in GetParent().GetChildren())
            {
                if (child is Settlement settlement)
                    Settlement = settlement;
                if (child is Environment planet)
                    Environment = planet;
            }

            _player.Selectable = selectable;
        }

        public override void _Ready()
        {
            _player = this.GetPlayer();
        }

        public void Unselect()
        {
        }
    }
}