using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyoto
{
    public class DiegeticLight : MonoBehaviour
    {
        public TimeController timeController;
        public Light theLight;
        public SeasonSunlightController lightController;

        // Start is called before the first frame update
        void Awake()
        {
            timeController = TimeController.Instance;
            theLight = gameObject.GetComponent<Light>();
            lightController = gameObject.GetComponent<SeasonSunlightController>();
        }

        // Update is called once per frame
        void Update()
        {
            theLight.color = lightController.weightedColor;
        }
    }
}
