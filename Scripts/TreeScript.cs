using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyoto
{
    public class TreeScript : MonoBehaviour
    {
        public float lifespan;
        public ParticleSystem leaves;
        public TimeController timeController;

        // Start is called before the first frame update
        void Start()
        {
            timeController = TimeController.Instance;
            leaves = GetComponentInChildren<ParticleSystem>();
            var main = leaves.main;

            leaves.Stop(); // Cannot set duration whilst Particle System is playing
            main.startLifetime = lifespan;
            main.duration = lifespan;
            // main.duration = timeController.yearSeconds/timeController.timeScale;
            // main.duration = Mathf.Infinity;
            // main.startLifetime = timeController.yearSeconds/timeController.timeScale;
            // leaves.Simulate(timeController.gameDateNormalized * timeController.yearSeconds/timeController.timeScale);
            leaves.Play();
        }
    }
}
