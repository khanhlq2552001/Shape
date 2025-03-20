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
            UpdateCoint();
            UpdateLevel();
        }

        public override void OnSetup()
        {
            GameManager.Instance.AddActionCoin(UpdateCoint);
            GameManager.Instance.AddActionLevel(UpdateLevel);
            _btnReplay.onClick.AddListener(() => BtnReplay());
        }

        public void BtnReplay()
        {
            int level = PlayerPrefs.GetInt("level");
            LevelManager.Instance.GenerateGrid(level - 1);
            UIManager.Instance.QueuePop();
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
