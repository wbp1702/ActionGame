using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health = 100;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) Destroy(gameObject);

        Vector3 target = Player.Instance.transform.position;
        agent.SetDestination(target);

        target.y = transform.position.y;
        transform.rotation = Quaternion.LookRotation(target - transform.position, Vector3.up);
    }
}
