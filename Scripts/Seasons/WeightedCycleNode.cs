using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace Kyoto
{
    [CreateAssetMenu(fileName = "New_WeightedCycleNode", menuName = "Kyoto/WeightedCycleNode")]
    public class WeightedCycleNode : ScriptableObject
    {
        public FloatVariable valueToWatch;
        [SerializeField] private float key = default;
        [SerializeField] private bool isConstant = false;
        public AnimationCurve curve;

        public float Key
        {
            get => key;
        }

        public float Weight
        {
            get => curve.Evaluate(valueToWatch.Value);
        }

        public void CreateCurve(float prev, float next)
        {
            // set looping
            curve.preWrapMode = WrapMode.Loop;
            curve.postWrapMode = WrapMode.Loop;

            // create key for this season
            curve.AddKey(key, 1f);

            // create keys for surrounding nodes
            if (prev > 0)
            {
                curve.AddKey(prev, 0f);
                // curve.AddKey(0f, 0f);
            } else {
                curve.AddKey(1+prev, 0f);
                curve.AddKey(prev, 0f);
            }

            if (next < 1)
            {
                curve.AddKey(next, 0f);
                // curve.AddKey(1f, 0f);
            } else {
                curve.AddKey(next-1, 0f);
                curve.AddKey(next, 0f);
            }

            if (prev > 0 && next < 1)
            {
                curve.AddKey(0f, 0f);
                curve.AddKey(1f, 0f);
            }

            // set all keys to flat
            // because Unity keys is by-value, we need to copy and replace
            Keyframe[] tempKeys = curve.keys;
            for (int i = 0; i < tempKeys.Length; i++)
            {
                tempKeys[i].inTangent = isConstant ? Mathf.Infinity : 0f;
                tempKeys[i].outTangent = isConstant ? Mathf.Infinity : 0f;
            }
            curve.keys = tempKeys;
        }
    }
}
