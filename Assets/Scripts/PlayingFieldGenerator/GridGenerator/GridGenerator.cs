using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private Slider _gridColumns;
    [SerializeField] private Slider _gridRows;
    [SerializeField] private GridData _gridData;
    private List<Cell> _gridCellsList = new List<Cell>();
    private GameObject _grid;

    public List<Cell> GridCells => _gridCellsList;
    public Vector3 GridCenter
    {
        get
        {
            Vector3 sumVector = Vector3.zero;
            foreach(Transform child in _grid.transform)
            {
                sumVector += child.position;
            }
            return sumVector / _grid.transform.childCount;
        }
    }

    public float GridRadius
    {
        get
        {
            Vector3 extremePoint = _gridCellsList[_gridCellsList.Count - 1].GetComponent<BoxCollider>().bounds.max;
            float radius = Vector3.Distance(GridCenter, extremePoint);
            return radius;
        }
    }

    private void Start()
    {
        EventHandler.RestartLevelEvent.AddListener(GenerateNewGrid);
        GenerateNewGrid();
    }

    private void GenerateNewGrid()
    {
        Destroy(_grid);
        _gridCellsList.Clear();
        _grid = new GameObject("Grid");
        GenerateCells();
    }

    private void GenerateCells()
    {
        Vector3 startPos = LeftBottomCellCenter();
        CreateCells(startPos);
    }

    private Vector3 LeftBottomCellCenter()
    {
        float edgeCellsCenterDistanceX = _gridData.RealColumnsOffset * (_gridColumns.value - 1);
        float edgeCellsCenterDistanceY = _gridData.RealRowsOffset * (_gridRows.value - 1);
        float xPosition = edgeCellsCenterDistanceX / 2;
        float zPosition = edgeCellsCenterDistanceY / 2;
        return new Vector3(-xPosition, 0, -zPosition);
    }

    private void CreateCells(Vector3 startPos)
    {
        for (int row = 0; row < _gridRows.value; row++)
        {
            for (int col = 0; col < _gridColumns.value; col++)
            {
                Vector3 offsetCells = new Vector3(_gridData.RealColumnsOffset * col, 0, _gridData.RealRowsOffset * row);
                Cell newCell = Instantiate(_gridData.GridCell, startPos + offsetCells, _gridData.GridCell.transform.rotation);
                newCell.transform.SetParent(_grid.transform);
                _gridCellsList.Add(newCell);
            }
        }
    }
}
