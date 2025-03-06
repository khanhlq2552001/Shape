using System.Collections.Generic;
using Lean.Pool;
using UnityEditor.Build.Content;
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

                    if(value != -1)
                    {
                        CheckResult();
                    }
                }
            }
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
            if(type == TypeWall.left)
            {
                GameObject obj = LeanPool.Spawn(LevelManager.Instance.dataShape.shapesWall[idWall]);

                Wall wall = obj.GetComponent<Wall>();
                wall.CalculatorCoordinates(ID);
                wall.SetMask(type);

                if (!isWall)
                {
                    wall.IDGate = idGate;

                    InfoWall infoWall = new InfoWall()
                    {
                        wall = wall,
                        idGate = idGate,
                        type = TypeWall.left
                    };

                    _listInfoWall.Add(infoWall);
                }

                Vector2 centre = wall.centre.localPosition;
                Vector2 newPos = (Vector2)transWall[0].position - centre;
                obj.transform.position = newPos;
            }
            else if(type == TypeWall.top)
            {
                GameObject obj = LeanPool.Spawn(LevelManager.Instance.dataShape.shapesWall[idWall]);

                Wall wall = obj.GetComponent<Wall>();
                wall.CalculatorCoordinates(ID);
                wall.SetMask(type);

                if (!isWall)
                {
                    wall.IDGate = idGate;

                    InfoWall infoWall = new InfoWall()
                    {
                        wall = wall,
                        idGate = idGate,
                        type = TypeWall.top
                    };

                    _listInfoWall.Add(infoWall);
                }

                Vector2 centre = wall.centre.localPosition;
                Vector2 newPos = (Vector2)transWall[1].position - centre;
                obj.transform.position = newPos;
            }
            else if (type == TypeWall.right)
            {
                GameObject obj = LeanPool.Spawn(LevelManager.Instance.dataShape.shapesWall[idWall]);

                Wall wall = obj.GetComponent<Wall>();
                wall.CalculatorCoordinates(ID);
                wall.SetMask(type);

                if (!isWall)
                {
                    wall.IDGate = idGate;

                    InfoWall infoWall = new InfoWall()
                    {
                        wall = wall,
                        idGate = idGate,
                        type = TypeWall.right
                    };

                    _listInfoWall.Add(infoWall);
                }

                Vector2 centre = wall.centre.localPosition;
                Vector2 newPos = (Vector2)transWall[2].position - centre;
                obj.transform.position = newPos;
            }
            else if (type == TypeWall.bot)
            {
                GameObject obj = LeanPool.Spawn(LevelManager.Instance.dataShape.shapesWall[idWall]);

                Wall wall = obj.GetComponent<Wall>();
                wall.CalculatorCoordinates(ID);
                wall.SetMask(type);

                if (!isWall)
                {
                    wall.IDGate = idGate;

                    InfoWall infoWall = new InfoWall()
                    {
                        wall = wall,
                        idGate = idGate,
                        type = TypeWall.bot
                    };
                    _listInfoWall.Add(infoWall);
                }

                Vector2 centre = wall.centre.localPosition;
                Vector2 newPos = (Vector2)transWall[3].position - centre;
                obj.transform.position = newPos;
            }
        }

        public void CheckResult()
        {
            ObjShape shape = LevelManager.Instance.GetListItemShape()[IDShape];
            for (int i = 0; i < _listInfoWall.Count; i++)
            {
                if (_listInfoWall[i].idGate == shape.IDGate)
                {
                    bool value = shape.CheckResult(_listInfoWall[i].type);

                    if (value)
                    {
                        shape.CanMove = false;
                        shape.EffectWin(_listInfoWall[i].type, _listInfoWall[i].wall);
                    }
                }
            }
        }

        public void SetWallCorner(TypeWallCorner type)
        {
                GameObject obj = LeanPool.Spawn(LevelManager.Instance.dataShape.shapesWallCorner[(int)type]);

                obj.transform.position = transWallCorner[(int)type].position;
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
