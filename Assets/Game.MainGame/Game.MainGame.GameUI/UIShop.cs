using System;
using BlitzyUI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.MainGame
{
    public class UIShop : BlitzyUI.Screen
    {
        [SerializeField] private ScrollRect _scroll;
        [SerializeField] private Button _btnClose;
        [SerializeField] private Button _btnBuyNoAds;
        [SerializeField] private Button _btnBuyNoAdsPack;
        [SerializeField] private Button _btnBuyStaterPack;

        private Action _actionClose;

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
            _scroll.verticalNormalizedPosition = 1;
        }

        public override void OnSetup()
        {
            _btnClose.onClick.AddListener(() => BtnClose());
            _btnBuyNoAdsPack.onClick.AddListener(() => BtnBuyNoAdsPack());
            _btnBuyStaterPack.onClick.AddListener(() => BtnBuyStaterPack());
        }

        public void SetAction(Action action = null)
        {
            _actionClose = action;
        }

        private void BtnClose()
        {
            _actionClose?.Invoke();
            _actionClose = null;
            UIManager.Instance.ForceRemoveScreen(GameManager.ScreenId_UIShop);
        }

        private void BtnBuyNoAdsPack()
        {
            int coin = GameManager.Instance.pref.GetCoin();
            coin += 2000;
            int boosterFreeze = GameManager.Instance.pref.GetCountBooster(0);
            int boosterHammer = GameManager.Instance.pref.GetCountBooster(1);
            int boosterMagic = GameManager.Instance.pref.GetCountBooster(2);
            boosterFreeze++;
            boosterHammer++;
            boosterMagic++;

            GameManager.Instance.UpdateCoin(coin);
            GameManager.Instance.UpdateBooster(boosterFreeze, 0);
            GameManager.Instance.UpdateBooster(boosterHammer, 1);
            GameManager.Instance.UpdateBooster(boosterMagic, 2);
        }

        private void BtnBuyStaterPack()
        {
            int coin = GameManager.Instance.pref.GetCoin();
            coin += 500;
            int boosterFreeze = GameManager.Instance.pref.GetCountBooster(0);
            int boosterHammer = GameManager.Instance.pref.GetCountBooster(1);
            int boosterMagic = GameManager.Instance.pref.GetCountBooster(2);
            boosterFreeze++;
            boosterHammer++;
            boosterMagic++;

            GameManager.Instance.UpdateCoin(coin);
            GameManager.Instance.UpdateBooster(boosterFreeze, 0);
            GameManager.Instance.UpdateBooster(boosterHammer, 1);
            GameManager.Instance.UpdateBooster(boosterMagic, 2);
        }
    }
}
