using BlitzyUI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.MainGame
{
    public class UILose : BlitzyUI.Screen
    {
        [SerializeField] private Button _btnHome;
        [SerializeField] private Button _btnReplay;
        [SerializeField] private Text _txtCoin;
        [SerializeField] private Text _txtLevel;
        [SerializeField] private Text _txtTym;
        [SerializeField] private Text _txtTimeTym;

        public override void OnFocus()
        {

        }

        public override void OnFocusLost()
        {
        }

        public override void OnPop()
        {
            PopFinished();
            GameManager.Instance.RemoveActionCoin(UpdateCoint);
            GameManager.Instance.RemoveActionLevel(UpdateLevel);
            GameManager.Instance.RemoveActionTym(UpdateTym);
            GameManager.Instance.RemoveActionTimeHeal(UpdateTextTime);
        }

        public override void OnPush(Data data)
        {
            PushFinished();
            GetComponent<Canvas>().overrideSorting = false;
            UpdateCoint();
            UpdateLevel();
            UpdateTym();
            GameManager.Instance.AddActionCoin(UpdateCoint);
            GameManager.Instance.AddActionLevel(UpdateLevel);
            GameManager.Instance.AddActionTym(UpdateTym);
            GameManager.Instance.AddActionTimeHeal(UpdateTextTime);
        }

        public override void OnSetup()
        {
            _btnReplay.onClick.AddListener(() => BtnReplay());
            _btnHome.onClick.AddListener(() => BtnHome());
        }

        public void BtnReplay()
        {
            int level = PlayerPrefs.GetInt("level");
            UIManager.Instance.QueuePush(GameManager.ScreenId_UIFadeScreen, null, "UIFadeScene", null);

            UIFadeScreen ui = UIManager.Instance.GetScreen<UIFadeScreen>(GameManager.ScreenId_UIFadeScreen);

            if (ui != null && !ui.gameObject.active) ui.gameObject.SetActive(true);

            ui.SetAction(() => {
                LevelManager.Instance.GenerateGrid(PlayerPrefs.GetInt("level") - 1);
                UIManager.Instance.ForceRemoveScreen(GameManager.ScreenId_UILose);
            }, true);
        }

        private void UpdateTym()
        {
            _txtTym.text = GameManager.Instance.pref.GetTym().ToString();
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

        private void UpdateTextTime()
        {
            _txtTimeTym.text = GameManager.Instance.timeHeal;
        }

        public void UpdateCoint()
        {
            int coin = PlayerPrefs.GetInt("coin");
            _txtCoin.text = GameManager.Instance.FormatMoney(coin);
        }

        public void UpdateLevel()
        {
            _txtLevel.text = "level " + PlayerPrefs.GetInt("level").ToString();
        }
    }
}
