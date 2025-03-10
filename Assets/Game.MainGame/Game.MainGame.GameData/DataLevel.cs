using System.Collections.Generic;
using UnityEngine;

namespace Game.MainGame
{
    [CreateAssetMenu(fileName = "DataLevelGame", menuName = "Game/DataLevel")]
    public class DataLevel : ScriptableObject
    {
        public List<Data> listDatas;
    }
}
