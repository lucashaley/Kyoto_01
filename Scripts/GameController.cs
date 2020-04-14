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
        private Text gameDateNormalizedText;
        private TimeController timeController;

        // Start is called before the first frame update
        void Start()
        {
            mainCanvasObject = GameObject.Find("MainCanvas");
            debugTextObject = GameObject.Find("DebugText");
            gameDateNormalizedText = GameObject.Find("GameDateNormalized").GetComponent<Text>();
            timeController = TimeController.Instance;
        }

        public void UpdateGameDateNormalizedText (float newValue)
        {
            gameDateNormalizedText.text = "Game Date Normalized: " + newValue;
        }
    }
}
