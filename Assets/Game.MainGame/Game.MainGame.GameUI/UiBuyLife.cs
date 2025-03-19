using BlitzyUI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.MainGame
{
    public class UiBuyLife : BlitzyUI.Screen
    {
        [SerializeField] private Text _txtCoin;
        [SerializeField] private Text _txtTym;
        [SerializeField] private Text _txtTime;
        [SerializeField] private Button _btnBuyCoin;
        [SerializeField] private Button _btnBuyTym;
        [SerializeField] private Button _btnX;

        public override void OnFocus()
        {
        }

        public override void OnFocusLost()
        {
        }

        public override void OnPop()
        {
            PopFinished();
            GameManager.Instance.RemoveActionTym(UpdateTxtTym);
            GameManager.Instance.RemoveActionTimeHeal(UpdateTxtTimeTym);
            GameManager.Instance.RemoveActionCoin(UpdateTxtCoin);
        }

        public override void OnPush(Data data)
        {
            PushFinished();
            UpdateTxtTym();
            GameManager.Instance.AddActionTym(UpdateTxtTym);
            GameManager.Instance.AddActionTimeHeal(UpdateTxtTimeTym);
            GameManager.Instance.AddActionCoin(UpdateTxtCoin);
        }

        public override void OnSetup()
        {
            GetComponent<Canvas>().overrideSorting = false;
            _btnX.onClick.AddListener(() => BtnClose());
            _btnBuyTym.onClick.AddListener(() => BtnBuyAds());
            _btnBuyCoin.onClick.AddListener(() => BtnBuyCoin());
        }

        private void UpdateTxtTym()
        {
            _txtTym.text = GameManager.Instance.pref.GetTym().ToString();
        }

        private void UpdateTxtCoin()
        {
            _txtCoin.text = GameManager.Instance.pref.GetCoin().ToString();
        }

        private void UpdateTxtTimeTym()
        {
            _txtTime.text = GameManager.Instance.timeHeal;
        }

        private void BtnClose()
        {
            UIManager.Instance.QueuePop();
        }

        private void BtnBuyCoin()
        {


        }

        private void BtnBuyAds()
        {
            int tym = GameManager.Instance.pref.GetTym();
            tym++;
            GameManager.Instance.UpdateTym(tym);
            UIHome uiHome = UIManager.Instance.GetScreen<UIHome>(GameManager.ScreenId_UIHome);

            if (uiHome.gameObject.active)
            {
                uiHome.LoadEffectTym();
            }
            else
            {

            }

            UIManager.Instance.QueuePop();
        }
    }
}
