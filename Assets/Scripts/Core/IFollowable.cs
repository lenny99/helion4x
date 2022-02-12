using UnityEngine;

namespace Helion4x.Core
{
    public interface IFollowable
    {
        public Transform FollowTransform { get; }
    }
}