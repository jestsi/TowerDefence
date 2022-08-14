using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Tools.UnityEditor
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private Cell _prefab;
        [SerializeField] private float _offset;
        [SerializeField] private Transform _parent;
        public Vector2Int GridSize { get => _gridSize; }

        [ContextMenu("Generate grid")]
        private void GenerateGrid()
        {
            var cellsSize = _prefab.GetComponent<MeshRenderer>().bounds.size;

            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int z = 0; z < _gridSize.y; z++)
                {
                    var position = new Vector3(x * (cellsSize.x + _offset), 0, z * (cellsSize.z + _offset));

                    var cell = Instantiate(_prefab, position, Quaternion.identity, _parent);
                    cell.name = $"X:{x} Y:{z}";
                }
            }
        }

        public static Cell[,] FindCountCells(GridGenerator generator)
        {
            var cells = new Cell[generator.GridSize.x, generator.GridSize.y];

            for (int x = 0; x < generator.GridSize.x; x++)
            {
                for (int y = 0; y < generator.GridSize.y; y++)
                {
                    cells[x, y] = GameObject.Find($"X:{x} Y:{y}").GetComponent<Cell>();
                }
            }

            return cells;
        }
    }
}
