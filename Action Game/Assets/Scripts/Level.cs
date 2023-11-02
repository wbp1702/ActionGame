using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }

    void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple Level Instances");
            Destroy(gameObject);
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
