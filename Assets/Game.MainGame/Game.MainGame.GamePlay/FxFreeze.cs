using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MainGame
{
    public class FxFreeze : MonoBehaviour
    {
        [SerializeField] private GameObject _par1;
        [SerializeField] private GameObject _par2;
        [SerializeField] private GameObject _par3;

        public void SetPos()
        {
            Vector3 topCenter = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0));
            topCenter.z = 0; // Giữ Z bằng 0 nếu đang làm game 2D

            Vector3 rightCenter = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0));
            rightCenter.z = 0; // Giữ Z bằng 0 nếu game 2D

            Vector3 leftCenter = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0));
            leftCenter.z = 0; // Đảm bảo Z = 0 nếu game 2D

            Vector3 bottomCenter = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0));
            bottomCenter.z = 0;

            Vector3 scale = (Camera.main.orthographicSize/ 5f) * Vector3.one;
            _par1.transform.localScale = scale;
            _par2.transform.localScale = scale;
            _par3.transform.localScale = scale;
            _par1.transform.position = topCenter;
            _par2.transform.position = rightCenter;
            _par3.transform.position = leftCenter;

        }
    }
}
