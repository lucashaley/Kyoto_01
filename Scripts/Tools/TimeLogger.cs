using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

[CreateAssetMenu(menuName = "Unity Atoms/Examples/Health Logger")]
public class TimeLogger : FloatAction
{
    public override void Do(float health)
    {
        // Debug.Log("<3: " + health);
    }
}
