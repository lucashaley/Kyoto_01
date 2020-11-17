using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Kyoto
{
    public class DebugGizmo : MonoBehaviour
    {
        public bool width, height, depth;
        public Color displayColor;
        public float scale = 1f;

        void OnDrawGizmos()
        {
            Vector3 cube = new Vector3(Convert.ToSingle(width)*scale, Convert.ToSingle(height)*scale, Convert.ToSingle(depth)*scale);
            Gizmos.color = displayColor;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(transform.position, cube);
        }
    }
}
