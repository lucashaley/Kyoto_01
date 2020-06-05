using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyoto
{
    public class Fadeable : MonoBehaviour
    {
        public Renderer[] children;
        public Material[] mats;
        public Material tempMat;
        public bool isFaded;

        // Start is called before the first frame update
        void Awake()
        {
            // renderers = gameObject.GetComponentsInChildren<Renderer>();
            // mats = new Material[renderers.materials.Length];
        }

        void toggleFade()
        {
            isFaded = !isFaded;
            // mats[0].SetColor("_Color", tempColor);

            // Renderer[] children;
            children = GetComponentsInChildren<Renderer>();
            foreach (Renderer rend in children)
            {
                mats = new Material[rend.materials.Length];
                for (int j = 0; j < rend.materials.Length; j++)
                {
                    // shared material?
                    tempMat = rend.materials[j];
                    Color tempColor = tempMat.GetColor("_Color");
                    tempColor.a = isFaded ? 0.5f : 1f;
                    rend.materials[j].SetColor("_Color", tempColor);
                }
                // rend.materials = mats;
            }

        }

        void setFade(bool inFade)
        {

        }
    }
}
