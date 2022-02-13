using Godot;

namespace Helion4x.Core
{
    public interface IFollowable
    {
        Vector3 FollowPosition { get; }
    }
}