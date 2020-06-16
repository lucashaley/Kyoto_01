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
        public float rakeThreshold = 0.05f;

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
