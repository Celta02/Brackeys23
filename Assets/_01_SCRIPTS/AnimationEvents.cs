using System;
using UnityEngine;

namespace CeltaGames
{
    public class AnimationEvents : MonoBehaviour
    {
        public event Action BreaststrokeImpulse = delegate{};
        public event Action StartTurningAround = delegate{};
        public event Action StopTurningAround = delegate{};
        public event Action StopSwimming = delegate{};

        public void BreaststrokeImpulseTrigger() => BreaststrokeImpulse?.Invoke();
        public void StartTurningAroundTrigger() => StartTurningAround?.Invoke();
        public void StopTurningAroundTrigger() => StopTurningAround?.Invoke();
        public void StopSwimmingTrigger() => StopSwimming?.Invoke();
    }
}