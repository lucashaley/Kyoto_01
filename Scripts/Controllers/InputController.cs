using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

namespace Kyoto
{
    public class InputController : Singleton<InputController>
    {
        public ViewStateController viewStateController;
        public bool isRaking;
        public int tileLayer;

        void Awake()
        {
            // refactor
            viewStateController = transform.parent.GetComponentInChildren<ViewStateController>();
            tileLayer = LayerMask.GetMask("Tiles");
        }
        // Update is called once per frame
        void Update()
        {
            // #if EDITOR
                // use keyboard
                if (Input.GetAxis("RotateView") != 0f)
                {
                    Debug.Log("BOOM");
                    if (Input.GetAxis("RotateView") > 0)
                    {
                        viewStateController.RotateViewLeft();
                    } else {
                        viewStateController.RotateViewRight();
                    }
                }

                // if (isRaking)
                // {
                //     RaycastHit hit;
                //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //     if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
                //     {
                //         // check if we hit a tile
                //         if (hit.collider.gameObject.TryGetComponent(out Tile tile))
                //         {
                //             tile.Ping();
                //         }
                //     }
                // }

                // int layerMask = 1 << 26;
                // RaycastHit hit;
                // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                // {
                //     Debug.Log("Hit: " + hit.transform.name);
                //     Debug.Log("Contact: " + hit.point);
                //     Debug.Log("Closest: " + hit.collider.ClosestPointOnBounds(hit.point));
                // }
            // #endif
        }
    }
}
