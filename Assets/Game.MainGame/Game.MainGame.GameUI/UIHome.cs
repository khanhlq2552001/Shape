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
        [SerializeField] private GameObject _menuMid;
        [SerializeField] private GameObject _menuLeft;
        [SerializeField] private GameObject _menuRight;
        [SerializeField] private GameObject _effTym;
        [SerializeField] private Transform _tranStart;
        [SerializeField] private Transform _tranMid;
        [SerializeField] private Transform _tranEnd;

        [Header("icon")]
        [SerializeField] private Sprite _sprIconOnLeft;
        [SerializeField] private Sprite _sprIconOnMid;
        [SerializeField] private Sprite _sprIconOnRight;
        [SerializeField] private Image _imgIconChoose;
        [SerializeField] private Text _txtChoose;
        [SerializeField] private GameObject _objChoose;
        [SerializeField] private Animator _animMenu;

        [Header("Home")]
        [SerializeField] private Sprite[] _sprBtnDifficulty = new Sprite[3];
        [SerializeField] private Sprite[] _sprIconDifficulty = new Sprite[3];
        [SerializeField] private Sprite[] _sprIconBgName = new Sprite[3];
        [SerializeField] private Image[] _imgIconDifficuly = new Image[3];
        [SerializeField] private Image[] _imgIconBgName = new Image[3];
        [SerializeField] private Text[] _txtIconName = new Text[3];
        [SerializeField] private Text[] _txtLevels = new Text[3];

        private enum Menu {shop, home, rank };
        private enum Difficulty { Normal, Hard, VeryHard}

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
            BtnHome();

            GameManager.Instance.AddActionCoin(UpdateTextCoint);
            GameManager.Instance.AddActionLevel(UpdateTextLevel);
            GameManager.Instance.AddActionTym(UpdateTextTym);
        }

        private void ItemChoose(Menu id)
        {
            switch (id)
            {
                case Menu.home:
                    _objChoose.transform.SetParent(_menuMid.transform);
                    _objChoose.transform.localPosition = Vector3.zero;
                    _imgIconChoose.sprite = _sprIconOnMid;
                    _imgIconChoose.SetNativeSize();
                    _txtChoose.text = "Home";
                    _animMenu.Play("17", 0, 0f);
                    break;
                case Menu.shop:
                    _objChoose.transform.SetParent(_menuLeft.transform);
                    _objChoose.transform.localPosition = Vector3.zero;
                    _imgIconChoose.sprite = _sprIconOnLeft;
                    _imgIconChoose.SetNativeSize();
                    _txtChoose.text = "Shop";
                    _animMenu.Play("17", 0, 0f);
                    break;
                case Menu.rank:
                    _objChoose.transform.SetParent(_menuRight.transform);
                    _objChoose.transform.localPosition = Vector3.zero;
                    _imgIconChoose.sprite = _sprIconOnRight;
                    _imgIconChoose.SetNativeSize();
                    _txtChoose.text = "Rank";
                    _animMenu.Play("17", 0, 0f);
                    break;
            }
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
            int level =GameManager.Instance.pref.GetLevel();
            DataLevel data = LevelManager.Instance._dataLevels;
            for (int i=0; i< 3; i++)
            {
                _txtLevels[i].text = (level + i).ToString();

                if(i == 0)
                {
                    _btnPlay.GetComponent<Image>().sprite = _sprBtnDifficulty[(int)data.listDatas[level - 1].diff];
                }

                _imgIconDifficuly[i].sprite = _sprIconDifficulty[(int)data.listDatas[level - 1 + i].diff];
                _imgIconDifficuly[i].SetNativeSize();

                if (data.listDatas[level - 1 + i].diff == MainGame.Difficulty.normal)
                {
                    _imgIconBgName[i].gameObject.SetActive(false);
                }
                else
                {
                    _imgIconBgName[i].gameObject.SetActive(true);
                    _imgIconBgName[i].sprite = _sprIconBgName[(int)data.listDatas[level - 1 + i].diff];

                    if (data.listDatas[level - 1 + i].diff == MainGame.Difficulty.hard) _txtIconName[i].text = "Hard";
                    else _txtIconName[i].text = "Very Hard";
                }
            }
        }

        public void UpdateTextTym()
        {
            if (GameManager.Instance.pref.GetInfiniteTime())
            {
                Debug.Log("okok");
                _txtHeal.text = "∞";
                return;
            }

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
            ItemChoose(Menu.home);

            _objHome.SetActive(true);
            _btnHome.interactable = false;
            _btnShop.interactable = true;
            _btnLeaderBoard.interactable = true;

            if (_objShop.active) StartCoroutine(DelayOffCoroutine());
        }

        public void BtnShop()
        {
            _objShop.SetActive(true);
            ItemChoose(Menu.shop);

            _objHome.SetActive(false);
            _btnShop.interactable = false;
            _btnHome.interactable = true;
            _btnLeaderBoard.interactable = true;
        }


        public void BtnLeaderBoard()
        {
            ItemChoose(Menu.rank);

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
