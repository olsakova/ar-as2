using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyro2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }


    // Update is called once per frame
    void Update()
    {
        //Rotate on all axes using gyroscope
        Quaternion deviceRotation = GyroToUnity(Input.gyro.attitude);
        Vector3 deviceEulerAngles = deviceRotation.eulerAngles;
        Vector3 transformEulerAngles = transform.rotation.eulerAngles;
        transform.Rotate((new Vector3(transformEulerAngles.x, deviceEulerAngles.x, transformEulerAngles.z)) * (0.1f * Time.deltaTime));

        Debug.Log(Input.gyro.attitude); 
    }
}
