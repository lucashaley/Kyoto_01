using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyoto
{
    public class Raycatcher : MonoBehaviour
    {
        public PlaceableManager placeableManager;

        // // Start is called before the first frame update
        void Awake()
        {
            placeableManager = PlaceableManager.Instance;
        }

        void OnMouseUp()
        {
            Debug.Log("boom");
            placeableManager.Reset();
        }
    }
}
