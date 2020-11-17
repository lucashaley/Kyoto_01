using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace Kyoto
{
    public class Sun : MonoBehaviour
    {
        [SerializeField]
        private ColorVariable sun = default;
        private Light sunlight;

        void Start()
        {
            sunlight = GetComponent<Light>();
        }

        public void ChangeSun()
        {
            sunlight.color = sun.Value;
        }
    }
}
