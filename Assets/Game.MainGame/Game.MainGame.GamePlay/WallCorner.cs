using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MainGame
{
    public class WallCorner : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spr;
        [SerializeField] private SpriteRenderer _sprShadow;

        public void SetColor()
        {
            DataColor data = GameManager.Instance.dataColors;

            Material mat = new Material(GameManager.Instance.materialWall);
            mat.SetColor("_MultiplyColor", data.colorWall);

            _spr.material = mat;

            _sprShadow.color = data.colorWallShadow;
        }
    }
}
