using UnityEngine;

public class BadItemsGenerator : ItemGenerator
{
    [SerializeField] private int _percentageOfBadItems;
    private int _itemsCountOnGrid;

    public void GenerateBadItems()
    {
        _itemsCountOnGrid = GridGenerator.GridCells.Count * _percentageOfBadItems / 100;
        for (int i = 0; i <= _itemsCountOnGrid; i++)
        {
            Cell randomCell = GetRandomCell();
            if (randomCell.IsEmpty)
            {
                CreateItemInCell(_itemPrefab, randomCell);
            }
        }
    }

    protected override void RestartLevelHandler()
    {
        DestroyAllItems();
        GenerateBadItems();
    }

    protected override void ReturnMainMenuHandler()
    {
        DestroyAllItems();
    }

    protected override void StartGameHandler()
    {
        GenerateBadItems();
    }

    protected override void TimerIsEndHandler()
    {
        DestroyAllItems();
    }
}
