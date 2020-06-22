using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyoto
{
    public class Tile : KyotoBase
    {
        public enum TileEdge {
            None    = 0,
            Top     = 1,
            Left    = 2,
            Bottom  = 4,
            Right   = 7
        }

        public ViewStateController viewState;
        public BoxCollider tileCollider;
        public Vector3 boundsCenter, boundsMin, boundsMax;
        public TileEdge enterEdge, exitEdge;

        public Texture TopBottom, LeftRight;
        public Texture BottomLeft, BottomRight, TopLeft, TopRight;

        protected override void Initialize()
        {
            // refactor
            viewState = GameObject.Find("GameController").GetComponent<ViewStateController>();
            tileCollider = GetComponent<BoxCollider>();
            boundsCenter = tileCollider.bounds.center;
            boundsMin  = tileCollider.bounds.min;
            boundsMax = tileCollider.bounds.max;
            enterEdge = exitEdge = TileEdge.None;
        }

        void OnMouseDown()
        {
            inputController.isRaking = true;
        }
        void OnMouseUp()
        {
            inputController.isRaking = false;
        }

        void OnTriggerEnter(Collider col)
        {
            if (Input.GetMouseButton(0))
            {
                enterEdge = GetEdge(Input.mousePosition);
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (Input.GetMouseButton(0))
            {
                exitEdge = GetEdge(Input.mousePosition);
            }

            // gameObject.GetComponentInChildren<Renderer>().material.SetTexture("_BumpMap", switcher);

            // check if we're in the Raking state
            // REFACTOR
            if (gameController.viewStateController.currentState.gameObject.name == "RakingState")
            {
                // Debug.Log(enterEdge & exitEdge);
                if ((enterEdge == TileEdge.Top || enterEdge == TileEdge.Bottom) && (exitEdge == TileEdge.Top || exitEdge == TileEdge.Bottom))
                {
                    gameObject.GetComponentInChildren<Renderer>().material.SetTexture("_BumpMap", TopBottom);
                }
                if ((enterEdge == TileEdge.Left || enterEdge == TileEdge.Right) && (exitEdge == TileEdge.Left || exitEdge == TileEdge.Right))
                {
                    gameObject.GetComponentInChildren<Renderer>().material.SetTexture("_BumpMap", LeftRight);
                }
                if ((enterEdge == TileEdge.Left || exitEdge == TileEdge.Left)  && (enterEdge == TileEdge.Bottom || exitEdge == TileEdge.Bottom))
                {
                    gameObject.GetComponentInChildren<Renderer>().material.SetTexture("_BumpMap", BottomLeft);
                }
                if ((enterEdge == TileEdge.Right || exitEdge == TileEdge.Right)  && (enterEdge == TileEdge.Bottom || exitEdge == TileEdge.Bottom))
                {
                    gameObject.GetComponentInChildren<Renderer>().material.SetTexture("_BumpMap", BottomRight);
                }
                if ((enterEdge == TileEdge.Left || exitEdge == TileEdge.Left)  && (enterEdge == TileEdge.Top || exitEdge == TileEdge.Top))
                {
                    gameObject.GetComponentInChildren<Renderer>().material.SetTexture("_BumpMap", TopLeft);
                }
                if ((enterEdge == TileEdge.Right || exitEdge == TileEdge.Right)  && (enterEdge == TileEdge.Top || exitEdge == TileEdge.Top))
                {
                    gameObject.GetComponentInChildren<Renderer>().material.SetTexture("_BumpMap", TopRight);
                }
            }
        }

        protected TileEdge GetEdge(Vector3 mousePosition)
        {
            int layerMask = 1 << 26;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                // world coords
                Vector3 closestPoint = tileCollider.ClosestPointOnBounds(hit.point);
                // local coords
                // Vector3 closestPoint = transform.InverseTransformPoint(tileCollider.ClosestPointOnBounds(hit.point));
                // Debug.Log("Closest point: " + closestPoint);
                // if (Mathf.Approximately(closestPoint.x, boundsMin.x)) {Debug.Log("Left");}
                if ((Mathf.Abs(closestPoint.x - boundsMin.x)) < gameController.rakeThreshold)
                {
                    // Debug.Log("Left");
                    return TileEdge.Right;
                }
                if ((Mathf.Abs(closestPoint.z - boundsMin.z)) < gameController.rakeThreshold)
                {
                    // Debug.Log("Top");
                    return TileEdge.Top;
                }
                if ((Mathf.Abs(closestPoint.x - boundsMax.x)) < gameController.rakeThreshold)
                {
                    // Debug.Log("Right");
                    return TileEdge.Left;
                }
                if ((Mathf.Abs(closestPoint.z - boundsMax.z)) < gameController.rakeThreshold)
                {
                    // Debug.Log("Bottom");
                    return TileEdge.Bottom;
                }
            }
            return TileEdge.None;
        }
    }
}
