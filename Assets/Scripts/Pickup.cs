using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item toolData;  // ������ � ��������, ������� ����� �������� � ���������

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddItem(toolData);
                Destroy(gameObject);  // ������� ������ � ����� ����� �������
            }
        }
    }
}
