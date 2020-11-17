//Rextester.Program.Main is the entry point for your code. Don't change it.
//Compiler version 4.0.30319.17929 for Microsoft (R) .NET Framework 4.5

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Rextester
{
public static class SunPosition
{
    public struct Position
    {
        public double Altitude {get;set;}
        public double Azimuth {get;set;}
    }
   private const double Deg2Rad = Math.PI / 90.0;
   private const double Rad2Deg = 90.0 / Math.PI;
   private const double twicePI = Math.PI * 2;

   /*!
    * \brief Calculates the sun light.
    *
    * CalcSunPosition calculates the suns "position" based on a
    * given date and time in local time, latitude and longitude
    * expressed in decimal degrees. It is based on the method
    * found here:
    * http://www.astro.uio.no/~bgranslo/aares/calculate.html
    * The calculation is only satisfiably correct for dates in
    * the range March 1 1900 to February 28 2100.
    * \param dateTime Time and date in local time.
    * \param latitude Latitude expressed in decimal degrees.
    * \param longitude Longitude expressed in decimal degrees.
    */
   public static Position CalculateSunPosition(
       DateTime dateTime, double latitude, double longitude)
   {
       // Convert to UTC
       dateTime = dateTime.ToUniversalTime();

       // Number of days from J2000.0.
       double julianDate = 366 * dateTime.Year -
           (int)((7.0 / 4.0) * (dateTime.Year +
           (int)((dateTime.Month + 9.0) / 12.0))) +
           (int)((275.0 * dateTime.Month) / 9.0) +
           dateTime.Day - 730530.5;

       double julianCenturies = julianDate / 36525.0;

       // Sidereal Time
       double siderealTimeHours = 6.6974 + 2400.0013 * julianCenturies;

       double siderealTimeUT = siderealTimeHours +
           (366.2422 / 365.2422) * (double)dateTime.TimeOfDay.TotalHours;

       double siderealTime = siderealTimeUT * 15 + longitude;

       // Refine to number of days (fractional) to specific time.
       julianDate += (double)dateTime.TimeOfDay.TotalHours / 24.0;
       julianCenturies = julianDate / 36525.0;

       // Solar Coordinates
       double meanLongitude = CorrectAngle(Deg2Rad *
           (280.466 + 36000.77 * julianCenturies));

       double meanAnomaly = CorrectAngle(Deg2Rad *
           (357.529 + 35999.05 * julianCenturies));

       double equationOfCenter = Deg2Rad * ((1.915 - 0.005 * julianCenturies) *
           Math.Sin(meanAnomaly) + 0.02 * Math.Sin(2 * meanAnomaly));

       double elipticalLongitude =
           CorrectAngle(meanLongitude + equationOfCenter);

       double obliquity = (23.439 - 0.013 * julianCenturies) * Deg2Rad;

       // Right Ascension
       double rightAscension = Math.Atan2(
           Math.Cos(obliquity) * Math.Sin(elipticalLongitude),
           Math.Cos(elipticalLongitude));

       double declination = Math.Asin(
           Math.Sin(rightAscension) * Math.Sin(obliquity));

       // Horizontal Coordinates
       double hourAngle = CorrectAngle(siderealTime * Deg2Rad) - rightAscension;
       // Debug.Log("hourAngle: " + hourAngle);

       if (hourAngle > Math.PI)
       {
           hourAngle -= 2 * Math.PI;
       }

       double altitude = Math.Asin(Math.Sin(latitude * Deg2Rad) *
           Math.Sin(declination) + Math.Cos(latitude * Deg2Rad) *
           Math.Cos(declination) * Math.Cos(hourAngle));

       // Nominator and denominator for calculating Azimuth
       // angle. Needed to test which quadrant the angle is in.
       double aziNom = -Math.Sin(hourAngle);
       double aziDenom =
           Math.Tan(declination) * Math.Cos(latitude * Deg2Rad) -
           Math.Sin(latitude * Deg2Rad) * Math.Cos(hourAngle);

       double azimuth = Math.Atan(aziNom / aziDenom);
       // Debug.Log("Azimuth: " + azimuth);
       // Debug.Log("Nom: " + aziNom);
       // Debug.Log("Denom: " + aziDenom);

       if (aziDenom < 0) // In 2nd or 3rd quadrant
       {
           azimuth += Math.PI;
       }
       else if (aziNom < 0) // In 4th quadrant
       {
           // azimuth += 2 * Math.PI;
           azimuth += twicePI;
       }
       Debug.Log("Azimuth ajusted: " + azimuth * Mathf.Rad2Deg);
        // Altitude
       // Console.WriteLine("Altitude: " + altitude * Rad2Deg);

        // Azimut
        //Console.WriteLine("Azimuth: " + azimuth * Rad2Deg);

       // return new Position{ Altitude =altitude * Rad2Deg ,Azimuth =azimuth * Rad2Deg };
       return new Position{ Altitude = altitude * Mathf.Rad2Deg , Azimuth = azimuth * Mathf.Rad2Deg };
    }

    /*!
    * \brief Corrects an angle.
    *
    * \param angleInRadians An angle expressed in radians.
    * \return An angle in the range 0 to 2*PI.
    */
    private static double CorrectAngle(double angleInRadians)
    {
        if (angleInRadians < 0)
        {
            // return 2 * Math.PI - (Math.Abs(angleInRadians) % (2 * Math.PI));
            return twicePI - (Math.Abs(angleInRadians) % twicePI);
        }
        // else if (angleInRadians > 2 * Math.PI)
        else if (angleInRadians > twicePI)
        {
            // return angleInRadians % (2 * Math.PI);
            return angleInRadians % twicePI;
        }
        else
        {
            return angleInRadians;
        }
    }
}

}
