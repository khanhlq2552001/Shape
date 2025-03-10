using System;
using BlitzyUI;
using UnityEngine;

namespace Game.MainGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public static readonly BlitzyUI.Screen.Id ScreenId_ExampleMenu = new BlitzyUI.Screen.Id("ExampleMenu");
        public static readonly BlitzyUI.Screen.Id ScreenId_Setting = new BlitzyUI.Screen.Id("SettingUI");
        public static readonly BlitzyUI.Screen.Id ScreenId_UIWin = new BlitzyUI.Screen.Id("WinUI");
        public static readonly BlitzyUI.Screen.Id ScreenId_UIHome = new BlitzyUI.Screen.Id("HomeUI");
        public static readonly BlitzyUI.Screen.Id ScreenId_UILose = new BlitzyUI.Screen.Id("LoseUI");

        private Action _onActionUpdate;
        private Action _onUpdateCoin;
        private Action _onUpdateLevel;
        private Action _onUpdateTym;

        // Start is called before the first frame update
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            CheckData();
        }

        void Start()
        {
            UIManager.Instance.QueuePush(ScreenId_UIHome, null, "UIHome", null);

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

        public void AddActionCoin(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "Action cannot be null.");
            }

            _onUpdateCoin += action;
        }

        public void RemoveActionCoin(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "Action cannot be null.");
            }

            _onUpdateCoin -= action;
        }

        public void AddActionLevel(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "Action cannot be null.");
            }

            _onUpdateLevel += action;
        }

        public void RemoveActionLevel(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "Action cannot be null.");
            }

            _onUpdateLevel -= action;
        }

        public void AddActionTym(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "Action cannot be null.");
            }

            _onUpdateTym += action;
        }

        public void RemoveActionTym(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "Action cannot be null.");
            }

            _onUpdateTym -= action;
        }

        public void UpdateCoin(int coin)
        {
            PlayerPrefs.SetInt("coin", coin);
            _onUpdateCoin?.Invoke();
        }

        public void UpdateLevel(int level)
        {
            PlayerPrefs.SetInt("level", level);
            _onUpdateLevel?.Invoke();
        }

        public void UpdateTym(int tym)
        {
            PlayerPrefs.SetInt("tym", tym);
            _onUpdateTym?.Invoke();
        }

        public void CheckData()
        {
            if (!PlayerPrefs.HasKey("isFirst"))
            {
                PlayerPrefs.SetInt("isFirst", 1);
                UpdateLevel(1);
                UpdateCoin(0);
                UpdateTym(5);
            }
        }
    }
}
