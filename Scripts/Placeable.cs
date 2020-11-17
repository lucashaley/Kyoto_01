using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pixelplacement;

namespace Kyoto
{
    public class Placeable : MonoBehaviour
    {
        public bool isRotating;
        public bool isTranslating;
        public Vector3 offset;

        public Transform model;
        private LayerMask mask;

        public Vector2Int location;
        public Vector2Int dimensions;
        public Vector2Int scaled;

        void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            model = transform.Find("Model");
            mask = LayerMask.GetMask("Tiles");
            UpdateLocation();
            Debug.Log(OccupiedTiles().Count);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Rotate()
        {
            if (!isRotating && !isTranslating)
                Tween.Rotate (model.transform, new Vector3 (0,90,0), Space.Self,
                0.25f, 0f, Tween.EaseInOutStrong, Tween.LoopType.None,
                ()=>isRotating=true, ()=>isRotating=false);
        }

        public void Translate(Vector3 inPosition)
        {
            if (!isTranslating && !isRotating && (inPosition != transform.position))
            {
                Tween.Position(transform,
                               inPosition,
                               0.25f,
                               0f,
                               Tween.EaseInOutStrong,
                               Tween.LoopType.None,
                               StartTranslating,
                               FinishTranslating
                               );
            }
        }

        void StartTranslating()
        {
            isTranslating=true;
        }

        void FinishTranslating()
        {
            isTranslating = false;
            UpdateLocation();
        }

        void UpdateLocation()
        {
            location = Vector2Int.RoundToInt(transform.position.Vector2NoY());
            scaled = location + dimensions - Vector2Int.one;
        }

        public List<Vector2Int> OccupiedTiles()
        {
            List<Vector2Int> result = new List<Vector2Int>();

            // add the basic location
            result.Add(location);
            // iterate through dimensions
            for (int x = 1; x < dimensions.x; x++)
            {
                for (int y = 1; y < dimensions.y; y++)
                {
                    result.Add(location + new Vector2Int(x, y));
                }
            }

            return result;
        }
    }
}
