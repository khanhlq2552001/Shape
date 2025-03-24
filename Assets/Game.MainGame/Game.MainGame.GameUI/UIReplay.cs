using BlitzyUI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.MainGame
{
    public class UIReplay : BlitzyUI.Screen
    {
        [SerializeField] private Text _txtLevel;
        [SerializeField] private Button _btnReplay;
        [SerializeField] private Button _btnClose;
        [SerializeField] private Button _btnHome;
        [SerializeField] private Text _txtCoin;
        [SerializeField] private Text _txtTym;
        [SerializeField] private Text _txtTimeTym;

        public void BtnClose()
        {
            LevelManager.Instance.controller.StateController = StateController.NoDrag;
            UIManager.Instance.QueuePop();
        }

        public void BtnReplay()
        {
            if(PlayerPrefs.GetInt("tym") > 0)
            {
                int tym = PlayerPrefs.GetInt("tym") - 1;
                GameManager.Instance.UpdateTym(tym);
                UIManager.Instance.QueuePush(GameManager.ScreenId_UIFadeScreen, null, "UIFadeScene", null);

                UIFadeScreen ui = UIManager.Instance.GetScreen<UIFadeScreen>(GameManager.ScreenId_UIFadeScreen);

                if (ui != null && !ui.gameObject.active) ui.gameObject.SetActive(true);

                ui.SetAction(() => {
                    LevelManager.Instance.GenerateGrid(PlayerPrefs.GetInt("level") - 1);
                    Close();
                }, true);
            }
        }

        private void Close()
        {
            UIManager.Instance.ForceRemoveScreen(GameManager.ScreenId_UIReplay);
        }


        public void UpdateTextLevel()
        {
            _txtLevel.text = "Level " + PlayerPrefs.GetInt("level").ToString();
        }

        // Start is called before the first frame update
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
            UpdateTextLevel();
            UpdateTxtCoin();
            UpdateTym();

            GameManager.Instance.AddActionTym(UpdateTym);
            GameManager.Instance.AddActionTimeHeal(UpdateTextTime);
        }

        public override void OnSetup()
        {
            _btnClose.onClick.AddListener(() => BtnClose());
            _btnReplay.onClick.AddListener(() => BtnReplay());
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

        private void UpdateTxtCoin()
        {
            int coin = GameManager.Instance.pref.GetCoin();
            _txtCoin.text = GameManager.Instance.FormatMoney(coin);
        }
    }
}
