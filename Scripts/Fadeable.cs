using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

namespace Kyoto
{
    public class Fadeable : MonoBehaviour
    {
        private GameController gameController;

        public enum RenderMode { Opaque, Cutout, Fade, Transparent };

        public Renderer[] childrenRenderers;
        public bool isFaded;

        // Start is called before the first frame update
        void Awake()
        {
            gameController = GameController.Instance;

            childrenRenderers = GetComponentsInChildren<Renderer>();

            // add outline scripts
            foreach (Renderer rend in childrenRenderers)
            {
                var outline = rend.gameObject.AddComponent<Outline>();
                outline.OutlineMode = Outline.Mode.OutlineAll;
                outline.OutlineColor = gameController.fadeableOutlineColor;
                // outline.OutlineWidth = gameController.fadeableOutlineWidth;
                outline.OutlineWidth = 0f;
                outline.UpdateMaterialProperties();
            }
        }

        void ToggleFade()
        {
            // if (gameController.viewStateController.currentState.name == "RakingState")
            // {
            //     FadeOut();
            // } else {
            //     FadeIn();
            // }

            if (isFaded)
            {
                FadeIn();
            } else {
                FadeOut();
            }
            // Move this to the end
            isFaded = !isFaded;
        }

        void FadeOut()
        {
            // fade out global shadows
            Tween.Value(gameController.shadowDistance, gameController.shadowDistanceFaded, HandleShadowFade, gameController.shadowFadeTime, 0f);

            // go through each child renderer, and start the tween
            foreach (Renderer rend in childrenRenderers)
            {
                // then go through each material, as there may be more than one
                for (int i = 0; i < rend.materials.Length; i++)
                {
                    // check what kind of material it is
                    // we need to do different things to outlines and fills
                    switch (rend.materials[i].shader.name)
                    {
                        case string s when s.Contains("Standard"):
                            // switch to fade render mode
                            // we can do this here, as we loop, to save cycles
                            FlipRendermode(rend.materials[i]);

                            Color tempColor = rend.materials[i].GetColor("_Color");
                            tempColor.a = gameController.fadeableFadeAmount;
                            Tween.Color (rend.materials[i], tempColor, gameController.stateChangeTime, gameController.shadowFadeTime);
                            break;

                        case string s when s.Contains("Outline Fill"):
                            Tween.ShaderFloat (rend.materials[i], "_OutlineWidth", gameController.fadeableOutlineWidth, gameController.stateChangeTime, gameController.shadowFadeTime);
                            break;

                        case string s when s.Contains("Universal"):
                            Debug.LogError("URP Material!", rend.materials[i]);
                            break;
                    }
                }
            }
        }
        void FadeIn()
        {
            //go through each child renderer, and start the tween
            foreach (Renderer rend in childrenRenderers)
            {
                // then go through each material, as there may be more than one
                for (int i = 0; i < rend.materials.Length; i++)
                {
                    // check what kind of material it is
                    // we need to do different things to outlines and fills
                    switch (rend.materials[i].shader.name)
                    {
                        case string s when s.Contains("Standard"):
                            Color tempColor = rend.materials[i].GetColor("_Color");
                            tempColor.a = 1f;
                            Tween.Color (rend.materials[i], tempColor, gameController.stateChangeTime, 0f, null, Tween.LoopType.None, null, FlipRendermodeAll);
                            // switch to fade render mode
                            // we can't do this here, as it cuts off the fade
                            // so we put it all in a callback
                            // it's extra cycles, but hey
                            // FlipRendermode(rend.materials[i]);
                            break;

                        case string s when s.Contains("Outline Fill"):
                            Tween.ShaderFloat (rend.materials[i], "_OutlineWidth", 0f, gameController.stateChangeTime, 0f);
                            break;

                        case string s when s.Contains("Universal"):
                            Debug.LogError("URP Material!", rend.materials[i]);
                            break;
                    }
                }
            }

            // fade in global shadows
            Tween.Value(gameController.shadowDistanceFaded, gameController.shadowDistance, HandleShadowFade, gameController.shadowFadeTime, gameController.stateChangeTime);
        }

        void FlipRendermodeAll()
        {
            foreach (Renderer rend in childrenRenderers)
            {
                for (int i = 0; i < rend.materials.Length; i++)
                {
                    FlipRendermode(rend.materials[i]);
                }
            }
        }

        void FlipRendermode(Material mat)
        {
            if (mat.shader.name.Contains("Standard"))
            {
                int currentMode = (int)mat.GetFloat("_Mode");

                switch (currentMode)
                {
                    case (int)RenderMode.Opaque:
                        // from https://forum.unity.com/threads/access-rendering-mode-var-on-standard-shader-via-scripting.287002/
                        mat.SetFloat("_Mode", (float)RenderMode.Fade);
                        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                        mat.SetInt("_ZWrite", 0);
                        mat.DisableKeyword("_ALPHATEST_ON");
                        mat.EnableKeyword("_ALPHABLEND_ON");
                        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                        mat.renderQueue = 3000;

                        break;

                    case (int)RenderMode.Fade:
                        // from https://forum.unity.com/threads/access-rendering-mode-var-on-standard-shader-via-scripting.287002/
                        mat.SetFloat("_Mode", (float)RenderMode.Opaque);
                        // mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                        // mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                        mat.SetInt("_ZWrite", 1);
                        mat.EnableKeyword("_ALPHATEST_ON");
                        mat.DisableKeyword("_ALPHABLEND_ON");
                        mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                        mat.renderQueue = -1;

                        break;
                }
            }
        }

        void HandleShadowFade(float value)
        {
            QualitySettings.shadowDistance = value;
        }
    }
}
