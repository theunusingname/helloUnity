using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float thrustForce = 10f;
    public float rotationSpeed = 100f;

    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }

    void FixedUpdate()
    {
        // Получение ввода с геймпада (например, левого стика)
        float moveInput = Input.GetAxis("Vertical");   // вперед/назад
        float turnInput = Input.GetAxis("Horizontal"); // поворот в стороны

        // Прямое движение вперед и назад
        var force = transform.forward * (moveInput * thrustForce);
        _rb.AddForce(force);

        // Вращение вокруг вертикальной оси
        var turn = turnInput * rotationSpeed * Time.fixedDeltaTime;
        var turnRotation = Quaternion.Euler(0f, turn, 0f);
        _rb.MoveRotation(_rb.rotation * turnRotation);
    }
}