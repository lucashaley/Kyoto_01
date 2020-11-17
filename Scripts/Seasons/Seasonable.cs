using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Kyoto {
  public class Seasonable : MonoBehaviour
  {
    [SerializeField]
    private KyotoDictionary seasonStates = default;
    public float[] seasonStatesArray;

    public void Start ()
    {
      seasonStatesArray = seasonStates.Keys.ToArray();
      // GetStateForValue(0.115f);
      // GetStateForValue(0.976f);
    }

    public int GetStateForValue (float value)
    {
      int minIndex = 0, maxIndex = 0;
      float min = 0f, max = 0f, percentMin, percentMax;

      for (int i=0; i < seasonStatesArray.Length; i++)
      {
        if (value > seasonStatesArray[i])
        {
          minIndex = i;
          maxIndex = i + 1;
          min = seasonStatesArray[i];
          max = seasonStatesArray[i+1];
        } else {
          // it is less than the first value
          minIndex = seasonStatesArray.Length;
          maxIndex = i;
          min = seasonStatesArray.Last();
          max = seasonStatesArray.First();
        }
      }

      // WHAT ABOUT ABS()?
      // what if value is greater than last state? Need to loop to first value;
      float loopMax = seasonStatesArray.First();
      if (maxIndex == seasonStatesArray.Length)
      {
        loopMax += 1.0f;
        max = seasonStatesArray[0];
      }
      // what if its less than the first?
      float loopMin = seasonStatesArray.Last();
      if (minIndex == seasonStatesArray.Length)
      {

      }

      percentMax = (value-min)/(loopMax-min);
      percentMin = 1.0f - percentMax;
      // Debug.Log(percentMin + " of " + seasonStates[min] + ", " + percentMax + " of " + seasonStates[max]);


      return 1;
    }
  }
}
