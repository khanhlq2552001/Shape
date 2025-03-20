using UnityEngine;
using UnityEngine.UI;

namespace Game.MainGame
{
    public class ShopHome : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scroll;
        [SerializeField] private Button _btnBuyNoAds;
        [SerializeField] private Button _btnBuyNoAdsPack;
        [SerializeField] private Button _btnBuyStaterPack;

        private void OnEnable()
        {
            _scroll.verticalNormalizedPosition = 1;
        }

        private void Start()
        {
            _btnBuyNoAdsPack.onClick.AddListener(() => BtnBuyNoAdsPack());
            _btnBuyStaterPack.onClick.AddListener(() => BtnBuyStaterPack());
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
