using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;  // ������ ���������
    public GameObject toolbarPanel;    // ������ ������������
    public Button[] inventorySlots;    // ������ � ���������
    public Button[] toolbarSlots;      // ������ �� ������ ������������

    private bool isInventoryOpen = false;

    private void Start()
    {
        inventoryPanel.SetActive(false);  // �������� ��������� ��� ������
        InitializeToolbar();
    }

    private void Update()
    {
        // ���������/��������� ��������� �� ������� Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("���� ���");
            ToggleInventory();
        }

        // ����� �������� �� ������ ������������ � ������� ������ 1-9 � 0
        for (int i = 0; i < toolbarSlots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectToolbarItem(i);
            }
        }
    }

    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);
    }

    private void InitializeToolbar()
    {
        for (int i = 0; i < toolbarSlots.Length; i++)
        {
            int index = i;
            toolbarSlots[i].onClick.AddListener(() => SelectToolbarItem(index));
        }
    }

    private void SelectToolbarItem(int index)
    {
        Debug.Log("������ ���������� � ������ ������������: " + index);

        for (int i = 0; i < toolbarSlots.Length; i++)
        {
            toolbarSlots[i].GetComponent<Image>().color = (i == index) ? Color.yellow : Color.white;  // ��������� ��������� ��������
        }

        // ������ ���������� ��������� �����������
        EquipTool(index);
    }

    private void EquipTool(int index)
    {
        Inventory inventory = GetComponent<Inventory>();
        if (inventory != null && index < inventory.items.Count)
        {
            Item selectedTool = inventory.items[index];
            // ����� �������� ����� ��� ���������� ����������� � PlayerController
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                player.SetCurrentItem(selectedTool);
            }
        }
    }

}
