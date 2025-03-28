using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

namespace Game.MainGame
{
    public enum TypeWall
    {
        left,
        top,
        right,
        bot
    }

    public enum TypeWallCorner
    {
        topLeft,
        topRight,
        botRight,
        botLeft
    }

    public class ItemGrid : MonoBehaviour
    {
        [SerializeField] private int _idShape;
        [SerializeField] private List<InfoWall> _listInfoWall = new List<InfoWall>();

        private int _id;

        public Transform[] transWall;
        public Transform[] transWallCorner;

        public int IDShape
        {
            get => _idShape;

            // id = -1 nghia la khong co shape nào chứa ô đó
            set
            {
                if(value != _idShape)
                {
                    _idShape = value;
                }
            }
        }

        public List<InfoWall> GetListInfoWall()
        {
            return _listInfoWall;
        }

        public bool CheckItem(int idShape, int idGate, TypeWall type)
        {
            if (idShape != _idShape && _idShape != -1) return false;

            if (_listInfoWall.Count == 0) return true;


            for(int i=0; i< _listInfoWall.Count; i++)
            {
                if (_listInfoWall[i].idGate == idGate && _listInfoWall[i].type == type)
                {
                    return true;
                }

                if (_listInfoWall[i].type == type)
                {
                    return false;
                }
            }

            return true;
        }

        public int ID
        {
            get => _id;

            set => _id = value;
        }

        public void ResetAttibute()
        {
            _idShape = -1;
            _listInfoWall.Clear();
        }

        public void SetWall(TypeWall type, int idWall, bool isWall, int idGate)
        {
            DataColor data = GameManager.Instance.dataColors;

            if(type == TypeWall.left)
            {
                GameObject obj = LeanPool.Spawn(LevelManager.Instance.dataShape.shapesWall[idWall]);
                obj.transform.SetParent(LevelManager.Instance.TranParent());

                Wall wall = obj.GetComponent<Wall>();
                wall.SetMask(type);
                wall.ResetAtribute();
                LevelManager.Instance.AddWall(obj);

                if (!isWall)
                {
                    wall.IDGate = idGate;

                    wall.SetColor(false ,LevelManager.Instance.GetData().colorCodes[idGate]);

                    wall.SetArrow(type);

                    wall.CalculatorCoordinates(ID, isWall, idGate);
                }
                else
                {
                    wall.SetColor(true, 0);
                    wall.CalculatorCoordinates(ID, isWall, -1);
                }

                Vector2 centre = wall.centre.localPosition;
                Vector2 newPos = (Vector2)transWall[0].position - centre;
                obj.transform.position = newPos;
            }
            else if(type == TypeWall.top)
            {
                GameObject obj = LeanPool.Spawn(LevelManager.Instance.dataShape.shapesWall[idWall]);
                obj.transform.SetParent(LevelManager.Instance.TranParent());

                Wall wall = obj.GetComponent<Wall>();
                wall.SetMask(type);
                wall.ResetAtribute();
                LevelManager.Instance.AddWall(obj);

                if (!isWall)
                {
                    wall.IDGate = idGate;

                    wall.SetColor(false, LevelManager.Instance.GetData().colorCodes[idGate]);

                    wall.SetArrow(type);

                    wall.CalculatorCoordinates(ID, isWall, idGate);
                }
                else
                {
                    wall.SetColor(true, 0);

                    wall.CalculatorCoordinates(ID, isWall, -1);
                }

                Vector2 centre = wall.centre.localPosition;
                Vector2 newPos = (Vector2)transWall[1].position - centre;
                obj.transform.position = newPos;
            }
            else if (type == TypeWall.right)
            {
                GameObject obj = LeanPool.Spawn(LevelManager.Instance.dataShape.shapesWall[idWall]);
                obj.transform.SetParent(LevelManager.Instance.TranParent());

                Wall wall = obj.GetComponent<Wall>();
                wall.SetMask(type);
                wall.ResetAtribute();
                LevelManager.Instance.AddWall(obj);

                if (!isWall)
                {
                    wall.IDGate = idGate;

                    wall.SetColor(false, LevelManager.Instance.GetData().colorCodes[idGate]);

                    wall.SetArrow(type);

                    wall.CalculatorCoordinates(ID, isWall, idGate);
                }
                else
                {
                    wall.SetColor(true, 0);

                    wall.CalculatorCoordinates(ID, isWall, -1);
                }

                Vector2 centre = wall.centre.localPosition;
                Vector2 newPos = (Vector2)transWall[2].position - centre;
                obj.transform.position = newPos;
            }
            else if (type == TypeWall.bot)
            {
                GameObject obj = LeanPool.Spawn(LevelManager.Instance.dataShape.shapesWall[idWall]);
                obj.transform.SetParent(LevelManager.Instance.TranParent());

                Wall wall = obj.GetComponent<Wall>();
                wall.SetMask(type);
                wall.ResetAtribute();
                LevelManager.Instance.AddWall(obj);

                if (!isWall)
                {
                    wall.IDGate = idGate;

                    wall.SetColor(false, LevelManager.Instance.GetData().colorCodes[idGate]);

                    wall.SetArrow(type);

                    wall.CalculatorCoordinates(ID, isWall, idGate);
                }
                else
                {
                    wall.SetColor(true, 0);

                    wall.CalculatorCoordinates(ID, isWall, -1);
                }

                Vector2 centre = wall.centre.localPosition;
                Vector2 newPos = (Vector2)transWall[3].position - centre;
                obj.transform.position = newPos;
            }
        }

        public void AddInfoWall(Wall newWall, TypeWall type, int idGate)
        {
            _listInfoWall.Add(new InfoWall() {
                wall = newWall,
                type = type,
                idGate = idGate
            });
        }

        public InfoWall CheckResult()
        {
            ObjShape shape = LevelManager.Instance.GetListItemShape()[IDShape];
            for (int i = 0; i < _listInfoWall.Count; i++)
            {
                if (_listInfoWall[i].idGate == shape.IDGate && shape.IDGate2 == -1)
                {
                    bool value = shape.CheckResult(_listInfoWall[i].type);

                    if (value)
                    {
                        return _listInfoWall[i];
                    }
                }
                else if(_listInfoWall[i].idGate == shape.IDGate && shape.IDGate2 != -1)
                {
                    bool value = shape.CheckResult(_listInfoWall[i].type);

                    if (value)
                    {
                        return _listInfoWall[i];
                    }
                }

            }
            return new InfoWall() {
                wall = new Wall(),
                type = TypeWall.left,
                idGate = -1,
            };
        }

        public void SetWallCorner(TypeWallCorner type)
        {
                GameObject obj = LeanPool.Spawn(LevelManager.Instance.dataShape.shapesWallCorner[(int)type]);
                obj.transform.SetParent(LevelManager.Instance.TranParent());
                LevelManager.Instance.AddWall(obj);

                obj.transform.position = transWallCorner[(int)type].position;
                WallCorner wallCorner = obj.GetComponent<WallCorner>();
                wallCorner.SetColor();
        }
    }

    [System.Serializable]
    public class InfoWall
    {
        public Wall wall;
        public TypeWall type;
        public int idGate;
    }
}
