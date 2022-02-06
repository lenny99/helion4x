using Godot;

namespace Helion4x.Scripts
{
    public interface IEventInput
    {
        void OnInputEvent(Node camera, InputEvent @event, Vector3 position, Vector3 normal, int shapeIndex);
    }
}