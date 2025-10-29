using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] public Vector3 offset;        // Смещение камеры относительно объекта

    void LateUpdate()
    {
        // Позиция камеры = позиция объекта + смещение
        transform.position = target.transform.position + offset;

        // Камера не вращается, просто смотрит в одном фиксированном направлении (можно настроить)
    }
}