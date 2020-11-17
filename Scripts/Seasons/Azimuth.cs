using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace Kyoto
{
    public class Azimuth : MonoBehaviour
    {
        [SerializeField]
        private FloatVariable azimuth = default;

        public void ChangeAzimuth()
        {
            transform.localRotation = Quaternion.Euler(Vector3.right * azimuth.Value);
        }
    }
}
