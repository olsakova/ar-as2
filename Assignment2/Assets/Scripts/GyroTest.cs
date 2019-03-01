using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroTest : MonoBehaviour
{
    //[SerializeField]
    //private float speed = 3f;
    //[SerializeField]
    //private float upSpeed =0.5f;
    //[SerializeField]
    //private float maxSpeed = 3f;

    //private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion deviceRotation = DeviceRotation.Get();

        transform.rotation = deviceRotation;

        //rb.AddForce(transform.up * upSpeed, ForceMode.Acceleration);
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

    }
}
