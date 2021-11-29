using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float speed;
    private bool stop;

    // Update is called once per frame
    void changeSpeed(float spe)
    {
        speed = spe;
    }
    void Update()
    {
        if (stop == false)
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            var rotation = Quaternion.identity;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * 2);
        }
    }

    public void stopRotation()
    {
        stop = true;
    }
}
