using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;                  // Персонаж, за которым следует камера
    public float distance = 5.0f;             // Начальное расстояние до персонажа
    public float minDistance = 2.0f;          // Минимальное расстояние (максимальное приближение)
    public float maxDistance = 10.0f;         // Максимальное расстояние (максимальное отдаление)
    public float scrollSpeed = 2.0f;          // Скорость приближения/отдаления
    public float rotationSpeed = 5.0f;        // Скорость вращения камеры при зажатой ПКМ

    private float currentRotationX = 0.0f;    // Угол вращения по вертикали
    private float currentRotationY = 0.0f;    // Угол вращения по горизонтали
    private float currentDistance;            // Текущее расстояние до персонажа

    private void Start()
    {
        // Инициализируем начальное расстояние и начальные углы
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

            // Рассчитываем позицию камеры относительно персонажа
            Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);
            Vector3 positionOffset = rotation * new Vector3(0, 0, -currentDistance);
            Vector3 targetPosition = target.position + Vector3.up * 1.5f;  // Поднимаем точку взгляда немного выше головы персонажа

            // Устанавливаем позицию и направление камеры
            transform.position = targetPosition + positionOffset;
            transform.LookAt(targetPosition);
        }
    }

    private void HandleRotation()
    {
        // Вращение камеры при зажатой правой кнопке мыши
        currentRotationY += Input.GetAxis("Mouse X") * rotationSpeed;
        currentRotationX -= Input.GetAxis("Mouse Y") * rotationSpeed;
    }

    private void HandleZoom()
    {
        // Приближение и отдаление камеры с помощью колесика мыши
        currentDistance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
    }
}
