using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 3.0f;
    public float runSpeed = 6.0f;
    public float jumpForce = 5.0f; 
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Inventory inventory; 
    public Transform cameraTransform;

    public Item[] items;           // ������ ��������� (��������, ����� � �������)
    private Item currentItem;       // ������� ��������� �������
    private int currentToolIndex = 0;  // ������ �������� ��������� �����������
    private Rigidbody rb;

    private UnityEngine.CharacterController controller;
    private Animator animator;
    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // ������������� ��������� ����������
        if (items.Length > 0)
        {
            foreach (var item in items)
            {
                inventory.AddItem(item);
            }
        }

        if (items.Length > 0)
        {
            currentItem = items[currentToolIndex];
            UpdateToolVisibility();
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        //    HandleToolSwitch();
        HandleItemUse();
        HandleInventoryInput();
    }

    private void HandleInventoryInput()
    {
        // ������ ������������ ����� �������� ������� (1, 2, 3...)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.SelectItem(0);
            EquipSelectedItem();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && inventory.items.Count > 1)
        {
            inventory.SelectItem(1);
            EquipSelectedItem();
        }
    }

    private void EquipSelectedItem()
    {
        Item selectedTool = inventory.GetSelectedItem();
        if (selectedTool != null)
        {
            currentItem = selectedTool;
            UpdateToolVisibility();
            Debug.Log("���������� �������: " + currentItem.itemName);
        }
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        if (move.magnitude >= 0.1f)
        {
            // ������� ������ ��� ����������� ��������
            Vector3 direction = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0) * move;
            Vector3 moveDirection = direction * speed * Time.deltaTime;

            rb.MovePosition(rb.position + moveDirection);

            // ������� ��������� � ����������� ��������
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 10 * Time.deltaTime);
        }

        animator.SetFloat("Speed", move.magnitude * speed);
    }

    private void HandleJump()
    {
        // ��������, ��������� �� �������� �� �����
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundMask);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // ��������� ��������� ���� �� ��� Y
            Debug.Log("����");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void HandleItemUse()
    {
        if (Input.GetMouseButtonDown(0) && currentItem != null)
        {
            currentItem.Use();
        }
    }

    private void HandleToolSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Set 1");
            //SetTool(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && items.Length > 1)
        {
            Debug.Log("Set 2");
            //SetTool(1);
        }
    }

public void SetCurrentItem(Item newTool)
{
    if (newTool != null)
    {
        currentItem = newTool;
        UpdateToolVisibility();
        Debug.Log("���������� �������: " + currentItem.itemName);
    }
    else
    {
            currentItem = null;
        UpdateToolVisibility();
        Debug.Log("���������� �����, ���� ��������.");
    }
}


    private void UpdateToolVisibility()
    {
        foreach (var item in items)
        {
            if (item != null && item.worldModel != null)
            {
                GameObject instance = Instantiate(item.worldModel);
                instance.SetActive(item == currentItem);
            }
        Debug.Log("����� � ���� " + currentItem.itemName);
        }
    }

}
