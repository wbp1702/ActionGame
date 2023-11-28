using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{

    [Header("Inscribed")]
    public float CameraHeight = 10f;
    
    [Range(0.0f, 1.0f)]
    public float LerpFactor = 0.05f;

    void FixedUpdate()
    {
        Vector3 target = Player.Instance.transform.position;
        target.y = CameraHeight;

        transform.position = Vector3.Lerp(transform.position, target, 0.1f);
    }
}
