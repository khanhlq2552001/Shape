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
        [SerializeField] private SpriteRenderer _spr;
        [SerializeField] private List<SpriteRenderer> _sprShadows;
        [SerializeField] private GameObject[] _arrowTopDown = new GameObject[2];
        [SerializeField] private GameObject[] _arrowRightLeft = new GameObject[2];
        [SerializeField] private TypeWall _typeWall;

        private int _idGate;

        private void Awake()
        {
            idsItemGrid.Clear();
            for (int i=0; i< _calculateXies.Count; i++)
            {
                idsItemGrid.Add(0);
            }
        }

        public TypeWall Type
        {
            get => _typeWall;

            set => _typeWall = value;
        }

        public int IDGate
        {
            get => _idGate;

            set => _idGate = value;
        }

        public void SetColor(string strHex)
        {
            Color color;
            ColorUtility.TryParseHtmlString("#" + strHex, out color);
            _spr.color = color;

            Color darkerColor = new Color(color.r *0.4f, color.g * 0.35f, color.b * 0.35f, color.a);

            for (int i = 0; i < _sprShadows.Count; i++)
            {
                _sprShadows[i].color = darkerColor;
            }
        }

        public void SetMask(TypeWall type)
        {
            Type = type;

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

        public void SetArrow(TypeWall type)
        {
            switch (type)
            {
                case TypeWall.top:
                    _arrowTopDown[0].SetActive(true);
                    _arrowTopDown[1].SetActive(false);
                    break;
                case TypeWall.bot:
                    _arrowTopDown[1].SetActive(true);
                    _arrowTopDown[0].SetActive(false);
                    break;
                case TypeWall.left:
                    _arrowRightLeft[1].SetActive(true);
                    _arrowRightLeft[0].SetActive(false);
                    break;
                case TypeWall.right:
                    _arrowRightLeft[0].SetActive(true);
                    _arrowRightLeft[1].SetActive(false);
                    break;
            }
        }

        public void CalculatorCoordinates(int id, bool isWall, int iDGate)
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
                LevelManager.Instance.GetListItemGrid()[idsItemGrid[i]].GetComponent<ItemGrid>().AddInfoWall(this, _typeWall, iDGate);
                    
            }
        }
    }
}
