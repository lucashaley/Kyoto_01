using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

// https://suncalc.org/#/lat,lon,zoom/date/time/objectlevel/maptype

namespace Kyoto
{
    /// <summary>
    /// A singleton to store information about current location.
    /// </summary>
    public class LocationController : Singleton<LocationController>
    {
        public LocationData locationData;
        public float longitude, latitude;
        public float offsetUTC;
        public SeasonSunlightController[] sunlightControllers;
        public SeasonAsset[] seasonAssets;
        public Color weightedSunColor;

        // Start is called before the first frame update
        void Start()
        {
            sunlightControllers = GetComponentsInChildren<SeasonSunlightController>();
            seasonAssets = GetComponentsInChildren<SeasonAsset>();

            longitude = locationData.Longitude;
            latitude = locationData.Latitude;
            offsetUTC = locationData.OffsetUTC;
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
