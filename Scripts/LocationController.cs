using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyoto
{
    public class LocationController : MonoBehaviour
    {
        public SeasonSunlightController[] sunlightControllers;
        public SeasonAsset[] seasonAssets;
        public Color weightedSunColor;

        // Start is called before the first frame update
        void Start()
        {
            sunlightControllers = GetComponentsInChildren<SeasonSunlightController>();
            seasonAssets = GetComponentsInChildren<SeasonAsset>();
        }

        // Update is called once per frame
        void Update()
        {
            GetSunColor();
        }

        void GetSunColor()
        {
            Color averagedColor = Color.black;

            for (int i = 0; i < sunlightControllers.Length; i++)
            {
                averagedColor += sunlightControllers[i].weightedColor * seasonAssets[i].GetWeight();
            }

            weightedSunColor = averagedColor;
        }
    }
}
