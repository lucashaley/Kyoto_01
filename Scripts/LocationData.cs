using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyoto
{
    [CreateAssetMenu(fileName = "New LocationData", menuName = "Kyoto/Location Data", order = 51)]
    public class LocationData : ScriptableObject
    {
        [SerializeField] private string locationName;
        [SerializeField] private float longitude, latitude;
        [SerializeField] private float offsetUTC;

        public float Longitude
        {
            get => longitude;
        }

        public float Latitude
        {
            get => latitude;
        }

        public float OffsetUTC
        {
            get => offsetUTC;
        }
    }
}
