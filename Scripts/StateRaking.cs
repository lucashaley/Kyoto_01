using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

namespace Kyoto
{
    public class StateRaking : State
    {
        private GameController gameController;
        public Transform cameraPivot;
        public bool isRotating;

        public ViewData viewData;
        public Vector3 defaultCameraPosition;
        public Quaternion defaultCameraRotation;
        public Quaternion currentPivotRotation;

        void Start ()
        {
            gameController = GameController.Instance;
            cameraPivot = GameObject.Find("CameraPivot").GetComponent<Transform>();
            defaultCameraPosition = Camera.main.transform.position;
            defaultCameraRotation = Camera.main.transform.rotation;
        }

        void OnEnable ()
    	{

    	}

        public void RotateViewLeft()
        {
            if (!isRotating)
            {
                Tween.Rotate (cameraPivot, new Vector3 (0, 90, 0), Space.Self, gameController.stateChangeTime, 0f, Tween.EaseInOutStrong, Tween.LoopType.None, ()=>isRotating=true, ()=>isRotating=false);
            }
        }

        public void RotateViewRight()
        {
            if (!isRotating)
            {
                Tween.Rotate (cameraPivot, new Vector3 (0, -90, 0), Space.Self, gameController.stateChangeTime, 0f, Tween.EaseInOutStrong, Tween.LoopType.None, ()=>isRotating=true, ()=>isRotating=false);
            }
        }
    }
}
