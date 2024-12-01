using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();  // Список предметов в инвентаре
    public int maxCapacity = 40;                 // Максимальное количество слотов

    private int selectedItemIndex = -1;          // Индекс выбранного предмета (-1 если ничего не выбрано)

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallback;

    public bool AddItem(Item item)
    {
        if (items.Count >= maxCapacity)
        {
            Debug.Log("Инвентарь полон!");
            return false;
        }

        items.Add(item);
        onInventoryChangedCallback?.Invoke();
        return true;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        onInventoryChangedCallback?.Invoke();
    }

    public void DropItem(Item item, Vector3 dropPosition)
    {
        RemoveItem(item);
        Instantiate(item.worldModel, dropPosition, Quaternion.identity);
        Debug.Log("Предмет выброшен: " + item.itemName);
    }

    public Item GetSelectedItem()
    {
        if (selectedItemIndex >= 0 && selectedItemIndex < items.Count)
        {
            return items[selectedItemIndex];
        }
        return null;
    }

    public void SelectItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            selectedItemIndex = index;
            Debug.Log("Выбран предмет: " + items[selectedItemIndex].itemName);
        }
    }
}
