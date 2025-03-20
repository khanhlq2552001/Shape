using BlitzyUI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.MainGame
{
    public class UINewObject : BlitzyUI.Screen
    {
        [SerializeField] private Image _imgTitle;
        [SerializeField] private Image _imgItem;
        [SerializeField] private Text _txtName;
        [SerializeField] private Text _txtDescribe;
        [SerializeField] private Button _btnClaim;
        [SerializeField] private GameObject _objLight;
        [SerializeField] private Sprite _sprObject;
        [SerializeField] private Sprite _sprBooster;

        public override void OnFocus()
        {
        }

        public override void OnFocusLost()
        {
        }

        public override void OnPop()
        {
            PopFinished();
            DOTween.Kill(gameObject);
        }

        public override void OnPush(Data data)
        {
            PushFinished();

            Vector3 rotationAngle = new Vector3(0, 0, 360);
            float duration = 3f;
            _objLight.transform.DORotate(rotationAngle, duration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear) // Xoay đều không giảm tốc
                .SetLoops(-1, LoopType.Restart); // Lặp vô hạn

            int id  = GameManager.Instance.pref.GetIdNewBlock();
            DataObject dataO = GameManager.Instance.dataObject;
            if (dataO.objects[id].type == TypeObject.newObject)
            {
                _imgTitle.sprite = _sprObject;
            }
            else
            {
                _imgTitle.sprite = _sprBooster;
            }
            _imgTitle.SetNativeSize();
            _imgItem.sprite = dataO.objects[id].spr;
            _imgItem.SetNativeSize();
            _txtName.text = dataO.objects[id].name;
            _txtDescribe.text = dataO.objects[id].describe.Replace("\\n", "\n"); ;
            id++;
            GameManager.Instance.pref.SetIDnewBlock(id);
        }

        public void BtnClaim()
        {
            UIManager.Instance.QueuePush(GameManager.ScreenId_UIFadeScreen, null, "UIFadeScene", null);
            UIFadeScreen ui = UIManager.Instance.GetScreen<UIFadeScreen>(GameManager.ScreenId_UIFadeScreen);

            if (ui != null && !ui.gameObject.active) ui.gameObject.SetActive(true);

            ui.SetAction(() => {
                UIManager.Instance.QueuePush(GameManager.ScreenId_UIHome, null, "UIHome", null);
                Close();
                UIGamePlay uigame = UIManager.Instance.GetScreen<UIGamePlay>(GameManager.ScreenId_ExampleMenu);
                uigame.CloseUI();
            });
        }

        public void Close()
        {
            UIManager.Instance.ForceRemoveScreen(GameManager.ScreenId_UINewObject);
        }

        public override void OnSetup()
        {
            GetComponent<Canvas>().overrideSorting = false;
            _btnClaim.onClick.AddListener(() => BtnClaim());
        }

    }
}
