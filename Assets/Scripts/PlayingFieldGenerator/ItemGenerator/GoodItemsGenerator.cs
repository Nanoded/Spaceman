using CryoDI;
using System.Collections;
using UnityEngine;

public class GoodItemsGenerator : ItemGenerator
{
    [SerializeField] private float _timeToSpawn;

    [Dependency] private ShipController ShipController { get; set; }

    private bool IsShotThroughPosition(Vector3 position)
    {
        Ray ray = new(position, ShipController.ShipPosition - position);
        foreach (var hit in Physics.RaycastAll(ray))
        {
            if (hit.collider.TryGetComponent(out GridItem item) && item.CurrentType == ItemType.BadItem)
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator GenerateGoodItems()
    {
        while(true)
        {
            Cell randomCell = GetRandomCell();
            while (!randomCell.IsEmpty)
            {
                randomCell = GetRandomCell();
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(_timeToSpawn);
            GridItem newItem = CreateItemInCell(_itemPrefab, randomCell);
            if(newItem == null) continue;
            if (IsShotThroughPosition(newItem.transform.position + Vector3.up * 0.5f) == false)
            {
                newItem.DestroyObject();
            }
        }
    }

    protected override void StartGameHandler()
    {
        StartCoroutine(GenerateGoodItems());
    }

    protected override void RestartLevelHandler()
    {
        StopAllCoroutines();
        DestroyAllItems();
        StartCoroutine(GenerateGoodItems());
    }

    protected override void TimerIsEndHandler()
    {
        StopAllCoroutines();
        DestroyAllItems();
    }

    protected override void ReturnMainMenuHandler()
    {
        StopAllCoroutines();
        DestroyAllItems();
    }
}
