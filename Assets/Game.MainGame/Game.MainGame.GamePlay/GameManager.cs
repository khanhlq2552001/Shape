using System;
using BlitzyUI;
using UnityEngine;

namespace Game.MainGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public static readonly BlitzyUI.Screen.Id ScreenId_ExampleMenu = new BlitzyUI.Screen.Id("ExampleMenu");

        public CanvasGroup canvasGroup;
        private Action _onActionUpdate;

        // Start is called before the first frame update
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        void Start()
        {
            UIManager.Instance.QueuePush(ScreenId_ExampleMenu, null, "UIInGame", null);

            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            _onActionUpdate?.Invoke();
        }

        public void AddActionToOnActionUpdate(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "Action cannot be null.");
            }

            _onActionUpdate += action;
        }

        public void RemoveActionFromOnActionInStart(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "Action cannot be null.");
            }

            _onActionUpdate -= action;
        }
    }
}
