using System.Collections.Generic;
using UnityEngine;

namespace Game.MainGame
{
    public enum InfoBooster
    {
        hammer,
        freezeTime,
        magic
    }

    [CreateAssetMenu(fileName = "DataGame", menuName = "Game/DataBooster")]
    public class DataBooster : ScriptableObject
    {
        public List<BoosterInfo> listDatas;
    }

    [System.Serializable]
    public class BoosterInfo
    {
        public string name;
        public int price;
        public Sprite sprBooster;
        public string describe;
    }
}
