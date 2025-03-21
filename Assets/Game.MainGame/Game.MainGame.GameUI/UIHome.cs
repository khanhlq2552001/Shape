using System.Collections;
using BlitzyUI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.MainGame
{
    public class UIHome : BlitzyUI.Screen
    {
        [SerializeField] private Text _txtHeal;
        [SerializeField] private Text _txtTimeHeal;
        [SerializeField] private Text _txtCoin;
        [SerializeField] private Button _btnSetting;
        [SerializeField] private Text _txtLevel;
        [SerializeField] private Button _btnPlay;
        [SerializeField] private Button _btnHome;
        [SerializeField] private Button _btnShop;
        [SerializeField] private Button _btnLeaderBoard;
        [SerializeField] private Button _btnBuyTym;
        [SerializeField] private Button _btnBuyCoin;
        [SerializeField] private GameObject _objShop;
        [SerializeField] private GameObject _objHome;
        [SerializeField] private Sprite _sprOnLeft;
        [SerializeField] private Sprite _sprOffLeft;
        [SerializeField] private Sprite _sprOnMid;
        [SerializeField] private Sprite _sprOffMid;
        [SerializeField] private Sprite _sprOnRight;
        [SerializeField] private Sprite _sprOffRight;
        [SerializeField] private Image _imgMid;
        [SerializeField] private Image _imgLeft;
        [SerializeField] private Image _imgRight;
        [SerializeField] private GameObject _effTym;
        [SerializeField] private Transform _tranStart;
        [SerializeField] private Transform _tranMid;
        [SerializeField] private Transform _tranEnd;

        [Header("icon")]
        [SerializeField] private Sprite _sprIconOnLeft;
        [SerializeField] private Sprite _sprIconOffLeft;
        [SerializeField] private Sprite _sprIconOnMid;
        [SerializeField] private Sprite _sprIconOffMid;
        [SerializeField] private Sprite _sprIconOnRight;
        [SerializeField] private Sprite _sprIconOffRight;
        [SerializeField] private Image _imgIconMid;
        [SerializeField] private Image _imgIconLeft;
        [SerializeField] private Image _imgIconRight;

        public override void OnFocus()
        {
        }

        public override void OnFocusLost()
        {
        }

        public override void OnPop()
        {
            GameManager.Instance.RemoveActionTimeHeal(UpdateTime);
            PopFinished();
        }

        public override void OnPush(Data data)
        {
            PushFinished();
            UpdateTextCoint();
            UpdateTextLevel();
            UpdateTextTym();
            UpdateTime();
            _imgMid.sprite = _sprOnMid;
            _imgLeft.sprite = _sprOffLeft;
            _imgRight.sprite = _sprOffRight;
            _imgIconMid.sprite = _sprIconOnMid;
            _imgIconLeft.sprite = _sprIconOffLeft;
            _imgIconRight.sprite = _sprIconOffRight;
            _effTym.gameObject.SetActive(false);
            GameManager.Instance.AddActionTimeHeal(UpdateTime);
            UIFadeScreen ui = UIManager.Instance.GetScreen<UIFadeScreen>(GameManager.ScreenId_UIFadeScreen);
            if (ui != null)
            {
                if (ui.gameObject.active)
                {
                    ui.FadeEnd();
                }
            }
        }

        public override void OnSetup()
        {
            _btnSetting.onClick.AddListener(() => BtnSetting());
            _btnHome.onClick.AddListener(() => BtnHome());
            _btnPlay.onClick.AddListener(() => BtnPlay());
            _btnShop.onClick.AddListener(() => BtnShop());
            _btnLeaderBoard.onClick.AddListener(() => BtnLeaderBoard());
            _btnBuyTym.onClick.AddListener(() => BtnBuyTym());
            _btnBuyCoin.onClick.AddListener(() => BtnBuyCoin());

            GameManager.Instance.AddActionCoin(UpdateTextCoint);
            GameManager.Instance.AddActionLevel(UpdateTextLevel);
            GameManager.Instance.AddActionTym(UpdateTextTym);
        }

        public void UpdateTime()
        {
            _txtTimeHeal.text = GameManager.Instance.timeHeal;
        }

        public void UpdateTextCoint()
        {
            int coin  = PlayerPrefs.GetInt("coin");
            _txtCoin.text = GameManager.Instance.FormatMoney(coin);
        }

        public void UpdateTextLevel()
        {
            _txtLevel.text = PlayerPrefs.GetInt("level").ToString();
        }

        public void UpdateTextTym()
        {
            _txtHeal.text = PlayerPrefs.GetInt("tym").ToString();
        }

        public void BtnSetting()
        {
            BlitzyUI.Screen.Data settingData = new BlitzyUI.Screen.Data();
            settingData.Add("pause", false);

            LevelManager.Instance.controller.StateController = StateController.Pause;
            UIManager.Instance.QueuePush(GameManager.ScreenId_Setting, settingData, "UISetting", null);

            UISetting ui = UIManager.Instance.GetScreen<UISetting>(GameManager.ScreenId_Setting);
            if (!ui.gameObject.active) ui.gameObject.SetActive(true);
        }

        public void BtnPlay()
        {
            UIManager.Instance.QueuePop();
            LevelManager.Instance.controller.StateController = StateController.NoDrag;
            UIGamePlay uiGameObject = UIManager.Instance.GetScreen<UIGamePlay>(GameManager.ScreenId_ExampleMenu);
            if (uiGameObject != null)
            {
                uiGameObject.gameObject.SetActive(true); // Bật UI lên nếu nó đang ẩn
            }
            UIManager.Instance.QueuePush(GameManager.ScreenId_ExampleMenu, null, "UIInGame", null);
            LevelManager.Instance.GenerateGrid(PlayerPrefs.GetInt("level") - 1);
        }

        public void BtnHome()
        {
            _imgMid.sprite = _sprOnMid;
            _imgLeft.sprite = _sprOffLeft;
            _imgRight.sprite = _sprOffRight;
            _imgIconMid.sprite = _sprIconOnMid;
            _imgIconLeft.sprite = _sprIconOffLeft;
            _imgIconRight.sprite = _sprIconOffRight;

            _objHome.SetActive(true);
            _btnHome.interactable = false;
            _btnShop.interactable = true;
            _btnLeaderBoard.interactable = true;

            if (_objShop.active) StartCoroutine(DelayOffCoroutine());
        }

        public void BtnShop()
        {
            _objShop.SetActive(true);
            _imgMid.sprite = _sprOffMid;
            _imgLeft.sprite = _sprOnLeft;
            _imgRight.sprite = _sprOffRight;
            _imgIconMid.sprite = _sprIconOffMid;
            _imgIconLeft.sprite = _sprIconOnLeft;
            _imgIconRight.sprite = _sprIconOffRight;

            _objHome.SetActive(false);
            _btnShop.interactable = false;
            _btnHome.interactable = true;
            _btnLeaderBoard.interactable = true;
        }


        public void BtnLeaderBoard()
        {
            _imgMid.sprite = _sprOffMid;
            _imgLeft.sprite = _sprOffLeft;
            _imgRight.sprite = _sprOnRight;
            _imgIconMid.sprite = _sprIconOffMid;
            _imgIconLeft.sprite = _sprIconOffLeft;
            _imgIconRight.sprite = _sprIconOnRight;

            _objHome.SetActive(false);
            _btnLeaderBoard.interactable = false;
            _btnShop.interactable = true;
            _btnHome.interactable = true;
            if (_objShop.active) StartCoroutine(DelayOffCoroutine());
        }

        IEnumerator DelayOffCoroutine()
        {
            _objShop.GetComponent<Animator>().SetBool("off", true);
            yield return new WaitForSeconds(0.25f);
            _objShop.SetActive(false);
        }

        private void BtnBuyTym()
        {
            if(GameManager.Instance.pref.GetTym() < 5)
            {
                UIManager.Instance.QueuePush(GameManager.ScreenId_UIBuyLife, null, "UIBuyTym", null);
            }
        }

        private void BtnBuyCoin()
        {
            UIManager.Instance.QueuePush(GameManager.ScreenId_UIShop, null, "UIShop", null);

            UIShop ui = UIManager.Instance.GetScreen<UIShop>(GameManager.ScreenId_UIShop);
            ui.SetAction();
        }

        public void LoadEffectTym()
        {
            _effTym.gameObject.SetActive(true);
            _effTym.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
            _effTym.transform.position = _tranStart.position;
            Vector3[] paths = new Vector3[]{_tranMid.position, _tranEnd.position};
            _effTym.transform.DOPath(paths, 0.3f, PathType.CatmullRom)
              .SetEase(Ease.Linear)
              .OnComplete(() => {
                  _effTym.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).OnComplete(() => {
                      _effTym.gameObject.SetActive(false);
                  });
              });
        }

    }
}
