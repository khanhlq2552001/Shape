using System.Collections;
using System.Collections.Generic;
using BlitzyUI;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

namespace Game.MainGame
{
    public enum TypeBlock
    {
        DontBlock,
        BlockHori,
        BlockVer
    }

    public enum TypeShape
    {
        Normal,
        Lock,
        Key,
        Freeze
    }

    public class ObjShape : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spr;
        [SerializeField] private List<SpriteRenderer> _sprShadows;
        [SerializeField] private GameObject[] _arrowBlocks = new GameObject[2];
        [SerializeField] private GameObject _border;
        [SerializeField] private GameObject _key;
        [SerializeField] private GameObject _lock;


        private bool _canMove = true;
        private int _idGate;
        private int _id;
        private TypeBlock _typeBlock;
        private TypeShape _typeShape;

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

        public TypeBlock TypeLock
        {
            get => _typeBlock;

            set => _typeBlock = value;
        }

        public TypeShape TypeShape
        {
            get => _typeShape;

            set => _typeShape = value;
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

        public void SetActiveBorder(bool active)
        {
            _border.SetActive(active);
        }

        public void UnLock()
        {
            _lock.SetActive(false);
            TypeShape = TypeShape.Normal;
            CanMove = true;
        }

        public void DestroyShape()
        {
            LeanPool.Despawn(gameObject);
        }

        public bool CheckBoosterHammer()
        {
            if (TypeShape == TypeShape.Lock)
            {
                return false;
            }

            CheckKey();

            DestroyShape();

            return true;
        }

        public bool CheckBoosterMagic(Vector2 posTarget)
        {
            if(TypeShape == TypeShape.Lock)
            {
                return false;
            }
            SetActiveBorder(true);

            CheckKey();

            _spr.maskInteraction = SpriteMaskInteraction.None;

            for(int i =0; i< _sprShadows.Count; i++)
            {
                _sprShadows[i].maskInteraction = SpriteMaskInteraction.None;
            }

            transform.DOScale(Vector2.zero, 0.7f)
                .SetEase(Ease.InQuart);

            transform.DOMove(posTarget, 0.7f)
                .SetEase(Ease.InQuart)
                .OnComplete(() => {
                    DestroyShape();
                });

            return true;
        }

        public void CheckKey()
        {
            if(TypeShape == TypeShape.Key)
            {
                LevelManager.Instance.CountKey--;
            }
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

        public void SetBlock(TypeBlock type)
        {
            switch (type)
            {
                case TypeBlock.DontBlock:
                    _arrowBlocks[0].SetActive(false);
                    _arrowBlocks[1].SetActive(false);
                    TypeLock = type;
                    break;
                case TypeBlock.BlockHori:
                    _arrowBlocks[0].SetActive(true);
                    _arrowBlocks[1].SetActive(false);
                    TypeLock = type;
                    break;
                case TypeBlock.BlockVer:
                    _arrowBlocks[0].SetActive(false);
                    _arrowBlocks[1].SetActive(true);
                    TypeLock = type;
                    break;
            }
        }

        public void SetKeyLock(bool isKey, bool isLock)
        {
            if (isLock)
            {
                CanMove = false;
                TypeShape = TypeShape.Lock;
            }

            if (isKey)
            {
                TypeShape = TypeShape.Key;
            }

            _key.SetActive(isKey);
            _lock.SetActive(isLock);
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
                        DestroyShape();
                    });
                }
                else if(type == TypeWall.left || type == TypeWall.right)
                {
                    float newX = (wall.transform.position.x - transform.position.x) + wall.transform.position.x;
                    Vector2 targetEnd = new Vector2(newX, transform.position.y);

                    transform.DOMove(targetEnd, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                        DestroyShape();
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
            SetActiveBorder(false);
            TypeShape = TypeShape.Normal;

            _spr.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

            for (int i = 0; i < _sprShadows.Count; i++)
            {
                _sprShadows[i].maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            }
        }
    }
}

[System.Serializable]
public struct CalculateXY
{
    public int x;
    public int y;
}
