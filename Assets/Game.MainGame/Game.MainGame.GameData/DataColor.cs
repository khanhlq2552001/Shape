using System.Collections.Generic;
using UnityEngine;

namespace Game.MainGame
{
    [CreateAssetMenu(fileName = "DataColorGame", menuName = "Game/DataColor")]
    public class DataColor : ScriptableObject
    {
        public Color colorWall;
        public Color colorWallShadow;
        public List<ColorShape> colorsShape;
    }

    [System.Serializable]
    public class ColorShape
    {
        public Color colorMul;
        public Color colorOver;
        public Color colorShadow;
    }
}
