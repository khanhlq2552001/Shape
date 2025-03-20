using System.Collections.Generic;
using UnityEngine;

namespace Game.MainGame
{
    public enum Difficulty
    {
        normal,
        hard,
        veryHard
    }
    [CreateAssetMenu(fileName = "DataLevelGame", menuName = "Game/DataLevel")]
    public class DataLevel : ScriptableObject
    {
        public List<Data> listDatas;
    }
}
