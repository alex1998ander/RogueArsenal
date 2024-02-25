using UnityEngine;

namespace BehaviorTree
{
    public class TaskPlayParticleSystem : Node
    {
        private ParticleSystem _ps;

        public TaskPlayParticleSystem(ParticleSystem ps)
        {
            _ps = ps;
        }

        public override NodeState Evaluate()
        {
            if (!_ps.isPlaying)
                _ps.Play();

            state = NodeState.SUCCESS;
            return state;
        }
    }
}