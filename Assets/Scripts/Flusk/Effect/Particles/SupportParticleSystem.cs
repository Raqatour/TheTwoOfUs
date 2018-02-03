using Flusk.DataHelp;
using Flusk.Extensions;
using Flusk.Structures;
using UnityEngine;

namespace Flusk.Effect.Particles
{
    public class SupportParticleSystem: MonoBehaviour
    {
        public enum State
        {
            Calm,
            Pulsing
        }
        private Pulse pulse;
        
        public State Current { get; private set; }

        public void SetState(State state)
        {
            if (state == Current)
            {
                return;
            }
            Current = state;
            if (pulse == null)
            {
                pulse = GetComponent<Pulse>();
            }
            pulse.enabled = Current == State.Pulsing;
        }

        protected virtual void Awake()
        {
            pulse = GetComponent<Pulse>();
        }
    }
}