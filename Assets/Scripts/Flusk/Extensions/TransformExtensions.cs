using UnityEngine;

namespace Flusk.Extensions
{
    public static class TransformExtensions
    {
        public static void SetScale(this Transform transform, float scale)
        {
            transform.localScale = Vector3.one * scale;
        }
    }
}