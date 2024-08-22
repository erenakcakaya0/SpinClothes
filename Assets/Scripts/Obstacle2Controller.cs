using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle2Controller : MonoBehaviour
{
    Rigidbody rb;
    float angle = 45f;
    public float speed;
    Vector3 axis = Vector3.up;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        //Dönmesi gereken engelleri kendi etrafında döndürüyor
        Quaternion q = Quaternion.AngleAxis(angle, axis);
        q.ToAngleAxis(out angle, out axis);
        rb.angularVelocity = axis * speed * angle * Mathf.Deg2Rad;
    }
}
