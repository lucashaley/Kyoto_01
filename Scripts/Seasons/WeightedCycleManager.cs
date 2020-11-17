using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyoto
{
    public class WeightedCycleManager : MonoBehaviour
    {
        public WeightedCycleList list;
        public Gradient gra;

        // Start is called before the first frame update
        void Start()
        {
            list.InitCurves();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
