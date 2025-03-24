using BlitzyUI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.MainGame
{
    public class UIWelcomPlayer : BlitzyUI.Screen
    {
        [SerializeField] private Button _btnClaim;
        [SerializeField] private Button _btnClose;
        [SerializeField] private Text _txtCoin;
        [SerializeField] private Text _txtTym;

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

        }

        public override void OnSetup()
        {
            _btnClaim.onClick.AddListener(() => BtnClaim());
            _btnClose.onClick.AddListener(() => BtnClaim());
        }

        private void BtnClaim()
        {
            GameManager.Instance.SetIsCountTime(true);
            GameManager.Instance.pref.SetInfiniteTime(true);
            GameManager.Instance.pref.SetTimeRemainingInfinite(3600f);
            GameManager.Instance.UpdateTimeTym();
            GameManager.Instance.UpdateTym(5);
            Debug.Log(GameManager.Instance.pref.GetTimeRemainingInfinite());
            UIManager.Instance.QueuePop();
        }
    }
}
