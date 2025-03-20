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
        public static readonly BlitzyUI.Screen.Id ScreenId_UIReplay = new BlitzyUI.Screen.Id("ReplayUI");
        public static readonly BlitzyUI.Screen.Id ScreenId_UIBuyLife = new BlitzyUI.Screen.Id("UIBuyTym");
        public static readonly BlitzyUI.Screen.Id ScreenId_UINewObject = new BlitzyUI.Screen.Id("UINewObject");
        public static readonly BlitzyUI.Screen.Id ScreenId_UIBuyBooster = new BlitzyUI.Screen.Id("UIBuyBooster");
        public static readonly BlitzyUI.Screen.Id ScreenId_UIOutOfTime = new BlitzyUI.Screen.Id("UIOutTime");
        public static readonly BlitzyUI.Screen.Id ScreenId_UIFadeScreen = new BlitzyUI.Screen.Id("UIFadeScene");

        [SerializeField] private float _remainingTime;
        [SerializeField] private float _remainingTimeStart;

        private Action _onActionUpdate;
        private Action _onUpdateCoin;
        private Action _onUpdateLevel;
        private Action _onUpdateTym;
        private Action _onUpdateBoosterTime;
        private Action _onUpdateBoosterHammer;
        private Action _onUpdateBoosterMagic;
        private Action _onUpdateTimeHeal;
        private bool _isCountTime = true;

        public string timeHeal;
        public ParticleBreak particleBreak;
        public UIBG uiBackGround;
        public ParticleSystem particleHammer;
        public ParticleSystem particleFreeze;
        public GameObject parMagic;
        public GameObject objmagic;
        public DataBooster dataBooster;
        public PrefData pref;
        public Camera cameraMain;
        public Camera cameraUI;
        public Camera cameraBG;
        public DataObject dataObject;

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

            UpdateTimeTym();
;        }

        public void UpdateTimeTym()
        {
            if(PlayerPrefs.GetInt("tym") < 5 && _isCountTime)
            {
                _isCountTime = false;
                _remainingTime = _remainingTimeStart;
            }

            if (!_isCountTime)
            {
                _remainingTime -= Time.deltaTime;  // Giảm thời gian theo thời gian thực
                if (_remainingTime <= 0)
                {
                    _remainingTime = 0;
                    _isCountTime = true;
                    int tym = PlayerPrefs.GetInt("tym") + 1;
                    UpdateTym(tym);
                }
            }
            FormatTime(_remainingTime);

            _onUpdateTimeHeal?.Invoke();
        }

        private void FormatTime(float timeInSeconds)
        {
            if (PlayerPrefs.GetInt("tym") == 5) {
                timeHeal = "Full";
                return;
            }

            int minutes = Mathf.FloorToInt(timeInSeconds / 60);
            int seconds = Mathf.FloorToInt(timeInSeconds % 60);
            timeHeal =  string.Format("{0:00}:{1:00}", minutes, seconds);
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

        public void RemoveActionTimeHeal(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "Action cannot be null.");
            }

            _onUpdateTimeHeal -= action;
        }

        public void AddActionTimeHeal(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "Action cannot be null.");
            }

            _onUpdateTimeHeal += action;
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

        public void AddActionBooster(Action action, int id)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "Action cannot be null.");
            }

            if (id == 0)
            {
                _onUpdateBoosterTime += action;
            }
            else if (id == 1)
            {
                _onUpdateBoosterHammer += action;
            }
            else if (id == 2)
            {
                _onUpdateBoosterMagic += action;
            }
        }

        public void RemoveActionBooster(Action action, int id)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "Action cannot be null.");
            }

            if (id == 0)
            {
                _onUpdateBoosterTime -= action;
            }
            else if (id == 1)
            {
                _onUpdateBoosterHammer -= action;
            }
            else if (id == 2)
            {
                _onUpdateBoosterMagic -= action;
            }
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
            if(tym <= 5)
            {
                PlayerPrefs.SetInt("tym", tym);
                _onUpdateTym?.Invoke();
            }
        }

        public void UpdateBooster(int value, int id)
        {
            switch (id)
            {
                case 0:
                    PlayerPrefs.SetInt("boosterTime", value);
                    _onUpdateBoosterTime?.Invoke();
                    break;
                case 1:
                    PlayerPrefs.SetInt("boosterHammer", value);
                    _onUpdateBoosterHammer?.Invoke();
                    break;
                case 2:
                    PlayerPrefs.SetInt("boosterMagic", value);
                    _onUpdateBoosterMagic?.Invoke();
                    break;
            }
        }

        public void UpdateSound(bool value)
        {
            PlayerPrefs.SetInt("sound", value ? 1 : 0);
        }
        public void UpdateMusic(bool value)
        {
            PlayerPrefs.SetInt("music", value ? 1 : 0);
        }
        public void UpdateVibration(bool value)
        {
            PlayerPrefs.SetInt("vibra", value ? 1 : 0);
        }
        public void UpdateNoti(bool value)
        {
            PlayerPrefs.SetInt("noti", value ? 1 : 0);
        }

        public void CheckData()
        {
            if (!PlayerPrefs.HasKey("isFirst"))
            {
                PlayerPrefs.SetInt("isFirst", 1);
                UpdateLevel(1);
                UpdateCoin(1000);
                UpdateTym(5);
                UpdateBooster(1, 0);
                UpdateBooster(1, 1);
                UpdateBooster(1, 2);
                UpdateSound(true);
                UpdateMusic(true);
                UpdateVibration(true);
                UpdateNoti(true);
                pref.SetIDnewBlock(0);
            }
        }
    }
}
