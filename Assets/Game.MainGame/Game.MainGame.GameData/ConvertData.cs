using System.Collections.Generic;
using Codice.Client.BaseCommands.Merge.Restorer;
using Game.MainGame.Level;
using UnityEngine;

namespace Game.MainGame
{
    public class ConvertData : MonoBehaviour
    {
        [SerializeField] private Game1_LevelMapSO _dataConvert;
        [SerializeField] private Data _resultConvert;
        [SerializeField] private DataShape _dataShape;

        private void Start()
        {
            _resultConvert = new Data();
        }

        private Data ConvertData1(Game1_LevelMapSO data)
        {
            Data result = new Data();
            result.width = data.width - 2;
            result.height = data.height - 2;
            result.idCentreShapes = new List<ShapeCreate>();

            foreach(ObjectData block in data.blocks)
            {
                ShapeCreate newShape = new ShapeCreate();
                newShape.idShapes = ConvertIdShape(block.objectName);

            }

            return result;
        }

        private int ConvertIdShape(string name)
        {
            switch (name)
            {
                case "Block 1":
                    return 0;
                case "Block 2-1":
                    return 9;
                case "Block 2-2":
                    return 6;
                case "Block 2-3":
                    return 12;
                case "Block 2-4":
                    return 1;
                case "Block 3-1":
                    return -1;
                case "Block 3-1-1":
                    return 11;
                case "Block 3-2":
                    return -1;
                case "Block 3-2-1":
                    return -1;
                case "Block 3-3":
                    return -1;
                case "Block 3-3-1":
                    return 8;
                case "Block 3-4":
                    return -1;
                case "Block 3-4-1":
                    return -1;
                case "Block 4-1":
                    return 7;
                case "Block 4-2":
                    return 5;
                case "Block 5-1":
                    return 2;
                case "Block 5-2":
                    return 4;
                case "Block 6-1":
                    return -1;
                case "Block 6-2":
                    return -1;
                case "Block 7":
                    return 3;
                case "Block 8-1":
                    return -1;
                case "Block 8-2":
                    return -1;
                case "Block 8-3":
                    return -1;
                case "Block 8-4":
                    return -1;
                case "Block 9":
                    return -1;
            }

            return 0;
        }
        private int ConvertIdGridShape(ObjectData block, int idShape)
        {
            int idGrid = 0;
            ObjShape obj = _dataShape.shapes[idShape].GetComponent<ObjShape>();
            Vector3 offset = obj.tranCentre.localPosition;
            Vector3 tranCentre = block.position  + offset;

            return idGrid;
        }
    }
}
