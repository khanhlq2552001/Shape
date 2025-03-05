using System.Collections.Generic;
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
        [SerializeField] private DataShape _dataShape;
        [SerializeField] private float _spacing = 1.1f;
        private List<GameObject> _listItemGrid = new List<GameObject>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            GenerateGrid();
        }

        public List<GameObject> GetListItemGrid()
        {
            return _listItemGrid;
        }

        private void GenerateGrid()
        {
            ClearCell();

            // Tính toán kích thước tổng thể của lưới
            float totalWidth = (_data.width - 1) * _spacing;
            float totalHeight = (_data.height - 1) * _spacing;

            // Tính toán vị trí bắt đầu để lưới được đặt ở giữa màn hình
            Vector2 startPosition = new Vector2(-totalWidth * 0.5f, totalHeight * 0.5f);

            for (int i = 0; i < _data.ids.Count; i++)
            {
                int x = _data.ids[i] % _data.width;
                int y = _data.ids[i] / _data.width;
                Vector2 position = startPosition + new Vector2(x * _spacing, -y * _spacing);

                GameObject o = LeanPool.Spawn(_itemGrid, position, Quaternion.identity, _tranParent);

                _listItemGrid.Add(o);
            }

            CreateObjShape();
        }

        private void CreateObjShape()
        {
            float totalWidth = (_data.width - 1) * _spacing;
            float totalHeight = (_data.width - 1) * _spacing;

            // Tính toán vị trí bắt đầu để lưới được đặt ở giữa màn hình
            Vector2 startPosition = new Vector2(-totalWidth * 0.5f, totalHeight * 0.5f);

            for (int i = 0; i < _data.idCentreShapes.Count; i++)
            {
                GameObject obj = LeanPool.Spawn(_dataShape.shapes[_data.idCentreShapes[i].idShapes]);
                ObjShape shape = obj.GetComponent<ObjShape>();
                Vector2 offset = shape.tranCentre.localPosition;
                int x = _data.idCentreShapes[i].idGrid % _data.width;
                int y = _data.idCentreShapes[i].idGrid / _data.width;
                Vector2 position = startPosition + new Vector2(x * _spacing, -y * _spacing);
                obj.transform.position = position - offset;
            }
        }

        public void ClearCell()
        {
            for (int i = 0; i < _listItemGrid.Count; i++)
            {
                LeanPool.Despawn(_listItemGrid[i]);
            }

            _listItemGrid.Clear();
        }

    }

    [System.Serializable]
    public class Data
    {
        public int width;
        public int height;
        public List<int> ids;
        public List<ShapeCreate> idCentreShapes;
    }

    [System.Serializable]
    public class ShapeCreate
    {
        public int idShapes;
        public int idGrid;
    }
}
