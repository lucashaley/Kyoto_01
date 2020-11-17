using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pixelplacement;

namespace Kyoto
{
    [System.Serializable]
    public class PlacerEvent : UnityEvent<Placer>
    {
    }

    public class PlaceableManager : Singleton<PlaceableManager>
    {
        private ViewStateController viewStateController;
        public bool isSelected;
        [SerializeField] private Placer currentPlacer = default;
        public PlacerEvent newSelectionNotification;

        public Placer CurrentPlacer
        {
            get => currentPlacer;
            set { currentPlacer = value; }
        }

        void Awake ()
        {
            // REFACTOR
            viewStateController = GameObject.Find("ViewController").GetComponent<ViewStateController>();

            // Is this needed?
            if (newSelectionNotification == null)
                newSelectionNotification = new PlacerEvent();
        }

        public void SetPlacer(Placer p)
        {
            // Oh snap look at that elegance
            currentPlacer?.Reset();
            currentPlacer = p;
            isSelected = true;
            newSelectionNotification.Invoke(p);
        }

        public void Reset()
        {
            if (isSelected)
            {
                currentPlacer?.Reset();
                currentPlacer = null;
                isSelected = false;
            }
        }

        public void EnablePlacing()
        {
            foreach (Placer p in GetComponentsInChildren<Placer>())
            {
                p.EnablePlacer();
            }
        }
        public void DisablePlacing()
        {
            foreach (Placer p in GetComponentsInChildren<Placer>())
            {
                p.DisablePlacer();
            }

        }
    }
}
