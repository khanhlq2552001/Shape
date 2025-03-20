using System.Collections.Generic;
using UnityEngine;

namespace Game.MainGame
{
    public enum TypeObject
    {
        newBooster,
        newObject
    }

    [CreateAssetMenu(fileName = "DataObjectGame", menuName = "Game/DataObject")]
    public class DataObject : ScriptableObject
    {
        public List<ObjectInfo> objects;
    }
    [System.Serializable]
    public class ObjectInfo
    {
        public TypeObject type;
        public string name;
        public Sprite spr;
        public string describe;
    }
}
