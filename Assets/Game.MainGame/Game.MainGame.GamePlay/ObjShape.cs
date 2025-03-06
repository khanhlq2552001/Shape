using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

namespace Game.MainGame
{
    public class ObjShape : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spr;
        [SerializeField] private List<SpriteRenderer> _sprShadows;

        private bool _canMove = true;
        private int _idGate;
        private int _id;

        public Transform tranCentre;
        public Rigidbody2D rb;
        public List<CalculateXY> listCalculateXY;
        public List<int> idsGrid;

        private void Awake()
        {
            idsGrid.Clear();

            for (int i = 0; i < listCalculateXY.Count; i++)
            {
                idsGrid.Add(0);
            }
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public bool CanMove
        {
            get => _canMove;

            set => _canMove = value;
        }

        public int IDGate
        {
            get => _idGate;

            set => _idGate = value;
        }

        public int ID
        {
            get => _id;

            set => _id = value;
        }

        public void SetColor(string strHex)
        {
            Color color;
            ColorUtility.TryParseHtmlString("#"+ strHex, out color);
            _spr.color = color;

            Color darkerColor = new Color(color.r *0.4f, color.g * 0.35f, color.b * 0.35f, color.a);

            for(int i=0; i< _sprShadows.Count; i++)
            {
                _sprShadows[i].color = darkerColor;
            }
        }

        public bool CheckResult(TypeWall type)
        {

            switch (type)
            {
                case TypeWall.top:
                    for(int i=0; i< idsGrid.Count; i++)
                    {
                        int x = idsGrid[i] % LevelManager.Instance.GetData().width;
                        int y = idsGrid[i] / LevelManager.Instance.GetData().width;

                        for(int k = y; k>=0; k--)
                        {
                            int id = k *  LevelManager.Instance.GetData().width + x;
                            ItemGrid item = LevelManager.Instance.GetListItemGrid()[id].GetComponent<ItemGrid>();

                            if (item.IDShape != -1 && item.IDShape != ID)
                            {
                                return false;
                            }
                        }
                    }
                    break;
                case TypeWall.bot:
                    for (int i = 0; i < idsGrid.Count; i++)
                    {
                        int x = idsGrid[i] % LevelManager.Instance.GetData().width;
                        int y = idsGrid[i] / LevelManager.Instance.GetData().width;

                        for (int k = y; k < LevelManager.Instance.GetData().height; k++)
                        {
                            int id = k *  LevelManager.Instance.GetData().width + x;
                            ItemGrid item = LevelManager.Instance.GetListItemGrid()[id].GetComponent<ItemGrid>();

                            if (item.IDShape != -1 && item.IDShape != ID)
                            {
                                return false;
                            }
                        }
                    }
                    break;
                case TypeWall.right:
                    for (int i = 0; i < idsGrid.Count; i++)
                    {
                        int x = idsGrid[i] % LevelManager.Instance.GetData().width;
                        int y = idsGrid[i] / LevelManager.Instance.GetData().width;

                        for (int k = x; k < LevelManager.Instance.GetData().width; k++)
                        {
                            int id = y *  LevelManager.Instance.GetData().width + k;
                            ItemGrid item = LevelManager.Instance.GetListItemGrid()[id].GetComponent<ItemGrid>();

                            if (item.IDShape != -1 && item.IDShape != ID)
                            {
                                return false;
                            }
                        }
                    }
                    break;
                case TypeWall.left:
                    for (int i = 0; i < idsGrid.Count; i++)
                    {
                        int x = idsGrid[i] % LevelManager.Instance.GetData().width;
                        int y = idsGrid[i] / LevelManager.Instance.GetData().width;

                        for (int k = x; k >= 0; k--)
                        {
                            int id = y *  LevelManager.Instance.GetData().width + k;
                            ItemGrid item = LevelManager.Instance.GetListItemGrid()[id].GetComponent<ItemGrid>();

                            if (item.IDShape != -1 && item.IDShape != ID)
                            {
                                return false;
                            }
                        }
                    }
                    break;
            }
            return true;
        }

        public void EffectWin(TypeWall type, Wall wall)
        {
            CanMove = false;
            Vector2 offset = tranCentre.localPosition;
            Vector2 posTarget = (Vector2)LevelManager.Instance.GetListItemGrid()[idsGrid[0]].transform.position - offset;

            transform.DOMove(posTarget, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                if(type == TypeWall.top || type == TypeWall.bot)
                {
                    float newY = (wall.transform.position.y - transform.position.y) + wall.transform.position.y;
                    Vector2 targetEnd = new Vector2(transform.position.x, newY);
                    transform.DOMove(targetEnd, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        LeanPool.Despawn(gameObject);
                    });
                }
                else if(type == TypeWall.left || type == TypeWall.right)
                {
                    float newX = (wall.transform.position.x - transform.position.x) + wall.transform.position.x;
                    Vector2 targetEnd = new Vector2(newX, transform.position.y);

                    transform.DOMove(targetEnd, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                        LeanPool.Despawn(gameObject);
                    });
                }
            });
        }

        public void CalculatorCoordinates(int id)
        {
            ResetIDItemGrid();

            int x = id % LevelManager.Instance.GetData().width;
            int y = id / LevelManager.Instance.GetData().width;
            int newX = 0;
            int newY = 0;

            for (int i=0; i< listCalculateXY.Count; i++)
            {
                newX = x + listCalculateXY[i].x;
                newY = y + listCalculateXY[i].y;
                idsGrid[i] = newY * LevelManager.Instance.GetData().width + newX;

                LevelManager.Instance.GetListItemGrid()[idsGrid[i]].GetComponent<ItemGrid>().IDShape = ID;
            }
        }

        private void ResetIDItemGrid()
        {
            for(int i=0; i< idsGrid.Count; i++)
            {
                LevelManager.Instance.GetListItemGrid()[idsGrid[i]].GetComponent<ItemGrid>().IDShape = -1;
            }
        }

        public void ResetAttribute()
        {
            CanMove = true;
        }
    }
}

[System.Serializable]
public struct CalculateXY
{
    public int x;
    public int y;
}
