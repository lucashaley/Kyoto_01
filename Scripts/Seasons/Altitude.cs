using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace Kyoto
{
    public class Altitude : MonoBehaviour
    {
        [SerializeField]
        private FloatVariable altitude = default;

        public void ChangeAltitude()
        {
            transform.localRotation = Quaternion.Euler(Vector3.up * altitude.Value);
        }
    }
}
