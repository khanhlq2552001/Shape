using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MainGame
{
    public class WallCorner : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spr;
        [SerializeField] private SpriteRenderer _sprShadow;

        public void SetColor(string strHex)
        {
            Color color;
            ColorUtility.TryParseHtmlString("#" + strHex, out color);
            _spr.color = color;

            Color darkerColor = new Color(color.r *0.4f, color.g * 0.35f, color.b * 0.35f, color.a);
            _sprShadow.color = darkerColor;
        }
    }
}
