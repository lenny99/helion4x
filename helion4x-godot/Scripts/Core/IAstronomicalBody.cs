using Godot;

namespace Helion4x.Core
{
    public interface IAstronomicalBody
    {
        float Mass { get; }
        Vector3 Translation { get; }
    }
}