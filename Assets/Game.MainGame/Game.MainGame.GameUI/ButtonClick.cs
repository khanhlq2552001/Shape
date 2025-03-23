using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;


     [RequireComponent(typeof(Button))]
     public class ButtonClick : MonoBehaviour
     {
        public Vector3 from = Vector3.one;
        public Vector3 to = Vector3.one * 0.95f;
        public Vector3 to1 = Vector3.one * 1.05f;

        private float duration = 0.1f; // Thời gian thực hiện hiệu ứng

        void Start()
              {
                   GetComponent<Button>().onClick.AddListener(OnButtonClick);
                   GetComponent<RectTransform>().localScale = from;
              }

    void OnButtonClick()
    {
        // Sử dụng DOTween để tạo hiệu ứng scale
        Sequence sequence = DOTween.Sequence();
        sequence.Append(GetComponent<RectTransform>().DOScale(to, duration));
        sequence.Append(GetComponent<RectTransform>().DOScale(to1, duration));
        sequence.Append(GetComponent<RectTransform>().DOScale(from, duration));
    }
}
