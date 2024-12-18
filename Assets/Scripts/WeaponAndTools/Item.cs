using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;        // �������� ��������
    public Sprite icon;            // ������ �������� ��� UI
    public GameObject worldModel;  // ������ �������� ��� ���������� � ����
    public bool isStackable;       // ����� �� ���������� �������� � ���� ������

    // ����������� ������� ��� ���������, ����� ��� ����� � �������
    public virtual void Use()
    {
        Debug.Log("���������� �������: " + itemName);
    }
}