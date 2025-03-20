using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using TMPro;
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

        [Header("Freeze")]
        [SerializeField] private GameObject _objFreeze;
        [SerializeField] private TextMeshPro _textCountFreeze;

        [Header("Shape2")]
        [SerializeField] private SpriteRenderer _shape2;
        [SerializeField] private SpriteRenderer _shape2Shadow;
        [SerializeField] private GameObject _shape2Border;

        private Vector3 _posStartShape2;
        private Vector3 _scaleStartShape2;
        private string _hexColorShape2;

        [Header("Shape")]
        private bool _canMove = true;
        private int _idGate;
        private int _idGate2;
        private int _id;
        private TypeBlock _typeBlock;
        private TypeShape _typeShape;
        private int _countFreeze = 0;
        private string _hexColorShape1;
        private PolygonCollider2D _col;

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
            rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<PolygonCollider2D>();
            _posStartShape2 = _shape2.transform.localPosition;
            _scaleStartShape2 = _shape2.transform.localScale;
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

        public int IDGate2
        {
            get => _idGate2;

            set => _idGate2 = value;
        }

        public int ID
        {
            get => _id;

            set => _id = value;
        }

        public int CountFreeze
        {
            get => _countFreeze;

            set => _countFreeze = value;
        }

        public void SetActiveBorder(bool active)
        {
            _border.SetActive(active);
        }

        public string GetColor()
        {
            return _hexColorShape1;
        }

        public void UnLock()
        {
            _lock.SetActive(false);
            TypeShape = TypeShape.Normal;
            CanMove = true;
        }

        public void DestroyShape()
        {
            if (gameObject.active)
            {
                LevelManager.Instance.ReduceCountShape();
                LevelManager.Instance.CheckShapeFreeze();
            }

            LeanPool.Despawn(gameObject);
        }

        public bool CheckBoosterHammer()
        {
            if (TypeShape == TypeShape.Lock)
            {
                return false;
            }

            if(CountFreeze > 0)
            {
                ReduceCountFreeze();
                return true;
            }

            if(IDGate2 != -1)
            {
                IDGate = IDGate2;
                IDGate2 = -1;
                _shape2.gameObject.SetActive(false);
                SetColor(_hexColorShape2, true);
                return true;
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

            if(CountFreeze > 0)
            {
                ReduceCountFreeze();
                return true;
            }

            if(IDGate2 != -1)
            {
                _shape2.transform.localScale = Vector2.one;
                _shape2.transform.localPosition = Vector2.zero;
                SetColorShape2(_hexColorShape1, false);
                SetColor(_hexColorShape2, true);
                IDGate = IDGate2;
                IDGate2 = -1;
                _shape2Border.SetActive(true);
                _shape2.maskInteraction = SpriteMaskInteraction.None;
                _shape2Shadow.maskInteraction = SpriteMaskInteraction.None;
                _shape2Border.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
              //  _shape2.gameObject.SetActive(false);

                return true;
            }

            SetActiveBorder(true);

            CheckKey();

            _spr.maskInteraction = SpriteMaskInteraction.None;

            for(int i =0; i< _sprShadows.Count; i++)
            {
                _sprShadows[i].maskInteraction = SpriteMaskInteraction.None;
            }
          //  DestroyShape();

            return true;
        }

        public void DestroyIfMagic()
        {
            if (IDGate2 != -1)
            {
                _shape2.gameObject.SetActive(false);
                return;
            }
            DestroyShape();
        }

        public void CheckKey()
        {
            if(TypeShape == TypeShape.Key)
            {
                LevelManager.Instance.CountKey--;
            }
        }

        public void SetColor(string strHex, bool saveColor)
        {
            Color color;
            ColorUtility.TryParseHtmlString("#"+ strHex, out color);
            _spr.color = color;

            if (saveColor)
            {
                _hexColorShape1 = strHex;
            }

            Color darkerColor = new Color(color.r *0.4f, color.g * 0.35f, color.b * 0.35f, color.a);

            for(int i=0; i< _sprShadows.Count; i++)
            {
                _sprShadows[i].color = darkerColor;
            }
        }

        public void SetColorShape2(string strHex, bool saveColor)
        {
            Color color;
            ColorUtility.TryParseHtmlString("#" + strHex, out color);
            _shape2Shadow.color = color;

            if (saveColor)
            {
                _hexColorShape2 = strHex;
            }

            Color darkerColor = new Color(color.r *0.4f, color.g * 0.35f, color.b * 0.35f, color.a);

            _shape2.color = darkerColor;
        }

        private void ResetShape2()
        {
            _shape2.gameObject.SetActive(false);
            _shape2.transform.localPosition = _posStartShape2;
            _shape2.transform.localScale = _scaleStartShape2;
            _shape2.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            _shape2Shadow.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            _shape2Border.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            _shape2Border.SetActive(false);
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

        public void SetIDGate2(string hexColor, int idGate2)
        {
            IDGate2 = idGate2;
            _shape2.gameObject.SetActive(true);
            SetColorShape2(hexColor, true);
        }

        public void SetFreeze(int count)
        {
            CountFreeze = count;
            _objFreeze.SetActive(true);
            CanMove = false;
            _textCountFreeze.text = count.ToString();
        }

        public void ReduceCountFreeze()
        {
            if(CountFreeze > 0)
            {
                CountFreeze--;
                _textCountFreeze.text = CountFreeze.ToString();

                if(CountFreeze == 0)
                {
                    _objFreeze.SetActive(false);
                    CanMove = true;
                }
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
                            bool value = item.CheckItem(ID, IDGate, type);

                            if (!value) return false;
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

                            bool value = item.CheckItem(ID, IDGate, type);

                            if (!value) return false;
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

                            bool value = item.CheckItem(ID, IDGate, type);

                            if (!value) return false;
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

                            bool value = item.CheckItem(ID, IDGate, type);

                            if (!value) return false;
                        }
                    }
                    break;
            }
            return true;
        }

        public void EffectWin(TypeWall type, Wall wall)
        {
            _col.isTrigger = true;
            CanMove = false;
            Vector2 offset = tranCentre.localPosition;
            Vector2 posTarget = (Vector2)LevelManager.Instance.GetListItemGrid()[idsGrid[0]].transform.position - offset;

            transform.DOMove(posTarget, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                if(type == TypeWall.top || type == TypeWall.bot)
                {
                    float newY = (wall.transform.position.y - transform.position.y) + wall.transform.position.y;
                    Vector2 targetEnd = new Vector2(transform.position.x, newY);
                    wall.TweenAnVatThe();

                    transform.DOMove(targetEnd, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        if (gameObject.active)
                        {
                            wall.TweenNayLen();
                            DestroyShape();
                        }
                    });
                }
                else if(type == TypeWall.left || type == TypeWall.right)
                {
                    float newX = (wall.transform.position.x - transform.position.x) + wall.transform.position.x;
                    Vector2 targetEnd = new Vector2(newX, transform.position.y);
                    wall.TweenAnVatThe();

                    transform.DOMove(targetEnd, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                        if (gameObject.active)
                        {
                            wall.TweenNayLen();
                            DestroyShape();
                        }
                    });
                }
            });
        }

        public void EffectEndShape2(TypeWall type, Wall wall)
        {
            CanMove = false;
            Vector2 offset = tranCentre.localPosition;
            Vector2 posTarget = (Vector2)LevelManager.Instance.GetListItemGrid()[idsGrid[0]].transform.position - offset;
            _shape2.transform.localScale = Vector2.one;
            _shape2.transform.localPosition = Vector2.zero;
            SetColorShape2(_hexColorShape1, false);
            SetColor(_hexColorShape2, true);
            IDGate = IDGate2;
            IDGate2 = -1;

            transform.DOMove(posTarget, 0.2f).SetEase(Ease.Linear).OnComplete(() => {
                if (type == TypeWall.top || type == TypeWall.bot)
                {
                    float newY = (wall.transform.position.y - _shape2.transform.position.y) + wall.transform.position.y;
                    Vector2 targetEnd = new Vector2(transform.position.x, newY);

                    _shape2.transform.DOMove(targetEnd, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                        _shape2.gameObject.SetActive(false);
                        CanMove = true;
                    });
                }
                else if (type == TypeWall.left || type == TypeWall.right)
                {
                    float newX = (wall.transform.position.x - _shape2.transform.position.x) + wall.transform.position.x;
                    Vector2 targetEnd = new Vector2(newX, _shape2.transform.position.y);

                    _shape2.transform.DOMove(targetEnd, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                        _shape2.gameObject.SetActive(false);
                        CanMove = true;
                    });
                }
            });
        }

        public void CalculatorCoordinates(int id, bool isStart = false)
        {
            if(!isStart)
                ResetIDItemGrid();

            int x = id % LevelManager.Instance.GetData().width;
            int y = id / LevelManager.Instance.GetData().width;
            int newX = 0;
            int newY = 0;
            bool check = false;

            for (int i=0; i< listCalculateXY.Count; i++)
            {
                newX = x + listCalculateXY[i].x;
                newY = y + listCalculateXY[i].y;
                idsGrid[i] = newY * LevelManager.Instance.GetData().width + newX;
                LevelManager.Instance.GetListItemGrid()[idsGrid[i]].GetComponent<ItemGrid>().IDShape = ID;
                if(check == false)
                {
                    InfoWall valueCheck =  LevelManager.Instance.GetListItemGrid()[idsGrid[i]].GetComponent<ItemGrid>().CheckResult();
                    if (valueCheck.idGate != -1)
                    {
                        check = true;
                        SetActiveBorder(false);
                        CheckKey();
                        CanMove = false;
                        EffectWin(valueCheck.type, valueCheck.wall);
                        LevelManager.Instance.controller.StateController = StateController.NoDrag;
                    }
                }
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
            rb.bodyType = RigidbodyType2D.Kinematic;
            _col.isTrigger = false;
            CanMove = true;
            SetActiveBorder(false);
            TypeShape = TypeShape.Normal;
            _countFreeze = 0;
            _spr.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            _objFreeze.SetActive(false);
            ResetShape2();
            IDGate2 = -1;

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
