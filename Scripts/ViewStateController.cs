using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

namespace Kyoto
{
    public class ViewStateController : StateMachine
    {
        public Transform cameraUserViewPivot;
        public Transform cameraStateViewPivot;
        public bool isRotating;

        void Awake ()
        {
            if (cameraUserViewPivot == null) {cameraUserViewPivot = GameObject.Find("CameraUserViewPivot").GetComponent<Transform>();}
            if (cameraStateViewPivot == null) {cameraStateViewPivot = GameObject.Find("CameraStateViewPivot").GetComponent<Transform>();}
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetStateView ()
        {
            if (!isRotating)
            {
                Tween.LocalRotation (cameraStateViewPivot, currentState.GetComponent<StateView>().viewData.Rotation, 1, 0, Tween.EaseInOutStrong, Tween.LoopType.None, ()=>isRotating=true, ()=>isRotating=false);
            }
        }

        public void SendStateMessage ()
        {
            GameObject.Find("World").BroadcastMessage(currentState.GetComponent<StateView>().message);
            Debug.Log(currentState.GetComponent<StateView>().message);
        }

        public void RotateViewLeft()
        {
            // currentState.GetComponent<StateViewing>().RotateViewLeft();
            if (!isRotating)
            {
                Tween.Rotate (cameraUserViewPivot, new Vector3 (0, 90, 0), Space.Self, 1, 0f, Tween.EaseInOutStrong, Tween.LoopType.None, ()=>isRotating=true, ()=>isRotating=false);
            }

        }

        public void RotateViewRight()
        {
            // currentState.GetComponent<StateViewing>().RotateViewRight();
            if (!isRotating)
            {
                Tween.Rotate (cameraUserViewPivot, new Vector3 (0, -90, 0), Space.Self, 1, 0f, Tween.EaseInOutStrong, Tween.LoopType.None, ()=>isRotating=true, ()=>isRotating=false);
            }
        }

    }
}
