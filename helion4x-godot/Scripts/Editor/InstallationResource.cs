using Godot;
using Helion4x.Core.Settlement.Installation;
using MonoCustomResourceRegistry;

namespace Helion4x.Editor
{
    [RegisteredType(nameof(InstallationResource))]
    public class InstallationResource : Resource
    {
        [Export] public InstallationBonus[] Bonuses = { };
        [Export] public int[] Count = { };
        [Export] public string Name;
    }
}