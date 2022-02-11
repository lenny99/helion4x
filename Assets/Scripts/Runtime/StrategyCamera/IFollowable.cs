using UnityEngine;

namespace Helion4x.Runtime
{
    public interface IFollowable
    {
        public Transform FollowTransform { get; }
    }
}