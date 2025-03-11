using BlitzyUI;
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
            GameManager.Instance.AddActionTimeHeal(UpdateTime);
        }

        public override void OnSetup()
        {
            _btnSetting.onClick.AddListener(() => BtnSetting());
            _btnHome.onClick.AddListener(() => BtnHome());
            _btnPlay.onClick.AddListener(() => BtnPlay());
            _btnShop.onClick.AddListener(() => BtnShop());
            _btnLeaderBoard.onClick.AddListener(() => BtnLeaderBoard());

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
            _txtCoin.text = PlayerPrefs.GetInt("coin").ToString();
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

        }

        public void BtnShop()
        {

        }

        public void BtnLeaderBoard()
        {

        }
    }
}
