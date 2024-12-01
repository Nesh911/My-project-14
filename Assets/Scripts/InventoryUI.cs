using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;  // Панель инвентаря
    public GameObject toolbarPanel;    // Панель инструментов
    public Button[] inventorySlots;    // Ячейки в инвентаре
    public Button[] toolbarSlots;      // Ячейки на панели инструментов

    private bool isInventoryOpen = false;

    private void Start()
    {
        inventoryPanel.SetActive(false);  // Скрываем инвентарь при старте
        InitializeToolbar();
    }

    private void Update()
    {
        // Открываем/закрываем инвентарь по нажатию Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Жмем таб");
            ToggleInventory();
        }

        // Выбор предмета из панели инструментов с помощью клавиш 1-9 и 0
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
        Debug.Log("Выбран инструмент с панели инструментов: " + index);

        for (int i = 0; i < toolbarSlots.Length; i++)
        {
            toolbarSlots[i].GetComponent<Image>().color = (i == index) ? Color.yellow : Color.white;  // Выделение активного предмета
        }

        // Логика экипировки активного инструмента
        EquipTool(index);
    }

    private void EquipTool(int index)
    {
        Inventory inventory = GetComponent<Inventory>();
        if (inventory != null && index < inventory.items.Count)
        {
            Item selectedTool = inventory.items[index];
            // Здесь вызываем метод для экипировки инструмента в PlayerController
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                player.SetCurrentItem(selectedTool);
            }
        }
    }

}
