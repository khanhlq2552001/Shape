using UnityEngine;
using UnityEngine.UI;

namespace Game.MainGame
{
    public class UIBuyBooster : BlitzyUI.Screen
    {
        [SerializeField] private Text _txtName;
        [SerializeField] private Text _txtDescript;
        [SerializeField] private Image _imgItem;
        [SerializeField] private Button _btnBuyCoin;
        [SerializeField] private Button _btnBuyAds;

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
        }

        public override void OnSetup()
        {
            GetComponent<Canvas>().overrideSorting = false;
        }

        public void SetUpBoosterBuy(InfoBooster info)
        {
            DataBooster data = GameManager.Instance.dataBooster;

            switch (info)
            {
                case InfoBooster.hammer:
                    _txtName.text = data.listDatas[0].name;
                    _txtDescript.text = data.listDatas[0].describe;
                    _imgItem.sprite = data.listDatas[0].sprBooster;
                    break;
                case InfoBooster.freezeTime:
                    _txtName.text = data.listDatas[1].name;
                    _txtDescript.text = data.listDatas[1].describe;
                    _imgItem.sprite = data.listDatas[1].sprBooster;
                    break;
                case InfoBooster.magic:
                    _txtName.text = data.listDatas[2].name;
                    _txtDescript.text = data.listDatas[2].describe;
                    _imgItem.sprite = data.listDatas[2].sprBooster;
                    break;
            }
        }

        private void BtnBuyCoin(InfoBooster info)
        {

        }

        private void BtnBuyAds(InfoBooster info)
        {

        }

    }


}
