using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float speed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(Time.timeSinceLevelLoad * speed, 0, 0);
    }
}
