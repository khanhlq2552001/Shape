using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MainGame
{
    [CreateAssetMenu(fileName = "ShapeDatas", menuName = "Shape/Data")]
    public class DataShape : ScriptableObject
    {
        public List<GameObject> shapes;
    }
}
