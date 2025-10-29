using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class MassiveBody : MonoBehaviour
{
    public float mass => GetComponent<Rigidbody>().mass;
    private List<MassiveBody> _others = new();
    

    // Start is called before the first frame update
    public void Awake()
    {
        GetComponent<Rigidbody>().useGravity = false;

        _others.AddRange(FindObjectsByType<MassiveBody>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID).Where(obj =>
            obj != this && obj.TryGetComponent(typeof(WorldController), out _) ));
    }

    // Update is called once per frame
    void Update()
    {
        var rb = GetComponent<Rigidbody>();
        var position = transform.position;
        if (rb != null)
        {
            Vector3 velocity = rb.velocity;

            // Рисуем линию из позиции объекта в направлении скорости
            Debug.DrawRay(position, velocity, Color.red);
        }
    }

    private void FixedUpdate()
    {
        if (!TryGetComponent(typeof(WorldController), out var _))
        {
            var resultGVector = _others.Select(GetAttractionTo).Aggregate((acc, x) => acc + x);
            GetComponent<Rigidbody>().AddForce(resultGVector, ForceMode.Acceleration);
            Debug.DrawRay(transform.position, resultGVector * 10, Color.blue);
        }
    }

    private Vector3 GetAttractionTo(MassiveBody other)
    {
        var direction = (other.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(other.transform.position, transform.position);

        return direction * (WorldController.GravityConst * (mass * other.mass / (distance * distance)));
    }
}