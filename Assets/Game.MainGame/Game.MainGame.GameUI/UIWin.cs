using BlitzyUI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.MainGame
{
    public class UIWin : BlitzyUI.Screen
    {
        [SerializeField] private Button _btnHome;
        [SerializeField] private Button _btnClaimVideo;
        [SerializeField] private Button _btnClaim;
        [SerializeField] private Text _txtCoin;
        [SerializeField] private Text _txtLevel;

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
            PushFinished();
            GetComponent<Canvas>().overrideSorting = false;
            _txtLevel.text = "Level " + PlayerPrefs.GetInt("level").ToString();
            UpdateTextCoint();
        }

        public void BtnHome()
        {
            UIManager.Instance.QueuePop();
            LevelManager.Instance.controller.StateController = StateController.Pause;
            LevelManager.Instance.ClearCell();
            UIGamePlay ui =  UIManager.Instance.GetScreen<UIGamePlay>(GameManager.ScreenId_ExampleMenu);
            ui.CloseUI();
            UIManager.Instance.QueuePush(GameManager.ScreenId_UIHome, null, "UIHome", null);
        }

        public void UpdateTextCoint()
        {
            _txtCoin.text = PlayerPrefs.GetInt("coin").ToString();
        }

        public override void OnSetup()
        {
            GameManager.Instance.AddActionCoin(UpdateTextCoint);
            _btnClaimVideo.onClick.AddListener(() => BtnClaimVideo());
            _btnHome.onClick.AddListener(() => BtnHome());
            _btnClaim.onClick.AddListener(() => BtnClaim());
        }

        public void BtnClaimVideo()
        {
            int coin = PlayerPrefs.GetInt("coin");
            int level = PlayerPrefs.GetInt("level");
            level++;
            coin += 100;

            GameManager.Instance.UpdateCoin(coin);
            GameManager.Instance.UpdateLevel(level);
            LevelManager.Instance.GenerateGrid(level - 1);
            UIManager.Instance.QueuePop();
        }

        public void BtnClaim()
        {
            int coin = PlayerPrefs.GetInt("coin");
            int level = PlayerPrefs.GetInt("level");
            level++;
            coin += 50;

            GameManager.Instance.UpdateCoin(coin);
            GameManager.Instance.UpdateLevel(level);
            LevelManager.Instance.GenerateGrid(level - 1);
            UIManager.Instance.QueuePop();
        }
    }
}
