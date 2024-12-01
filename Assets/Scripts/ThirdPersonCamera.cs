using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;                  // ��������, �� ������� ������� ������
    public float distance = 5.0f;             // ��������� ���������� �� ���������
    public float minDistance = 2.0f;          // ����������� ���������� (������������ �����������)
    public float maxDistance = 10.0f;         // ������������ ���������� (������������ ���������)
    public float scrollSpeed = 2.0f;          // �������� �����������/���������
    public float rotationSpeed = 5.0f;        // �������� �������� ������ ��� ������� ���

    private float currentRotationX = 0.0f;    // ���� �������� �� ���������
    private float currentRotationY = 0.0f;    // ���� �������� �� �����������
    private float currentDistance;            // ������� ���������� �� ���������

    private void Start()
    {
        // �������������� ��������� ���������� � ��������� ����
        currentDistance = distance;
        currentRotationX = transform.eulerAngles.x;
        currentRotationY = transform.eulerAngles.y;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            HandleZoom();
            HandleRotation();

            // ������������ ������� ������ ������������ ���������
            Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);
            Vector3 positionOffset = rotation * new Vector3(0, 0, -currentDistance);
            Vector3 targetPosition = target.position + Vector3.up * 1.5f;  // ��������� ����� ������� ������� ���� ������ ���������

            // ������������� ������� � ����������� ������
            transform.position = targetPosition + positionOffset;
            transform.LookAt(targetPosition);
        }
    }

    private void HandleRotation()
    {
        // �������� ������ ��� ������� ������ ������ ����
        currentRotationY += Input.GetAxis("Mouse X") * rotationSpeed;
        currentRotationX -= Input.GetAxis("Mouse Y") * rotationSpeed;
    }

    private void HandleZoom()
    {
        // ����������� � ��������� ������ � ������� �������� ����
        currentDistance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
    }
}
