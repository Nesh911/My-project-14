using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();  // ������ ��������� � ���������
    public int maxCapacity = 40;                 // ������������ ���������� ������

    private int selectedItemIndex = -1;          // ������ ���������� �������� (-1 ���� ������ �� �������)

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallback;

    public bool AddItem(Item item)
    {
        if (items.Count >= maxCapacity)
        {
            Debug.Log("��������� �����!");
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
        Debug.Log("������� ��������: " + item.itemName);
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
            Debug.Log("������ �������: " + items[selectedItemIndex].itemName);
        }
    }
}
