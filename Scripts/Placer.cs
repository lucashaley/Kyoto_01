using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyoto
{
    public class Placer : MonoBehaviour
    {
        public bool isSelected;
        public Renderer rend;
        public Placeable placeable;
        public Vector3 offset;
        public LayerMask touchLayer;
        public LayerMask placerLayer;
        public PlaceableManager placeableManager;
        public BoxCollider col;

        void Awake()
        {
            rend = GetComponent<Renderer>();
            placeable = transform.parent.GetComponent<Placeable>();
            placeableManager = PlaceableManager.Instance;
            placerLayer = LayerMask.GetMask("Placers");
            col = GetComponent<BoxCollider>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // get raycast from mouse
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // RaycastHit hitInfo;
            // if (Physics.Raycast(ray, out hitInfo))
            // {
            //     transform.position = hitInfo.point.Rounded(1f);
            // }
        }

        public void OnEnable()
        {
            placeableManager.newSelectionNotification.AddListener(Listening);
        }
        public void OnDisable()
        {
            placeableManager.newSelectionNotification.RemoveListener(Listening);
        }
        public void Listening(Placer p)
        {
            Debug.Log("LISTTTTEEENNNINININININNGGGG");
        }

        public void EnablePlacer()
        {
            col.enabled = true;
        }
        public void DisablePlacer()
        {
            Reset();
            col.enabled = false;
        }

        public void Reset()
        {
            isSelected = false;
            rend.enabled = false;
            gameObject.layer = 19;
        }

        IEnumerator OnMouseDrag()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, touchLayer))
            {
                Vector3 v = hit.point;
                placeable.Translate(v.Rounded(1f));
            }
            yield return null;
        }

        void OnMouseDown()
        {
            // Debug.Log("poo");
            if (isSelected)
            {
                // check which side was clicked
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    switch (hit.normal)
                    {
                        // case Vector3 v when v.Equals(Vector3.up):
                        case Vector3 v when v == Vector3.up:
                            // Debug.Log("Up");
                            // transform.parent.Rotate(Vector3.up * 90f);
                            placeable.Rotate();
                            break;
                        case Vector3 v when v == Vector3.left:
                            // Debug.Log("Left");
                            offset = transform.position - hit.point;
                            placeable.offset = transform.position - hit.point;
                            // Debug.Log(offset);
                            // placeable.Translate(hit.point.Rounded(1f));
                            break;
                        case Vector3 v when v == Vector3.back:
                            Debug.Log("Back");
                            break;
                        default:
                            Debug.Break();
                            break;
                    }
                }
            // } else {
            //     // turn on placing
            //     isSelected = true;
            //     rend.enabled = true;
            }
        }

        void OnMouseUp()
        {
            if (!isSelected)
            {
                // turn on placing
                isSelected = true;
                rend.enabled = true;
                gameObject.layer = 20;
                // placeableManager.isSelected = true;
                // placeableManager.CurrentPlacer = this;
                placeableManager.SetPlacer(this);
            } else {
                // // this is the brute-force way, iterating through each placer
                // Placer[] placers = placeableManager.GetComponentsInChildren<Placer>();
                // foreach (Placer p in placers)
                // {
                //
                // }

                // this is the way that checks everything automatically
                Collider[] colliders = Physics.OverlapBox(col.bounds.center, col.bounds.extents, Quaternion.identity, placerLayer);
                if (colliders.Length > 0)
                {
                    Debug.Log("OVERLAP");
                    foreach (Collider c in colliders)
                    {
                        Debug.Log(c.transform.parent.gameObject.name);
                    }
                }
                // Debug.Break();
            }
        }

        void OnCollisionEnter(Collision col)
        {
            Debug.Break();
        }
        void OnCollisionStay(Collision col)
        {
            Debug.Break();
        }
        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.layer == LayerMask.GetMask("Placers"))
                Debug.Log("TriggerEnter: " + col.gameObject.name);
        }
        void OnTriggerStay(Collider col)
        {
            Debug.Log("TriggerStay");
        }
    }
}
