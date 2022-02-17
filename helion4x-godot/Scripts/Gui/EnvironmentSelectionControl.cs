using Godot;
using Environment = Helion4x.Runtime.Environment;

namespace Helion4x.Gui
{
    public class EnvironmentSelectionControl : PanelContainer
    {
        public Environment Environment { get; set; }
    }
}