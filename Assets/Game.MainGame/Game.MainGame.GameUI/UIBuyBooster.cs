using BlitzyUI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.MainGame
{
    public class UIBuyBooster : BlitzyUI.Screen
    {
        [SerializeField] private Text _txtName;
        [SerializeField] private Text _txtCoin;
        [SerializeField] private Text _txtDescript;
        [SerializeField] private Image _imgItem;
        [SerializeField] private Button _btnBuyCoin;
        [SerializeField] private Button _btnBuyAds;
        [SerializeField] private Button _btnClose;
        [SerializeField] private Image _imgItemx3;
        [SerializeField] private Image _imgItemx1;

        private InfoBooster _info;

        private int _price;

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

            string booster = data.Get<string>("booster");

            UpdateTextCoin();

            if (booster == "freeze")
            {
                SetUpBoosterBuy(InfoBooster.freezeTime);
                _info = InfoBooster.freezeTime;
            }
            else if (booster == "hammer")
            {
                SetUpBoosterBuy(InfoBooster.hammer);
                _info = InfoBooster.hammer;
            }
            else if (booster == "magic")
            {
                SetUpBoosterBuy(InfoBooster.magic);
                _info = InfoBooster.magic;
            }
        }

        public override void OnSetup()
        {
            GetComponent<Canvas>().overrideSorting = false;
            _btnBuyAds.onClick.AddListener(() => BtnBuyAds(_info));
            _btnBuyCoin.onClick.AddListener(() => BtnBuyCoin(_info));
            _btnClose.onClick.AddListener(() => BtnClose());
        }

        public void SetUpBoosterBuy(InfoBooster info)
        {
            DataBooster data = GameManager.Instance.dataBooster;

            switch (info)
            {
                case InfoBooster.freezeTime:
                    _txtName.text = data.listDatas[0].name;
                    _txtDescript.text = data.listDatas[0].describe.Replace("\\n", "\n"); ;
                    _imgItem.sprite = data.listDatas[0].sprBooster;
                    _imgItemx3.sprite = data.listDatas[0].sprBooster;
                    _imgItemx1.sprite = data.listDatas[0].sprBooster;
                    break;
                case InfoBooster.hammer:
                    _txtName.text = data.listDatas[1].name;
                    _txtDescript.text = data.listDatas[1].describe.Replace("\\n", "\n"); ;
                    _imgItem.sprite = data.listDatas[1].sprBooster;
                    _imgItemx3.sprite = data.listDatas[1].sprBooster;
                    _imgItemx1.sprite = data.listDatas[1].sprBooster;
                    break;
                case InfoBooster.magic:
                    _txtName.text = data.listDatas[2].name;
                    _txtDescript.text = data.listDatas[2].describe.Replace("\\n", "\n"); ;
                    _imgItem.sprite = data.listDatas[2].sprBooster;
                    _imgItemx3.sprite = data.listDatas[2].sprBooster;
                    _imgItemx1.sprite = data.listDatas[2].sprBooster;
                    break;
            }
        }

        private void UpdateTextCoin()
        {
            _txtCoin.text = GameManager.Instance.pref.GetCoin().ToString();
        }

        private void BtnBuyCoin(InfoBooster info)
        {
            DataBooster data = GameManager.Instance.dataBooster;

            switch (info)
            {
                case InfoBooster.freezeTime:
                    if(GameManager.Instance.pref.GetCoin() >= data.listDatas[0].price)
                    {

                    }

                    break;
                case InfoBooster.hammer:
                    if (GameManager.Instance.pref.GetCoin() >= data.listDatas[1].price)
                    {

                    }

                    break;
                case InfoBooster.magic:
                    if (GameManager.Instance.pref.GetCoin() >= data.listDatas[2].price)
                    {

                    }

                    break;
            }
        }

        private void BtnBuyAds(InfoBooster info)
        {

        }

        private void BtnClose()
        {
            LevelManager.Instance.controller.StateController = StateController.NoDrag;
            UIManager.Instance.QueuePop();
        }

    }


}
