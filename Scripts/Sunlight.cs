using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Rendering.HighDefinition;

namespace Kyoto
{
    public class Sunlight : MonoBehaviour
    {
        private TimeController timeController;

        // public Vector3 sunriseAngle;
        // public Vector3 noonAngle;
        // public Vector3 sunsetAngle;
        // public float sunriseTime;
        // public float sunsetTime;

        // public Light sun;
        // public HDAdditionalLightData sun;
        public Light sun;
        public LocationController location;
        //
        // public AnimationCurve intensityCurve;
        // public AnimationCurve directionCurve;
        //
        // public SeasonSunlight[] seasonSunlights;

        // Start is called before the first frame update
        void Start()
        {
            // sun = GetComponent<Light>();
            // sun = GetComponent<HDAdditionalLightData>();
            sun = GetComponent<Light>();
            location = GameObject.Find("Location").GetComponent<LocationController>();
            timeController = TimeController.Instance;
            //
            // // Animation Curve
            // intensityCurve.preWrapMode = WrapMode.Loop;
            // intensityCurve.postWrapMode = WrapMode.Loop;
            //
            // intensityCurve.AddKey(0.3f, 0f);
            // intensityCurve.AddKey(0.5f, 1f);
            // intensityCurve.AddKey(0.8f, 0f);
            //
            // // go through all keys and flatten
            // // because Unity keys is by-value, we need to copy and replace
            // Keyframe[] tempKeys = intensityCurve.keys;
            // for (int i = 0; i < tempKeys.Length; i++)
            // {
            //     tempKeys[i].inTangent = 0f;
            //     tempKeys[i].outTangent = 0f;
            // }
            // intensityCurve.keys = tempKeys;
            //
            // seasonSunlights = GetComponentsInChildren<SeasonSunlight>();
        }

        // Update is called once per frame
        void Update()
        {
            // sun.intensity = intensityCurve.Evaluate(timeController.gameTimeNormalized);
            sun.color = location.weightedSunColor;
            transform.rotation = Quaternion.Euler(Vector3.up * timeController.gameTimeNormalized * 360);
        }
    }
}
