using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;
using System.Linq;

namespace Kyoto
{
    [CreateAssetMenu(fileName = "New WeightedCycleList", menuName = "Kyoto/WeightedCycleList")]
    public class WeightedCycleList : ScriptableObject
    {
        public FloatVariable valueToWatch;
        public List<WeightedCycleNode> list;

        public float Value
        {
            get
            {
                return 1f;
            }
        }

        public void InitCurves()
        {
            // do the first node
            list.First().CreateCurve(list.Last().Key, list.ElementAt(1).Key);
            foreach(WeightedCycleNode node in list)
            {

            }
        }
    }
}
