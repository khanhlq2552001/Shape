using System.Collections.Generic;
using UnityEngine;

namespace Game.MainGame
{
    public class Wall : MonoBehaviour
    {
        public Transform centre;
        public List<int> idsItemGrid;

        [SerializeField] private List<CalculateXY> _calculateXies;
        [SerializeField] private List<GameObject> _listMasks;

        private int _idGate;

        private void Awake()
        {
            idsItemGrid.Clear();
            for (int i=0; i< _calculateXies.Count; i++)
            {
                idsItemGrid.Add(0);
            }
        }

        public int IDGate
        {
            get => _idGate;

            set => _idGate = value;
        }
        public void SetMask(TypeWall type)
        {
            for(int i=0; i< _listMasks.Count; i++)
            {
                _listMasks[i].SetActive(false);
            }
            switch (type)
            {
                case TypeWall.top:
                    _listMasks[0].SetActive(true);
                    break;
                case TypeWall.bot:
                    _listMasks[1].SetActive(true);
                    break;
                case TypeWall.left:
                    _listMasks[0].SetActive(true);
                    break;
                case TypeWall.right:
                    _listMasks[1].SetActive(true);
                    break;
            }
        }

        public void CalculatorCoordinates(int id)
        {
            int x = id % LevelManager.Instance.GetData().width;
            int y = id / LevelManager.Instance.GetData().width;
            int newX = 0;
            int newY = 0;

            for (int i = 0; i < _calculateXies.Count; i++)
            {
                newX = x + _calculateXies[i].x;
                newY = y + _calculateXies[i].y;
                idsItemGrid[i] = newY * LevelManager.Instance.GetData().width + newX;
            }
        }
    }
}
