using Newtonsoft.Json;




#if UNITY_EDITOR
#endif
using UnityEngine;



namespace Game.MainGame
{
    public class LevelMapSO : ScriptableObject
    {
        [SerializeField] protected string baseMap;
        [SerializeField] protected string levelName;
        [SerializeField] protected string gameTut;

        public string BaseLevelMap
        {
            get => baseMap;
            set => baseMap = value;
        }
        public string LevelName
        {
            get=> levelName;
            set => levelName = value;
        }
        public string GameTut { get => gameTut; set => gameTut = value; }
    }


}
