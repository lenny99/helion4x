using UnityEngine;

namespace Helion4x.Runtime
{
    public interface IAstronomicalObject
    {
        float Mass { get; }
        Transform transform { get; }
    }
}