using CryoDI;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemGenerator : CryoBehaviour
{
    [SerializeField] protected GridItem _itemPrefab;
    protected List<GridItem> _allItems = new List<GridItem>();

    [Dependency] protected GridGenerator GridGenerator { get; set; }

    private void Start()
    {
        EventHandler.StartGameEvent.AddListener(StartGameHandler);
        EventHandler.RestartLevelEvent.AddListener(RestartLevelHandler);
        EventHandler.TimerIsEndEvent.AddListener(TimerIsEndHandler);
        EventHandler.ReturnMainMenuEvent.AddListener(ReturnMainMenuHandler);
    }

    protected Cell GetRandomCell()
    {
        int cellIndex = Random.Range(0, GridGenerator.GridCells.Count);
        return GridGenerator.GridCells[cellIndex];
    }

    protected GridItem CreateItemInCell(GridItem item, Cell cell)
    {
        GridItem newItem = Object.Instantiate(item, Vector3.zero, Quaternion.identity);
        _allItems.Add(newItem);
        newItem.PutInCell(cell);
        cell.PutItem(newItem.transform);
        return newItem;
    }

    protected void DestroyAllItems()
    {
        foreach (var i in _allItems)
        {
            if (i != null)
            {
                i.DestroyObject(0);
            }
        }
    }

    protected abstract void StartGameHandler();
    protected abstract void RestartLevelHandler();
    protected abstract void TimerIsEndHandler();
    protected abstract void ReturnMainMenuHandler();
}
