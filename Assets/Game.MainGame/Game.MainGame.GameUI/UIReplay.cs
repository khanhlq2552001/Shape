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

        public void BtnClose()
        {
            LevelManager.Instance.controller.StateController = StateController.NoDrag;
            UIManager.Instance.QueuePop();
        }

        public void BtnReplay()
        {
            if(PlayerPrefs.GetInt("tym") > 0)
            {
                LevelManager.Instance.GenerateGrid(PlayerPrefs.GetInt("level") - 1);
                UIManager.Instance.QueuePop();
                int tym = PlayerPrefs.GetInt("tym") - 1;
                GameManager.Instance.UpdateTym(tym);
            }
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
        }

        public override void OnPush(Data data)
        {
            PushFinished();
            LevelManager.Instance.controller.StateController = StateController.Pause;
            UpdateTextLevel();
            GetComponent<Canvas>().overrideSorting = false;
        }

        public override void OnSetup()
        {
            _btnClose.onClick.AddListener(() => BtnClose());
            _btnReplay.onClick.AddListener(() => BtnReplay());
        }
    }
}
