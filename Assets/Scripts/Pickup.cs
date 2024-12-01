using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item toolData;  // Данные о предмете, который будет добавлен в инвентарь

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddItem(toolData);
                Destroy(gameObject);  // Удаляем объект с земли после подбора
            }
        }
    }
}
