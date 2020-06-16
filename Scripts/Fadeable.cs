using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyoto
{
    public class Fadeable : MonoBehaviour
    {
        public Renderer[] children;
        // public Material[] mats;
        public Material tempMat;
        public bool isFaded;
        public Color outlineColor = Color.green;

        // Start is called before the first frame update
        void Awake()
        {
            // renderers = gameObject.GetComponentsInChildren<Renderer>();
            // mats = new Material[renderers.materials.Length];
            children = GetComponentsInChildren<Renderer>();
            // add outline scripts
            foreach (Renderer rend in children)
            {
                // OutlineRegister outline = rend.gameObject.AddComponent(typeof(OutlineRegister)) as OutlineRegister;
                // outline.OutlineTint = outlineColor;
                // outline.setupPropertyBlock();

                var outline = rend.gameObject.AddComponent<Outline>();
                outline.OutlineMode = Outline.Mode.OutlineAll;
                outline.OutlineColor = Color.yellow;
                outline.OutlineWidth = 0.25f;
                outline.UpdateMaterialProperties();
                // outline.SetPrecomputeOutline(true);
            }
        }

        void toggleFade()
        {
            isFaded = !isFaded;
            // mats[0].SetColor("_Color", tempColor);

            // Renderer[] children;
            // children = GetComponentsInChildren<Renderer>();
            foreach (Renderer rend in children)
            {
                // mats = new Material[rend.materials.Length];
                for (int j = 0; j < rend.materials.Length; j++)
                {
                    Debug.Log(rend.materials[j].name);
                    if (rend.materials[j].shader.name == "Standard")
                    {
                        // shared material?
                        Debug.Log("Standard");
                        tempMat = rend.materials[j];
                        Color tempColor = tempMat.GetColor("_Color");
                        tempColor.a = isFaded ? 0.15f : 1f;
                        rend.materials[j].SetColor("_Color", tempColor);
                    }
                }
                // rend.materials = mats;
                rend.GetComponent<Outline>().OutlineWidth = isFaded ? 1f : 0f;
                rend.GetComponent<Outline>().UpdateMaterialProperties();
            }
        }

        void setFade(bool inFade)
        {

        }
    }
}
