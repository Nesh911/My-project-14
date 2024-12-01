using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;        // Название предмета
    public Sprite icon;            // Иконка предмета для UI
    public GameObject worldModel;  // Префаб предмета для размещения в мире
    public bool isStackable;       // Можно ли складывать предметы в одну ячейку

    // Специальные функции для предметов, такие как топор и молоток
    public virtual void Use()
    {
        Debug.Log("Используем предмет: " + itemName);
    }
}