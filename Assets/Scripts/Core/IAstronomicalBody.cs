using UnityEngine;

namespace Helion4x.Runtime
{
    public interface IAstronomicalBody
    {
        float Mass { get; }
        Transform transform { get; }
    }
}