using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Newtonsoft.Json;


namespace Game.MainGame.Level
{

    [CreateAssetMenu(menuName = "LevelMap")]
    public class Game1_LevelMapSO : LevelMapSO
    {
        private const string Separator = ".";

        [JsonProperty("w")] public int width;
        [JsonProperty("h")] public int height;
        [JsonProperty("b")] public List<ObjectData> blocks=new List<ObjectData>();
        [JsonProperty("o")] public List<ObjectData> objects = new List<ObjectData>();

        [JsonProperty("t")]  public Table table;

    }

    [System.Serializable]
    public struct Table
    {
        [JsonProperty("r")] public List<NodeRow> rows;

        public void Set(Node node)
        {
            rows[node.y].column[node.x] = new NodeData() { x = node.x, y = node.y, tileType = node.tileType };
        }

        public NodeData GetNode(int x, int y)
        {
            if(y>=rows.Count ||y<0 || x >= rows[y].column.Count || x<0) return new NodeData() { tileType=NodeType.Unavailable};
            return rows[y].column[x];
        }
    }

    [System.Serializable]
    public struct NodeRow
    {
        [JsonProperty("c")] public List<NodeData> column;
    }

    [System.Serializable]
    public struct NodeData
    {
        public int x;
        public int y;
        [JsonProperty("t")] public NodeType tileType;
    }

}
