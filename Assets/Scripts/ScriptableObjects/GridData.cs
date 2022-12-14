using UnityEngine;

[CreateAssetMenu(fileName = "New grid", menuName = "Create scriptable object/Create new grid")]
public class GridData : ScriptableObject
{
    [SerializeField] private float _columnsOffset;
    [SerializeField] private float _rowsOffset;
    [SerializeField] private Cell _gridCell;
    public Cell GridCell => _gridCell;
    public float RealRowsOffset => GridCell.transform.localScale.x + _rowsOffset;
    public float RealColumnsOffset => GridCell.transform.localScale.z + _columnsOffset;
}
