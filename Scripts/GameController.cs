using System.Linq;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pixelplacement;

namespace Kyoto
{
    public class GameController : Singleton<GameController>
    {
        [Header("Debugging")]
        public bool debug;
        private GameObject mainCanvasObject;
        private GameObject debugTextObject;
        public Text gameDateNormalizedText;
        public Text gameTimeText;
        private TimeController timeController;
        public ViewStateController viewStateController;

        [Header("Global Variables")]
        // REFACTOR to data?
        public float rakeThreshold = 0.05f;
        public float stateChangeTime = 0.8f;

        [Header("Fading")]
        public float shadowDistance = 60f;
        public float shadowDistanceFaded = 30f;
        public float shadowFadeTime = 0.5f;

        public float fadeableFadeAmount = 0.1f;
        public Color fadeableOutlineColor = Color.red;
        public float fadeableOutlineWidth = 0.25f;

        // Start is called before the first frame update
        void Awake()
        {
            mainCanvasObject = GameObject.Find("MainCanvas");
            debugTextObject = GameObject.Find("DebugText");
            gameDateNormalizedText = GameObject.Find("GameDateNormalized").GetComponent<Text>();
            gameTimeText = GameObject.Find("GameTime").GetComponent<Text>();
            timeController = TimeController.Instance;
            // REFACTOR
            viewStateController = GameObject.Find("ViewController").GetComponent<ViewStateController>();
        }

        public void UpdateGameDateNormalizedText (float newValue)
        {
            gameDateNormalizedText.text = "Game Date Normalized: " + newValue;
        }

        public void UpdateGameTimeText(string newValue)
        {
            gameTimeText.text = "Game Time: " + newValue;
        }
    }
}
