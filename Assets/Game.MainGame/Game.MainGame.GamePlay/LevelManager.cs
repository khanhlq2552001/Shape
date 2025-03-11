using System.Collections;
using System.Collections.Generic;
using BlitzyUI;
using Lean.Pool;
using UnityEngine;

namespace Game.MainGame
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

        [SerializeField] private GameObject _itemGrid;
        [SerializeField] private Transform  _tranParent;
        [SerializeField] private Data _data;
        [SerializeField] private float _spacing = 1.1f;
        [SerializeField] private DataLevel _dataLevels;
        [SerializeField] private Camera _cameraMain;

        private List<GameObject> _listItemGrid = new List<GameObject>();
        private List<ObjShape> _listItemShape = new List<ObjShape>();
        private List<ObjShape> _listShapeFreeze = new List<ObjShape>();
        private List<GameObject> _listWall = new List<GameObject>();
        private int _countKey = 0;
        private int _countShape = 0;
        private ObjShape _objLock = new ObjShape();

        public DataShape dataShape;
        public GameController controller;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
         //   GenerateGrid();
        }

        public List<GameObject> GetListItemGrid()
        {
            return _listItemGrid;
        }

        public List<ObjShape> GetListItemShape()
        {
            return _listItemShape;
        }

        public void AddWall(GameObject obj)
        {
            _listWall.Add(obj);
        }

        public Transform TranParent()
        {
            return _tranParent;
        }

        public Data GetData()
        {
            return _data;
        }

        public int CountKey
        {
            get => _countKey;

            set
            {
                _countKey = value;
                if(_countKey == 0)
                {
                    if(_objLock != null)
                        _objLock.UnLock();
                }
            }
        }

        public int CountShape
        {
            get => _countShape;

            set
            {
                _countShape = value;
                if (_countShape == 0)
                {

                }
            }
        }

        public void GenerateGrid(int id)
        {
            _data = _dataLevels.listDatas[id];
            controller.StateController = StateController.NoDrag;
            UIGamePlay ui =  UIManager.Instance.GetScreen<UIGamePlay>(GameManager.ScreenId_ExampleMenu);
            ui.StartCountdown(_data.time);

            ClearCell();

            // Tính toán kích thước tổng thể của lưới
            float totalWidth = (_data.width - 1) * _spacing;
            float totalHeight = (_data.height - 1) * _spacing;
            int count = (_data.width * _data.height);
            int idCount= 0;

            _cameraMain.orthographicSize = ((float)_data.width / 3f) * 5f;

            // Tính toán vị trí bắt đầu để lưới được đặt ở giữa màn hình
            Vector2 startPosition = new Vector2(-totalWidth * 0.5f, totalHeight * 0.5f);

            for (int i = 0; i < count; i++)
            {
                int x = i % _data.width;
                int y = i / _data.width;
                Vector2 position = startPosition + new Vector2(x * _spacing, -y * _spacing);

                GameObject o = LeanPool.Spawn(_itemGrid, position, Quaternion.identity, _tranParent);

                ItemGrid item =  o.GetComponent<ItemGrid>();
                item.ID = i;
                item.ResetAttibute();

                if (i == _data.ids[idCount])
                {
                    idCount++;
                }
                else
                {
                    o.SetActive(false);
                }

                _listItemGrid.Add(o);
            }

            CreateObjShape();

            CreateWall();

            CreateWallCorner();
        }

        private void CreateObjShape()
        {
            float totalWidth = (_data.width - 1) * _spacing;
            float totalHeight = (_data.width - 1) * _spacing;

            // Tính toán vị trí bắt đầu để lưới được đặt ở giữa màn hình
            Vector2 startPosition = new Vector2(-totalWidth * 0.5f, totalHeight * 0.5f);

            for (int i = 0; i < _data.idCentreShapes.Count; i++)
            {
                GameObject obj = LeanPool.Spawn(dataShape.shapes[_data.idCentreShapes[i].idShapes]);

                ObjShape shape = obj.GetComponent<ObjShape>();
                //
                _listItemShape.Add(shape);
                shape.ResetAttribute();
                shape.IDGate = _data.idCentreShapes[i].idGate;
                shape.ID = i;
                shape.SetColor(_data.colorCodes[_data.idCentreShapes[i].idGate], true);
                shape.SetBlock((TypeBlock)_data.idCentreShapes[i].typeBlock);
                shape.SetKeyLock(_data.idCentreShapes[i].isKey, _data.idCentreShapes[i].isLock);

                if (_data.idCentreShapes[i].isKey)
                {
                    CountKey++;
                }

                if (_data.idCentreShapes[i].isLock)
                {
                    _objLock = shape;
                }

                if (_data.idCentreShapes[i].countFreeze > 0)
                {
                    _listShapeFreeze.Add(shape);
                    shape.SetFreeze(_data.idCentreShapes[i].countFreeze);
                }

                if (_data.idCentreShapes[i].idGate2 != -1)
                {
                    shape.SetIDGate2(_data.colorCodes[_data.idCentreShapes[i].idGate2], _data.idCentreShapes[i].idGate2);
                }

                Vector2 offset = shape.tranCentre.localPosition;
                int x = _data.idCentreShapes[i].idGrid % _data.width;
                int y = _data.idCentreShapes[i].idGrid / _data.width;

                _listItemGrid[_data.idCentreShapes[i].idGrid].GetComponent<ItemGrid>().IDShape = i;
                Vector2 position = startPosition + new Vector2(x * _spacing, -y * _spacing);
                obj.transform.position = (Vector2)_listItemGrid[_data.idCentreShapes[i].idGrid].transform.position - offset;
                shape.CalculatorCoordinates(_data.idCentreShapes[i].idGrid);
            }

            CountShape = _listItemShape.Count;
            Debug.Log(CountShape);
        }

        public void ReduceCountShape()
        {
            if(CountShape > 0)
            {
                CountShape--;
                if(CountShape == 0)
                {
                    controller.StateController = StateController.Pause;
                    UIManager.Instance.QueuePush(GameManager.ScreenId_UIWin, null, "UIWin", null);
                    UIGamePlay ui = UIManager.Instance.GetScreen<UIGamePlay>(GameManager.ScreenId_ExampleMenu);
                    ui.StopTime();
                }

            }
        }

        private void CreateWall()
        {
            for (int i=0; i< _data.datasWall.Count; i++)
            {
                ItemGrid item =  _listItemGrid[_data.datasWall[i].idGrid].GetComponent<ItemGrid>();
                item.SetWall((TypeWall)_data.datasWall[i].idDirection, _data.datasWall[i].idWall, _data.datasWall[i].isWall, _data.datasWall[i].idGate);
            }
        }


        private void CreateWallCorner()
        {
            for(int i=0; i< _data.datasWallCorner.Count; i++)
            {
                ItemGrid item =  _listItemGrid[_data.datasWallCorner[i].idGrid].GetComponent<ItemGrid>();
                item.SetWallCorner((TypeWallCorner)_data.datasWallCorner[i].idWallCorner);
            }
        }

        public void ClearCell()
        {
            for (int i = 0; i < _listItemGrid.Count; i++)
            {
                LeanPool.Despawn(_listItemGrid[i]);
            }

            for (int i=0; i< _listWall.Count; i++)
            {
                LeanPool.Despawn(_listWall[i]);
            }

            for(int i=0; i< _listItemShape.Count; i++)
            {
                if (_listItemShape[i].gameObject.active)
                    LeanPool.Despawn(_listItemShape[i]);
            }

            _listItemShape.Clear();
            _listItemGrid.Clear();
            _listShapeFreeze.Clear();
            _listWall.Clear();
        }

        public void CheckShapeFreeze()
        {
            for(int i=0; i< _listShapeFreeze.Count; i++)
            {
                _listShapeFreeze[i].ReduceCountFreeze();
            }
        }

        public void BoosterMagic(int idGate, Vector2 posTarget)
        {
            StartCoroutine(DelayMagicCouroutine(idGate, posTarget));
        }

        IEnumerator DelayMagicCouroutine(int idGate, Vector2 posTarget)
        {
            int count = 0;

            for (int i = 0; i < _listItemShape.Count; i++)
            {
                if (_listItemShape[i].gameObject.active
                    && _listItemShape[i].IDGate == idGate
                    && _listItemShape[i].TypeShape != TypeShape.Lock
                    && _listItemShape[i].CountFreeze == 0
                    && _listItemShape[i].IDGate2 == -1)
                {
                    _listItemShape[i].SetActiveBorder(true);
                }
            }

            for (int i = 0; i < _listItemShape.Count; i++)
            {
                if (_listItemShape[i].gameObject.active && _listItemShape[i].IDGate == idGate)
                {
                    bool value =  _listItemShape[i].CheckBoosterMagic(posTarget);

                    if (value)
                    {
                        count++;
                    }

                    float time = Random.Range(0,0.3f);
                    yield return new WaitForSeconds(time);
                }
            }

            if(count > 0)
            {
                yield return new WaitForSeconds(0.7f);
                UIGamePlay ui =  UIManager.Instance.GetScreen<UIGamePlay>(GameManager.ScreenId_ExampleMenu);
                ui.CloseBooster();
            }
        }
    }

    [System.Serializable]
    public class Data
    {
        public int width;
        public int height;
        public int time; // tinh bang giay
        public List<int> ids;
        public List<string> colorCodes;
        public List<ShapeCreate> idCentreShapes;
        public List<DataWall> datasWall;
        public List<DataWallCorner> datasWallCorner;
    }

    [System.Serializable]
    public class ShapeCreate
    {
        public int idShapes;
        public int idGrid;
        public int idGate;
        public int idGate2;
        public int countFreeze;
        public int typeBlock; // 0: khong block, 1: chi di chuyen ngang, 2: chi di chuyen doc
        public bool isKey;
        public bool isLock;
    }

    [System.Serializable]
    public class DataWall
    {
        public int idWall;
        public int idDirection;
        public bool isWall;
        public int idGate;
        public int idGrid;
    }

    [System.Serializable]
    public class DataWallCorner
    {
        public int idWallCorner;
        public int idGrid;
    }
}
