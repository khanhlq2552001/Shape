using UnityEngine;

namespace Game.MainGame
{
    public enum NodeType
    {
        None,
        Ground,
        Wall,
        Border,
        Collector,
        Unavailable=-1
    }

    public class Node : MonoBehaviour
    {
        public NodeType tileType=NodeType.Ground;
        public int x,y;

#if UNITY_EDITOR
        public void OnValidate()
        {
            //var colorHandler=GetComponent<ColorHandler>();
            //switch (tileType)
            //{
            //    case NodeType.None:
            //        colorHandler.SetColor(new Color(1, 1, 1, 0));
            //        break;
            //    case NodeType.Collector:
            //        colorHandler.SetColor(new Color(0.9f, 0.9f, 0.9f, 0));
            //        break;
            //    case NodeType.Ground:
            //        colorHandler.SetColor(new Color(0.5f, 0.5f, 0.5f, 0.5f));
            //        break;
            //    case NodeType.Wall:
            //        colorHandler.SetColor(new Color(0.25f,0.25f,0.25f,1));
            //        break;
            //    case NodeType.Border:
            //        colorHandler.SetColor(new Color(0, 0, 0, 1));
            //        break;
            //}
        }
#endif
    }
}
