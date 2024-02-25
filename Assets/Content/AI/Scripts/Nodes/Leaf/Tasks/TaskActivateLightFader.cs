using UnityEngine;

namespace BehaviorTree
{
    public class TaskActivateLightFader : Node
    {
        private LightFader _lf;
        private float _intensityChange;
        private float _minLightIntensity;
        private float _maxLightIntensity;

        public TaskActivateLightFader(LightFader lf, float intensityChange, float minLightIntensity, float maxLightIntensity)
        {
            _lf = lf;
            _intensityChange = intensityChange;
            _minLightIntensity = minLightIntensity;
            _maxLightIntensity = maxLightIntensity;
        }

        public override NodeState Evaluate()
        {
            _lf.IntensityChange = _intensityChange;
            _lf.MinLightIntensity = _minLightIntensity;
            _lf.MaxLightIntensity = _maxLightIntensity;

            state = NodeState.SUCCESS;
            return state;
        }
    }
}