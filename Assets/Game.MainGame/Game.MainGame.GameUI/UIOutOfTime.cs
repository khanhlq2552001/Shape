using BlitzyUI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.MainGame
{
    public class UIOutOfTime : BlitzyUI.Screen
    {
        [SerializeField] private Text _txtCoin;
        [SerializeField] private Button _btnClose;
        [SerializeField] private Button _btnBuyCoin;
        [SerializeField] private Button _btnBuyAds;
        [SerializeField] private Button _btnHome;
        [SerializeField] private Text _txtTym;
        [SerializeField] private Text _txtTimeTym;
        [SerializeField] private int _price;

        public override void OnFocus()
        {
        }

        public override void OnFocusLost()
        {
        }

        public override void OnPop()
        {
            PopFinished();
            GameManager.Instance.RemoveActionTym(UpdateTym);
            GameManager.Instance.RemoveActionTimeHeal(UpdateTextTime);
        }

        public override void OnPush(Data data)
        {
            PushFinished();
            LevelManager.Instance.controller.StateController = StateController.Pause;
            UpdateTxtCoin();
            UpdateTym();
            GameManager.Instance.AddActionTym(UpdateTym);
            GameManager.Instance.AddActionTimeHeal(UpdateTextTime);
        }

        public override void OnSetup()
        {
            _btnClose.onClick.AddListener(() => BtnClose());
            _btnBuyCoin.onClick.AddListener(() => BtnBuyCoin());
            _btnBuyAds.onClick.AddListener(() => BtnBuyAds());
            _btnHome.onClick.AddListener(() => BtnHome());
        }

        private void BtnHome()
        {
            UIManager.Instance.QueuePush(GameManager.ScreenId_UIFadeScreen, null, "UIFadeScene", null);
            UIFadeScreen ui = UIManager.Instance.GetScreen<UIFadeScreen>(GameManager.ScreenId_UIFadeScreen);
            ui.SetAction(() => {
                UIManager.Instance.CloseAllScreensExcept(GameManager.ScreenId_UIFadeScreen);
                UIManager.Instance.QueuePush(GameManager.ScreenId_UIHome, null, "UIHome", null);
            }, true);
        }

        private void UpdateTym()
        {
            if (GameManager.Instance.pref.GetInfiniteTime())
            {
                _txtTym.text = "∞";
                return;
            }

            _txtTym.text = GameManager.Instance.pref.GetTym().ToString();
        }

        private void UpdateTextTime()
        {
            _txtTimeTym.text = GameManager.Instance.timeHeal;
        }

        private void BtnClose()
        {
            UIManager.Instance.QueuePop();
            int tym = PlayerPrefs.GetInt("tym") -1;
            GameManager.Instance.UpdateTym(tym);
            UIManager.Instance.QueuePush(GameManager.ScreenId_UILose, null, "UILose", null);
        }

        private void UpdateTxtCoin()
        {
            int coin = GameManager.Instance.pref.GetCoin();
            _txtCoin.text = GameManager.Instance.FormatMoney(coin);
        }

        private void BtnBuyCoin()
        {
            int coin = GameManager.Instance.pref.GetCoin();
            if(coin >= _price)
            {
                coin -= _price;
                GameManager.Instance.UpdateCoin(coin);

                UIManager.Instance.QueuePop();
                LevelManager.Instance.controller.StateController = StateController.NoDrag;
                UIGamePlay ui = UIManager.Instance.GetScreen<UIGamePlay>(GameManager.ScreenId_ExampleMenu);
                ui.StartCountdown(20);
            }
            else
            {

            }


        }

        private void BtnBuyAds()
        {
            UIManager.Instance.QueuePop();
            LevelManager.Instance.controller.StateController = StateController.NoDrag;
            UIGamePlay ui = UIManager.Instance.GetScreen<UIGamePlay>(GameManager.ScreenId_ExampleMenu);
            ui.StartCountdown(20);
        }
    }
}
