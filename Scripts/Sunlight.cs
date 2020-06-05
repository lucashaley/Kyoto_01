using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rextester;
// using UnityEngine.Rendering.HighDefinition;

namespace Kyoto
{
    public class Sunlight : MonoBehaviour
    {
        private TimeController timeController;
        public LocationController locationController;

        // public Vector3 sunriseAngle;
        // public Vector3 noonAngle;
        // public Vector3 sunsetAngle;
        // public float sunriseTime;
        // public float sunsetTime;

        // public Light sun;
        // public HDAdditionalLightData sun;
        public Light sun;

        public float altitude, azimuth;
        public Transform altitudeObject, azimuthObject;
        //
        // public AnimationCurve intensityCurve;
        // public AnimationCurve directionCurve;
        //
        // public SeasonSunlight[] seasonSunlights;

        /// <summary>
        /// Assigns component references and TimeController singleton.
        /// </summary>
        void Awake()
        {
            // sun = GetComponent<Light>();
            // sun = GetComponent<HDAdditionalLightData>();
            sun = GetComponentInChildren<Light>();
            locationController = LocationController.Instance;
            timeController = TimeController.Instance;

            azimuthObject = transform.Find("Azimuth");
            altitudeObject = azimuthObject.Find("Altitude");
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

        /// <summary>
        /// Sets the sun color and rotation.
        /// </summary>
        void Update()
        {
            // Set the color of the sun
            sun.color = locationController.weightedSunColor;

            // Rextester version
            // // get from SunPosition code
            // SunPosition.Position position = Rextester.SunPosition.CalculateSunPosition(timeController.getGameDateTime(),locationController.longitude, locationController.latitude);
            // azimuth = (float)position.Azimuth;
            // altitude = (float)position.Altitude;
            //
            // // azimuthObject.Rotate(Vector3.up * azimuth);
            // altitudeObject.localRotation = Quaternion.Euler(Vector3.right * altitude);
            // azimuthObject.localRotation = Quaternion.Euler(Vector3.up * azimuth);

            //  Hack version
            // // sun.intensity = intensityCurve.Evaluate(timeController.gameTimeNormalized);
            // sun.color = locationController.weightedSunColor;
            // transform.rotation = Quaternion.Euler(new Vector3(70f, timeController.gameTimeNormalized * 360, 0f));

            // Hessburg version
            //ensure inputs stay within required limits
            float latitude=Mathf.Clamp(locationController.latitude, -90.0f, 90.0f);
            float longitude=Mathf.Clamp(locationController.longitude, -180.0f, 180.0f);
            float offsetUTC=Mathf.Clamp(locationController.offsetUTC, -12.0f, 14.0f);
            int dayOfYear=Mathf.Clamp(timeController.getGameDateTime().DayOfYear, 1, 366);
            float timeInHours=Mathf.Clamp((float)(timeController.getGameDateTime().Hour + ((float)timeController.getGameDateTime().Minute/60)), 0.0f, 24.00f);

            // calculate azimut & altitute
            float cosLatitude = Mathf.Cos(latitude*Mathf.Deg2Rad);
            float sinLatitude = Mathf.Sin(latitude*Mathf.Deg2Rad);
            float D=DeclinationOfTheSun(dayOfYear);
            float sinDeclination=Mathf.Sin(D);
            float cosDeclination=Mathf.Cos(D);
            float W = 360.0f/365.24f;
            float A = W * (dayOfYear + 10.0f);
            float B = A + (360.0f/Mathf.PI) * 0.0167f * Mathf.Sin(W * (dayOfYear-2) * Mathf.Deg2Rad);
            float C = (A - (Mathf.Atan(Mathf.Tan(B*Mathf.Deg2Rad)/Mathf.Cos(23.44f*Mathf.Deg2Rad))*Mathf.Rad2Deg)) / 180.0f;
            float EquationOfTime = 720.0f * (C - Mathf.Round(C)) / 60.0f;
            float hourAngle = (timeInHours + longitude / (360.0f / 24.0f) - offsetUTC - 12.0f + EquationOfTime) * (1.00273790935f-1.0f/365.25f)*(360.0f / 24.0f)*Mathf.Deg2Rad;

            float azimuth = Mathf.Atan2(-cosDeclination * Mathf.Sin(hourAngle), sinDeclination * cosLatitude - cosDeclination * Mathf.Cos(hourAngle) * sinLatitude);
            if (azimuth<0) azimuth = azimuth + 6.28318530717959f;
            float altitude = Mathf.Asin(sinDeclination * sinLatitude + cosDeclination * Mathf.Cos(hourAngle) * cosLatitude );

            azimuth = azimuth / Mathf.Deg2Rad;
            altitude = altitude / Mathf.Deg2Rad;
            if(altitude<0.0f) altitude=360.0f+altitude;

            // sun.transform.eulerAngles = new Vector3(altitude, azimuth, 0.0f);
            azimuthObject.localRotation = Quaternion.Euler(Vector3.right * azimuth);
            altitudeObject.localRotation =  Quaternion.Euler(Vector3.up * altitude);

            // Debug.Log(azimuth + ", " + altitude);
        }

        public float DeclinationOfTheSun(int dayOfYear)
        {
            float WD = 360.0f/365.24f;
            float AD = WD * (dayOfYear + 10.0f);
            float BD = AD + (360.0f/Mathf.PI) * 0.0167f * Mathf.Sin(WD * (dayOfYear-2) * Mathf.Deg2Rad);
            return -Mathf.Asin(Mathf.Sin(23.44f * Mathf.Deg2Rad) * Mathf.Cos(BD * Mathf.Deg2Rad) );
        }
    }
}
