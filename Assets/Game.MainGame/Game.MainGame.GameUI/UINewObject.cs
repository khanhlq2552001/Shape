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

            Vector3 rotationAngle = new Vector3(0, 360, 0);
            float duration = 3f;
            _objLight.transform.DORotate(rotationAngle, duration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear) // Xoay đều không giảm tốc
                .SetLoops(-1, LoopType.Restart); // Lặp vô hạn
        }

        public override void OnSetup()
        {
            GetComponent<Canvas>().overrideSorting = false;
        }


    }
}
