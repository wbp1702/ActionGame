using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }

    [Header("Inscribed")]
    [SerializeField]
    Vector3 playerSpawnPosition = Vector3.zero;

    void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple Instances of Level");
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
        if (other.TryGetComponent<Player>(out Player player))
        {
            // If the player is out of bounds teleport them back to the start.
            player.Teleport(playerSpawnPosition);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
