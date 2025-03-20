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
            UpdateTextCoin();

            if(data!= null)
            {
                string booster = data.Get<string>("booster");


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
                    _txtDescript.text = data.listDatas[0].describe.Replace("\\n", "\n");
                    _imgItem.sprite = data.listDatas[0].sprBooster;
                    _imgItemx3.sprite = data.listDatas[0].sprBooster;
                    _imgItemx1.sprite = data.listDatas[0].sprBooster;
                    break;
                case InfoBooster.hammer:
                    _txtName.text = data.listDatas[1].name;
                    _txtDescript.text = data.listDatas[1].describe.Replace("\\n", "\n");
                    _imgItem.sprite = data.listDatas[1].sprBooster;
                    _imgItemx3.sprite = data.listDatas[1].sprBooster;
                    _imgItemx1.sprite = data.listDatas[1].sprBooster;
                    break;
                case InfoBooster.magic:
                    _txtName.text = data.listDatas[2].name;
                    _txtDescript.text = data.listDatas[2].describe.Replace("\\n", "\n");
                    _imgItem.sprite = data.listDatas[2].sprBooster;
                    _imgItemx3.sprite = data.listDatas[2].sprBooster;
                    _imgItemx1.sprite = data.listDatas[2].sprBooster;
                    break;
            }
        }

        private void UpdateTextCoin()
        {
            int coin = GameManager.Instance.pref.GetCoin();
            _txtCoin.text = GameManager.Instance.FormatMoney(coin);
        }

        private void BtnBuyCoin(InfoBooster info)
        {
            DataBooster data = GameManager.Instance.dataBooster;

            switch (info)
            {
                case InfoBooster.freezeTime:
                    if(GameManager.Instance.pref.GetCoin() >= data.listDatas[0].price)
                    {
                        int coin = GameManager.Instance.pref.GetCoin();
                        coin -= data.listDatas[0].price;
                        GameManager.Instance.UpdateCoin(coin);
                        int booster = GameManager.Instance.pref.GetCountBooster(0);
                        booster += 3;
                        GameManager.Instance.UpdateBooster(booster, 0);

                        UIGamePlay ui = UIManager.Instance.GetScreen<UIGamePlay>(GameManager.ScreenId_ExampleMenu);
                        ui.EffBuy(InfoBooster.freezeTime, 3);

                        BtnClose();
                        return;
                    }

                    break;
                case InfoBooster.hammer:
                    if (GameManager.Instance.pref.GetCoin() >= data.listDatas[1].price)
                    {
                        int coin = GameManager.Instance.pref.GetCoin();
                        coin -= data.listDatas[1].price;
                        GameManager.Instance.UpdateCoin(coin);
                        int booster = GameManager.Instance.pref.GetCountBooster(1);
                        booster += 3;
                        GameManager.Instance.UpdateBooster(booster, 1);

                        UIGamePlay ui = UIManager.Instance.GetScreen<UIGamePlay>(GameManager.ScreenId_ExampleMenu);
                        ui.EffBuy(InfoBooster.hammer, 3);
                        BtnClose();
                        return;
                    }

                    break;
                case InfoBooster.magic:
                    if (GameManager.Instance.pref.GetCoin() >= data.listDatas[2].price)
                    {
                        int coin = GameManager.Instance.pref.GetCoin();
                        coin -= data.listDatas[2].price;
                        GameManager.Instance.UpdateCoin(coin);
                        int booster = GameManager.Instance.pref.GetCountBooster(2);
                        booster += 3;
                        GameManager.Instance.UpdateBooster(booster, 2);

                        UIGamePlay ui = UIManager.Instance.GetScreen<UIGamePlay>(GameManager.ScreenId_ExampleMenu);
                        ui.EffBuy(InfoBooster.magic, 3);
                        BtnClose();
                        return;
                    }

                    break;
            }

            BtnClose();
            UIManager.Instance.QueuePush(GameManager.ScreenId_UIShop, null, "UIShop", null);
            UIShop uiShop = UIManager.Instance.GetScreen<UIShop>(GameManager.ScreenId_UIShop);
            uiShop.SetAction(() => {
                UIManager.Instance.QueuePush(GameManager.ScreenId_UIBuyBooster, null, "UIBuyBooster", null);
            });

        }

        private void BtnBuyAds(InfoBooster info)
        {
            DataBooster data = GameManager.Instance.dataBooster;

            switch (info)
            {
                case InfoBooster.freezeTime:
                        int booster = GameManager.Instance.pref.GetCountBooster(0);
                        booster += 1;
                        GameManager.Instance.UpdateBooster(booster, 0);

                        UIGamePlay ui = UIManager.Instance.GetScreen<UIGamePlay>(GameManager.ScreenId_ExampleMenu);
                        ui.EffBuy(InfoBooster.freezeTime, 1);

                        BtnClose();
                        break;
                case InfoBooster.hammer:
                        booster = GameManager.Instance.pref.GetCountBooster(1);
                        booster += 1;
                        GameManager.Instance.UpdateBooster(booster, 1);

                        ui = UIManager.Instance.GetScreen<UIGamePlay>(GameManager.ScreenId_ExampleMenu);
                        ui.EffBuy(InfoBooster.hammer, 1);
                        BtnClose();
                        break;
                case InfoBooster.magic:
                        booster = GameManager.Instance.pref.GetCountBooster(2);
                        booster += 1;
                        GameManager.Instance.UpdateBooster(booster, 2);

                        ui = UIManager.Instance.GetScreen<UIGamePlay>(GameManager.ScreenId_ExampleMenu);
                        ui.EffBuy(InfoBooster.magic, 1);
                        BtnClose();
                        break;
            }
        }

        private void BtnClose()
        {
            LevelManager.Instance.controller.StateController = StateController.NoDrag;
            UIManager.Instance.QueuePop();
        }

    }


}
