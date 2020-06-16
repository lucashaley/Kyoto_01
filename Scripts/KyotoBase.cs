using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kyoto
{
    public abstract class KyotoBase : MonoBehaviour
    {
        public GameController gameController;
        public InputController inputController;

        // Start is called before the first frame update
        void Start()
        {
            // refactor
            gameController = GameController.Instance;
            inputController = InputController.Instance;
            Initialize();
        }

        protected abstract void Initialize();
    }
}
