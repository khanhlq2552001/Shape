using System.Collections;
using BlitzyUI;
using DG.Tweening;
using Lean.Pool;
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
        [SerializeField] private GameObject _objCoin;
        [SerializeField] private Transform _tranCha;
        [SerializeField] private Transform _tranStart;
        [SerializeField] private Transform[] _transPath =  new Transform[4];

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
            _txtLevel.text = "Level " + PlayerPrefs.GetInt("level").ToString();
            UpdateTextCoint();
        }

        public void BtnHome()
        {
            UIManager.Instance.QueuePop();
            LevelManager.Instance.controller.StateController = StateController.Pause;
            LevelManager.Instance.ClearCell();
            UIGamePlay ui =  UIManager.Instance.GetScreen<UIGamePlay>(GameManager.ScreenId_ExampleMenu);
            ui.CloseUI();
            UIManager.Instance.QueuePush(GameManager.ScreenId_UIHome, null, "UIHome", null);
        }

        public void UpdateTextCoint()
        {
            int coin = PlayerPrefs.GetInt("coin");
            _txtCoin.text = GameManager.Instance.FormatMoney(coin);
        }

        public override void OnSetup()
        {
            _btnClaimVideo.onClick.AddListener(() => BtnClaimVideo());
            _btnHome.onClick.AddListener(() => BtnHome());
            _btnClaim.onClick.AddListener(() => BtnClaim());
        }

        public void Close()
        {
            UIManager.Instance.ForceRemoveScreen(GameManager.ScreenId_UIWin);
        }

        public void BtnClaimVideo()
        {
            int coin = PlayerPrefs.GetInt("coin");
            int level = PlayerPrefs.GetInt("level");
            level++;
            coin += 100;

            GameManager.Instance.UpdateCoin(coin);
            GameManager.Instance.UpdateLevel(level);

            StartCoroutine(EffCoinCoroutine(level));
        }

        public void BtnClaim()
        {
            int coin = PlayerPrefs.GetInt("coin");
            int level = PlayerPrefs.GetInt("level");
            level++;
            coin += 50;

            GameManager.Instance.UpdateCoin(coin);
            GameManager.Instance.UpdateLevel(level);

            StartCoroutine(EffCoinCoroutine(level));
        }

        public void ShowNewObject()
        {
            UIManager.Instance.QueuePush(GameManager.ScreenId_UINewObject, null, "UINewObject", null);
        }

        IEnumerator EffCoinCoroutine(int level)
        {
            GameObject[] objs = new GameObject[10];
            Vector3 []paths = new Vector3[]{
                _transPath[1].position,
                _transPath[2].position,
                _transPath[3].position
            };

            for(int i=0; i< 8; i++)
            {
                Vector2 start = GetRandomPoint(_tranStart.position, 1f);

                GameObject obj  = LeanPool.Spawn(_objCoin,  start , Quaternion.identity );
                obj.transform.SetParent(_tranCha);
                obj.transform.localScale = Vector3.one;
                obj.transform.position = _tranStart.position;

                objs[i] = obj;

                obj.transform.DOMove(start, 0.2f).SetEase(Ease.Linear).OnUpdate(() => {
                    obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, 0);
                });

            }
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < 8; i++)
            {
                int idx = i;
                objs[i].transform.DOPath(paths, 0.6f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() => {
                    UpdateTextCoint();
                    LeanPool.Despawn(objs[idx]);
                });
                yield return new WaitForSeconds(0.13f);
            }

            yield return new WaitForSeconds(0.7f);

            if (level == 2)
            {
                UIManager.Instance.QueuePop();
                UIManager.Instance.QueuePush(GameManager.ScreenId_UINewObject, null, "UINewObject", null);
            }
            else
            {
                UIManager.Instance.QueuePush(GameManager.ScreenId_UIFadeScreen, null, "UIFadeScene", null);
                UIFadeScreen ui = UIManager.Instance.GetScreen<UIFadeScreen>(GameManager.ScreenId_UIFadeScreen);

                if (ui != null && !ui.gameObject.active) ui.gameObject.SetActive(true);

                ui.SetAction(() => {
                    UIManager.Instance.QueuePush(GameManager.ScreenId_UIHome, null, "UIHome", null);
                    Close();
                    UIGamePlay uigame = UIManager.Instance.GetScreen<UIGamePlay>(GameManager.ScreenId_ExampleMenu);
                    LevelManager.Instance.ClearCell();
                    uigame.CloseUI();
                });
            }

        }

        public static Vector2 GetRandomPoint(Vector2 origin, float radius)
        {
            float theta = Random.Range(0f, Mathf.PI * 2); // Góc ngẫu nhiên
            float distance = Mathf.Sqrt(Random.Range(0f, 1f)) * radius; // Khoảng cách ngẫu nhiên

            float x = origin.x + distance * Mathf.Cos(theta);
            float y = origin.y + distance * Mathf.Sin(theta);

            return new Vector2(x, y);
        }
    }
}
