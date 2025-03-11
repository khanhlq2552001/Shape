using System.Collections;
using BlitzyUI;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;

namespace Game.MainGame
{
    public class UIGamePlay : BlitzyUI.Screen
    {
        [SerializeField] private Button _btnBoosterFreeze;
        [SerializeField] private Button _btnBoosterHammer;
        [SerializeField] private Button _btnBoosterMagic;
        [SerializeField] private Button _btnPause;
        [SerializeField] private Button _btnRestart;
        [SerializeField] private Image _imgFreezeTime;
        [SerializeField] private Image _imgBgFreeze;
        [SerializeField] private Text _txtTimeCountDown;
        [SerializeField] private Text _txtLevel;
        [SerializeField] private GameObject _hammerAnim;

        [Header("FreezingUI")]
        [SerializeField] private CanvasGroup _groupProgressFreezing;
        [SerializeField] private Image _progressFreezing;
        [SerializeField] private Image _bgFreezing;
        [SerializeField] private Image _bgTimeFreezing;
        [SerializeField] private Text _textTimeFreeze;
        [SerializeField] private float _fadeDuration;
        [SerializeField] private int _timeFreeze;

        [Header("HammerUI")]
        [SerializeField] private GameObject _objBooster;
        [SerializeField] private Button _btnCloseBooster;
        [SerializeField] private Text _txtDescribe;
        [SerializeField] private Image _imgBooster;
        [SerializeField] private Sprite _sprBoosterHammer;
        [SerializeField] private Sprite _sprBoosterMagic;

        [Header("MagicUI")]
        [SerializeField] private RectTransform _tranTarget;

        [Header("UIBot")]
        [SerializeField] private GameObject _countItemTime;
        [SerializeField] private Text _txtCountTime;
        [SerializeField] private GameObject _increTime;
        [SerializeField] private GameObject _countItemHammer;
        [SerializeField] private Text _txtCountHammer;
        [SerializeField] private GameObject _increHammer;
        [SerializeField] private GameObject _countItemMagic;
        [SerializeField] private Text _txtCountMagic;
        [SerializeField] private GameObject _increMagic;

        private float _timeRemaining;
        private Coroutine _countdownCoroutine;
        private bool _lockBtnFreeze = false;
        private bool _lockBtnHammer = false;
        private bool _lockBtnMagic = false;

        private void DisplayTime(float seconds)
        {
            int minutes = Mathf.FloorToInt(seconds / 60);
            int secs = Mathf.FloorToInt(seconds % 60);
            string timeFormatted = string.Format("{0:00}:{1:00}", minutes, secs);
            _txtTimeCountDown.text = timeFormatted;
        }

        public void StartCountdown(int seconds)
        {
            if (_countdownCoroutine != null)
            {
                StopCoroutine(_countdownCoroutine); // Dừng Coroutine cũ nếu đang chạy
            }

            _timeRemaining = seconds;
            _countdownCoroutine = StartCoroutine(CountdownRoutine());
        }

        private IEnumerator CountdownRoutine()
        {
            while (_timeRemaining > 0)
            {
                DisplayTime(_timeRemaining);
                yield return new WaitForSeconds(1f); // Chờ 1 giây
                _timeRemaining--;
            }

            DisplayTime(0);
            LevelManager.Instance.controller.StateController = StateController.Pause;

            if(PlayerPrefs.GetInt("tym") > 1)
            {
                int tym = PlayerPrefs.GetInt("tym") -1;
                GameManager.Instance.UpdateTym(tym);
                UIManager.Instance.QueuePush(GameManager.ScreenId_UIWin, null, "UILose", null);
            }
        }

        private IEnumerator CountdownRoutineFreeze(int time)
        {
            _progressFreezing.DOFillAmount(0, time).SetEase(Ease.Linear);

            FxFreeze fx = GameManager.Instance.particleFreeze.GetComponent<FxFreeze>();
            fx.SetPos();
            GameManager.Instance.particleFreeze.Play();
            while (time > 0)
            {
                _textTimeFreeze.text = time.ToString();
                yield return new WaitForSeconds(1f); // Chờ 1 giây
                time--;
                if(time == 2f)
                {
                    _groupProgressFreezing.DOFade(0, 2f).SetEase(Ease.Linear);

                    Image img = GameManager.Instance.uiBackGround.imgBackGroundFreeze;
                    img.DOFade(0, 2f).SetEase(Ease.Linear);

                    _bgTimeFreezing.DOFade(0, 2f).SetEase(Ease.Linear).OnComplete(() => StartCoroutine(CountdownRoutine()));
                }
            }
            GameManager.Instance.particleFreeze.Stop();
            _lockBtnFreeze = false;
        }

        private void BtnFreezeTime()
        {
            if (_lockBtnFreeze) return;

            _lockBtnFreeze = true;

            if (_countdownCoroutine != null)
            {
                StopCoroutine(_countdownCoroutine);
            }

            _textTimeFreeze.text = _timeFreeze.ToString();
            _progressFreezing.fillAmount = 1f;
            _groupProgressFreezing.gameObject.SetActive(true);
            _groupProgressFreezing.alpha = 0f;

            _groupProgressFreezing.DOFade(1, _fadeDuration).SetEase(Ease.Linear);

            Image img = GameManager.Instance.uiBackGround.imgBackGroundFreeze;
            img.gameObject.SetActive(true);
            Color startColor = img.color;
            startColor.a = 0;
            img.color = startColor;

            img.DOFade(1, _fadeDuration).SetEase(Ease.Linear);

            _bgTimeFreezing.gameObject.SetActive(true);
            startColor = _bgTimeFreezing.color;
            startColor.a = 0;
            _bgTimeFreezing.color = startColor;

            _bgTimeFreezing.DOFade(1, _fadeDuration).SetEase(Ease.Linear).OnComplete(()=> StartCoroutine(CountdownRoutineFreeze(_timeFreeze)));
        }

        private void BtnHammer()
        {
            _objBooster.SetActive(true);
            _txtDescribe.text = "Choose one block \n to break";
            _imgBooster.sprite = _sprBoosterHammer;
            LevelManager.Instance.controller.StateController = StateController.Pause;
            GameManager.Instance.AddActionToOnActionUpdate(CheckInputHammer);
        }

        private void BtnMagic()
        {
            _objBooster.SetActive(true);
            _txtDescribe.text = "Tap and vacuum blocks \n with the same color!";
            _imgBooster.sprite = _sprBoosterMagic;
            LevelManager.Instance.controller.StateController = StateController.Pause;
            GameManager.Instance.AddActionToOnActionUpdate(CheckInputMagic);
        }

        public void StopTime()
        {
            StopCoroutine(_countdownCoroutine);
        }

        private void BtnCloseBooster()
        {
            _objBooster.SetActive(false);
        }

        private void BtnPause()
        {
            BlitzyUI.Screen.Data settingData = new BlitzyUI.Screen.Data();
            settingData.Add("pause", true);

            LevelManager.Instance.controller.StateController = StateController.Pause;
            UIManager.Instance.QueuePush(GameManager.ScreenId_Setting, settingData, "UISetting", null);
        }

        public void CheckInputHammer()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                if (touch.phase == TouchPhase.Began)
                {
                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                    if (hit.collider != null)
                    {
                        ObjShape obj = hit.collider.gameObject.GetComponent<ObjShape>();

                        if (obj != null)
                        {
                            StartCoroutine(delayHammer(obj));
                        }
                    }
                }
            }
        }

        IEnumerator delayHammer(ObjShape obj)
        {
            GameObject hammer =  LeanPool.Spawn(_hammerAnim, obj.transform.position, Quaternion.identity);
            Hammer h = hammer.GetComponent<Hammer>();
            h.hex = obj.GetColor();

            yield return new WaitForSeconds(1f);
            bool value = obj.CheckBoosterHammer();

            if (value)
            {
                _objBooster.SetActive(false);
                CloseBooster();
            }
            else
            {

            }
        }

        public void CheckInputMagic()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                if (touch.phase == TouchPhase.Began)
                {
                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                    if (hit.collider != null)
                    {
                        ObjShape obj = hit.collider.gameObject.GetComponent<ObjShape>();

                        if (obj != null)
                        {
                            Vector3 screenPos = Camera.main.WorldToScreenPoint(_tranTarget.position);
                            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Camera.main.nearClipPlane));

                            LevelManager.Instance.BoosterMagic(obj.IDGate , (Vector2)worldPos);
                        }
                    }
                }
            }
        }

        public void CloseBooster()
        {
            LevelManager.Instance.controller.StateController = StateController.NoDrag;
            GameManager.Instance.RemoveActionFromOnActionInStart(CheckInputHammer);
            GameManager.Instance.RemoveActionFromOnActionInStart(CheckInputMagic);

            _objBooster.SetActive(false);
        }

        public void UpdateTextLevel()
        {
            _txtLevel.text = "Level " + PlayerPrefs.GetInt("level").ToString();
        }

        public void UpdateBoosterTime()
        {
            int countTime = PlayerPrefs.GetInt("boosterTime");
            if (countTime > 0)
            {
                _countItemTime.gameObject.SetActive(true);
                _increTime.gameObject.SetActive(false);
                _txtCountTime.text = countTime.ToString();
            }
            else
            {
                _countItemTime.gameObject.SetActive(false);
                _increTime.gameObject.SetActive(true);
            }
        }

        public void UpdateBoosterHammer()
        {
            int countTime = PlayerPrefs.GetInt("boosterHammer");
            if (countTime > 0)
            {
                _countItemHammer.gameObject.SetActive(true);
                _increHammer.gameObject.SetActive(false);
                _txtCountHammer.text = countTime.ToString();
            }
            else
            {
                _countItemHammer.gameObject.SetActive(false);
                _increHammer.gameObject.SetActive(true);
            }
        }

        public void UpdateBoosterMagic()
        {
            int countTime = PlayerPrefs.GetInt("boosterMagic");
            if (countTime > 0)
            {
                _countItemMagic.gameObject.SetActive(true);
                _increMagic.gameObject.SetActive(false);
                _txtCountMagic.text = countTime.ToString();
            }
            else
            {
                _countItemMagic.gameObject.SetActive(false);
                _increMagic.gameObject.SetActive(true);
            }
        }

        public void CloseUI()
        {
            if(_countdownCoroutine != null)
            {
                StopCoroutine(_countdownCoroutine);
            }
            OnPop();
            gameObject.SetActive(false);
        }

        public void BtnReplay()
        {
            UIManager.Instance.QueuePush(GameManager.ScreenId_UIReplay, null, "UIReplay", null);
        }

        public override void OnFocus()
        {
        }

        public override void OnFocusLost()
        {
        }

        public override void OnPop()
        {
            PopFinished();
        }

        public override void OnPush(Data data)
        {
            _lockBtnFreeze = false;
            StartCountdown(75);
            PushFinished();
            UpdateTextLevel();
            UpdateBoosterTime();
            UpdateBoosterMagic();
            UpdateBoosterHammer();
        }

        public override void OnSetup()
        {
            _btnBoosterFreeze.onClick.RemoveAllListeners();
            _btnBoosterFreeze.onClick.AddListener(() => BtnFreezeTime());
            _btnBoosterHammer.onClick.RemoveAllListeners();
            _btnBoosterHammer.onClick.AddListener(() => BtnHammer());
            _btnBoosterMagic.onClick.RemoveAllListeners();
            _btnBoosterMagic.onClick.AddListener(() => BtnMagic());
            _btnCloseBooster.onClick.RemoveAllListeners();
            _btnCloseBooster.onClick.AddListener(() => BtnCloseBooster());
            _btnPause.onClick.AddListener(() => BtnPause());
            _btnRestart.onClick.AddListener(() => BtnReplay());

            GameManager.Instance.AddActionLevel(UpdateTextLevel);
            GameManager.Instance.AddActionBooster(UpdateBoosterTime, 0);
            GameManager.Instance.AddActionBooster(UpdateBoosterHammer, 1);
            GameManager.Instance.AddActionBooster(UpdateBoosterMagic, 2);
        }
    }
}
