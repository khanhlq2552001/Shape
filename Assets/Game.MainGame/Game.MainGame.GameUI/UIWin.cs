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
        }

        public override void OnSetup()
        {

        }

        public void BtnClaimVideo()
        {

        }
    }
}
