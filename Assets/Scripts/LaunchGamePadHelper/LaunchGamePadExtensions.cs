using UnityEngine;
using StickStates = GamePadController.Controller.StickStates;

namespace LaunchGamePadHelper
{
    public static class LaunchGamePadExtensions
    {
        public static Vector3 GetVector3(this StickStates state)
        {
            return new Vector3(state.X, state.Y);
        }

        public static Vector2 GetVector2(this StickStates state)
        {
            return new Vector2(state.X, state.Y);
        }
    }
}